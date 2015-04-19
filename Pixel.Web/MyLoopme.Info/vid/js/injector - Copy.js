(function() { 

// The URL of the JWPlayer flash player
var FLASH_PLAYER_URL = 'http://loopme.info/vid/jwplayer/player.swf';
var TRANSPARENT_GIF_URL = 'http://loopme.info/vid/transparent-gif.png';
// What URL to open when the user clicks on the video ad (currently static)
var VIDEO_AD_URL = 'http://www.lenovo.com';
// Where to bring the video ad from (currently static)
var VIDEO_AD_VIDEO_URL = 'http://loopme.info/vid/lenovo.mp4';

var MAX_INJECTION_RETRY_COUNT = 500;
var INJECTION_RETRY_DELAY_MS = 50;

// See https://developers.google.com/youtube/js_api_reference#Events
var YOUTUBE_STATE_UNSTARTED = -1;
var YOUTUBE_STATE_ENDED = 0;
var YOUTUBE_STATE_PLAYING = 1;
var YOUTUBE_STATE_PAUSED = 2;
var YOUTUBE_STATE_BUFFERING = 3;
var YOUTUBE_STATE_CUED = 5;

// Indicates whether or not the video ad was shown completely (until the end)
var adShownEntirely = false;


// Callback function called each time the YouTube player changes its state (must be global,
// since it's being called by the YouTube flash player)
window.onPlayerStateChange = function(newState) {
    console.log('onPlayerStateChange', newState);

    if ((newState == YOUTUBE_STATE_PLAYING) && (!adShownEntirely)) {
        // In case the video was buffering when we started displaying the ad, and
        // then started playing in the background - we must pause it.
        var video = getYouTubeVideo();
        video.get(0).pauseVideo();
    }
}


/*
 * getYouTubeVideo
 *
 * Returns the jQuery-wrapped YouTube video (or null if none found) - saved as a
 * global function since it's used by onPlayerStateChange
 */
window.getYouTubeVideo = function() {
    var video = $('embed');

    if (video.size() == 0) {
        // In IE, the video is saved as an object tag
        video = $('object');

        if (video.size() == 0) {
            return null;
        }
    }

    return video;
}


/*
 * resumeOriginalVideo
 *
 * Resumes playing of original video and hides our video ad
 *
 */
window.resumeOriginalVideo = function() {
    var video = getYouTubeVideo();
    video.show();
    $('#videoAdParent').hide();

    var player = jwplayer("videoAdContainer");
    player.stop();

    // Mark ad as shown (must be done before we resume the original video, since otherwise
    // the onPlayerStateChange callback will pause the video right back)
    adShownEntirely = true;
    // Override original onPlayerStateChange function - not needed anymore
    window.onPlayerStateChange = function(newState) { return; }

    // Resume playback of original video
    video.get(0).playVideo();
}

/*
 * injectVideoAd
 *
 * Injects a video ad on top of the existing YouTube video. Pauses the video before playing.
 * When the ad is clicked, will open a new window. Only when the ad is done playing, it'll
 * hide the ad and resume original video play.
 *
 * @tryCount (optional) used when retrying video ad injection - in case jQuery/JWPlayer scripts
 *                      were not loaded yet).
 */
function injectVideoAd(tryCount) {

    if (typeof(tryCount) === 'undefined') {
        tryCount = 0;
    } else if (tryCount > MAX_INJECTION_RETRY_COUNT) {
        console.error('Timed out while waiting for jQuery and JWPlayer scripts to load');
        return;
    }

    if ((typeof($) === 'undefined') || (typeof(jwplayer) === 'undefined')) {
        // jQuery / JWPlayer scripts not loaded yet - wait a little longer (increate try count by one)
        setTimeout(function() { injectVideoAd(tryCount + 1); }, INJECTION_RETRY_DELAY_MS);
        return;
    }

    // Find the video to overlay the ad on top of it
    var video = getYouTubeVideo();

    if (!video) {
        console.error('No YouTube video element found - trying again');

        setTimeout(function() { injectVideoAd(tryCount + 1); }, INJECTION_RETRY_DELAY_MS);
        return;
    }

    // First, pause the video so we'll display the video ad first
    video.get(0).addEventListener("onStateChange", "onPlayerStateChange");
    video.get(0).pauseVideo();

    // Create the video ad container (on top of the video)
    var ad = $('<div id="videoAdParent"><div id="videoAdContainer"></div></div>');
    ad.css('background', 'black');
    ad.css('z-index', '999');
    ad.css('position', 'absolute');
    ad.css('left', video.offset().left);
    ad.css('top', video.offset().top);
    ad.width(video.outerWidth());
    ad.height(video.outerHeight());

    // Display the ad
    $('body').append(ad);

    video.hide();

    // Create the video inside the container
    adPlayer = jwplayer("videoAdContainer").setup({
        autostart: true,
        controlbar: "none",
        flashplayer: FLASH_PLAYER_URL,
        file: VIDEO_AD_VIDEO_URL,
        height: video.outerHeight(),
        width: video.outerWidth(),
        menu: false,
        icons: false,
        events: {
            onReady: function() { console.log('onReady'); },

            onComplete: function() {
                // Video ad done displaying - hide the ad and resume playing original video
                console.log('Video Ad - onComplete');

                resumeOriginalVideo();
            },

            onPause: function() {
                // Video ad has been clicked - open the ad URL
                console.log('Video Ad - onPause');

                window.open(VIDEO_AD_URL);
            }
        }
    });

    // Add a Flash var to disable most Flash menu options
    flashContainer = $(adPlayer.container);
    flashContainer.append($('<param name="menu" value="false">'));

    // Add a new div to intercept video ad clicks made (this div will overlap the video ad)
    // We use this method since IE blocks any popups if not occurring directly from an active onclick event
    var adClick = $('<a></a>');
    adClick.css('z-index', '1000');
    adClick.css('position', 'absolute');
    adClick.width(video.outerWidth());
    adClick.height(video.outerHeight());

    adClick.click(function() {
        var player = jwplayer("videoAdContainer");
        var state = player.getState();

        if (state == 'PLAYING') {
            player.pause();
            window.open(VIDEO_AD_URL);
        } else {
            player.play();
        }
    });

    // Display the ad
    $('#videoAdParent').prepend(adClick);

    // Yellow progress bar in the bottom of the ad (mimic the same YouTube ad behaviour)
    var progressBar = $('<div></div>');
    progressBar.css('z-index', '1000');
    progressBar.css('position', 'absolute');
    progressBar.css('bottom', '0');
    progressBar.width(video.outerWidth() - 2);
    progressBar.height('4px');
    progressBar.css('background', 'yellow');
    progressBar.css('border', '1px solid #666666');
    // Add a gradient (support for all browsers)
    // Old browsers
    progressBar.css('background', '#F6D85B');
    // IE10
    progressBar.css('background-image', '-ms-linear-gradient(top, #F6D85B 0%, #DEA405 100%)');
    // Mozilla Firefox
    progressBar.css('background-image', '-moz-linear-gradient(top, #F6D85B 0%, #DEA405 100%)');
    // Opera
    progressBar.css('background-image', '-o-linear-gradient(top, #F6D85B 0%, #DEA405 100%)');
    // Webkit (Safari/Chrome 10)
    progressBar.css('background-image', '-webkit-gradient(linear, left top, left bottom, color-stop(0, #F6D85B), color-stop(1, #DEA405))');
    // Webkit (Chrome 11+)
    progressBar.css('background-image', '-webkit-linear-gradient(top, #F6D85B 0%, #DEA405 100%)');
    // W3C Markup, IE10 Release Preview
    progressBar.css('background-image', 'linear-gradient(to bottom, #F6D85B 0%, #DEA405 100%)');
    // IE 6-9
    progressBar.css('filter', "progid:DXImageTransform.Microsoft.gradient( startColorstr='#F6D85B', endColorstr='#DEA405',GradientType=0 )");
    adClick.prepend(progressBar);

    // Add a 'Skip Ad' button
    var skipAd = $('<div><div>Skip Ad &raquo;</div></div>');
    skipAd.css('z-index', '1000');
    skipAd.css('position', 'absolute');
    skipAd.css('bottom', '35px');
    skipAd.css('right', '1px');
    skipAd.width('180px');
    skipAd.height('34px');
    skipAd.css('border', '1px solid white');
    skipAd.css('background', 'transparent');
    skipAd.css('text-align', 'center');
    skipAd.css('color', 'white');
    skipAd.css('font-size', '19px');
    skipAd.css('box-shadow', '-1px -1px 5px #CCCCCC');
    skipAd.css('-moz-box-shadow', '-1px -1px 5px #CCCCCC'); // Older Firefox
    skipAd.css('-webkit-box-shadow', '-1px -1px 5px #CCCCCC'); // Older chrome

    skipAd.hover(function() {
        $(this).css('text-decoration', 'underline');
    }, function() {
        $(this).css('text-decoration', 'none');
    });

    skipAd.css('cursor', 'pointer');
    skipAd.css('display', 'table');
    skipAd.find('div').css('display', 'table-cell');
    skipAd.find('div').css('vertical-align', 'middle');
    skipAd.find('span').css('font-size', '12px');
    adClick.prepend(skipAd);

    skipAd.click(function(ev) {
        ev.stopPropagation();

        // Skip the ad
        resumeOriginalVideo();
    });

    var skipAdBackground = $('<div></div>');
    skipAdBackground.css('z-index', '1000');
    skipAdBackground.css('position', 'absolute');
    skipAdBackground.css('bottom', '35px');
    skipAdBackground.css('right', '1px');
    skipAdBackground.width('180px');
    skipAdBackground.height('34px');
    skipAdBackground.css('background', 'black');
    skipAdBackground.css('opacity', '0.7');
    adClick.prepend(skipAdBackground);
 
 
    var img = $('<img src="' + TRANSPARENT_GIF_URL + '" width="' + video.outerWidth() + '" height="' + video.outerHeight() + '" />');
    img.css('cursor', 'pointer');
    img.width(video.outerWidth());
    img.height(video.outerHeight());

    adClick.append(img);

}


console.log('Video Injector: ', window.location.href);

if (/http:\/\/www.youtube.com\/watch\?v=/.test(window.location.href)) {
    // Make sure we only run for YouTube video pages
    console.log('In YouTube video');

    injectVideoAd();
}

}).call(this);

