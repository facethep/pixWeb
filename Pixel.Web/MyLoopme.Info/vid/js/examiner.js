(function() { 

var MAX_INJECTION_RETRY_COUNT = 500;
var INJECTION_RETRY_DELAY_MS = 50;

// See https://developers.google.com/youtube/js_api_reference#Events
var YOUTUBE_STATE_UNSTARTED = -1;
var YOUTUBE_STATE_ENDED = 0;
var YOUTUBE_STATE_PLAYING = 1;
var YOUTUBE_STATE_PAUSED = 2;
var YOUTUBE_STATE_BUFFERING = 3;
var YOUTUBE_STATE_CUED = 5;

// The image being injected to the YouTube pages in order to "ping" our servers
var IMAGE_INJECTION_URL = '/t/v1/t?v=1.0.0&a=internal&ap=1&m=ce&ec=ssfp&ea=vio';
var IMAGE_INJETION_SECURE_HOST = 'https://s-vop.sundaysky.com';
var IMAGE_INJETION_HOST = 'http://vop.sundaysky.com';
var EL_TYPE_PARAM = 'el';
var CACHE_PARAM = 'cb';
var SOURCE_PAGE_PARAM = 'sp';
var EL_TYPE_WITH_AD = 'other';
var EL_TYPE_WITHOUT_AD = 'avail';


/*
 * getYouTubeVideo
 *
 * Returns the jQuery-wrapped YouTube video (or null if none found)
 */
window.getYouTubeVideo = function() {
    var video = $('video');

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
 * examineVideoAd
 *
 * Examines a YouTube video to analyze how many ad injection opportunities we might have.
 *
 * @tryCount (optional) used when retrying video ad examination - in case jQuery script was not loaded yet.
 */
function examineVideoAd(tryCount) {

    if (typeof(tryCount) === 'undefined') {
        tryCount = 0;
    } else if (tryCount > MAX_INJECTION_RETRY_COUNT) {
        console.error('Timed out while waiting for jQuery and JWPlayer scripts to load');
        return;
    }

    if (typeof($) === 'undefined') {
        // jQuery script not loaded yet - wait a little longer (increate try count by one)
        setTimeout(function() { examineVideoAd(tryCount + 1); }, INJECTION_RETRY_DELAY_MS);
        return;
    }

    // Find the video to overlay the ad on top of it
    var video = getYouTubeVideo();

    if (!video) {
        console.error('No YouTube video element found - trying again');

        setTimeout(function() { examineVideoAd(tryCount + 1); }, INJECTION_RETRY_DELAY_MS);
        return;
    }

    console.info('Player state = ' + video.get(0).getPlayerState());
    /*
    if (video.get(0).getPlayerState() != YOUTUBE_STATE_PLAYING) {
        console.info('Waiting for video to start playing');
        setTimeout(function() { examineVideoAd(tryCount + 1); }, INJECTION_RETRY_DELAY_MS);
        return;
    }
    */

    // Prepare the URL for image injection (will be used as a "ping" to our servers)
    var imageUrlToInject = IMAGE_INJECTION_URL + '&' + CACHE_PARAM + '=' + Math.floor((Math.random() * 0xFFFFFF) + 0); // Prevent browser cache
    var videoUrl = video.get(0).getVideoUrl();

    console.info('Video URL:', videoUrl);

    // Original method for veryfing ad play:
    //if (/.+\/watch\?.*v=.+/.test(videoUrl)) {
    var flashVars = video.attr('flashvars');
    if (flashVars.indexOf('advideo=1') == -1) {
        // No YouTube video ad was displayed before the original video
        imageUrlToInject += '&' + EL_TYPE_PARAM + '=' + EL_TYPE_WITHOUT_AD;
        console.info('Without ad');
    } else {
        // A YouTube ad was displayed before the original video
        imageUrlToInject += '&' + EL_TYPE_PARAM + '=' + EL_TYPE_WITH_AD;
        console.info('With ad');
    }

    if (/https:\/\/.+/.test(window.location.href)) {
        // HTTPS connection
        imageUrlToInject = IMAGE_INJETION_SECURE_HOST + imageUrlToInject;
        imageUrlToInject += '&' + SOURCE_PAGE_PARAM + '=' + encodeURIComponent(window.location.href); // Add source URL as a parameter
    } else {
        // Regular HTTP connection
        imageUrlToInject = IMAGE_INJETION_HOST + imageUrlToInject;
        imageUrlToInject += '&' + SOURCE_PAGE_PARAM + '=' + encodeURIComponent(window.location.href); // Add source URL as a parameter
    }

    // Finally, inject the image to the main page so it'll "ping" our servers
    var image = $('<img src="' + imageUrlToInject + '" />');
    image.css('visibility', 'hidden');

    $('body').append(image);
}


console.log('Video Examiner: ', window.location.href);

if (/http(s)?:\/\/www.youtube.com\/watch\?v=/.test(window.location.href)) {
    // Make sure we only run for YouTube video pages
    console.log('In YouTube video');

    examineVideoAd();
}

}).call(this);

