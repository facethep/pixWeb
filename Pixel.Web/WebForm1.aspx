<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Pixel.Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script language="javascript">

        function getPath()
        {

            return strDomain.value + '/r/' + strProvider.value + strPage.value + txt5.value;
        }

        function updateDiv(obj) {
            
           
            switch (obj.value) {

                case '1003':
                    txt5.value = "/?affid=34&tranid=134";
                    break;
                case '1000':
                    txt5.value = "/?tid=myretval&rid=macit";
                    break;

                default:
                    txt5.value = "/?tid=myretval&rid=macit";
                    break;



            }
         /*   if (obj.value == '1003') {
                txt5.value = "/?affid=34&tranid=134";
            }
            else {
                txt5.value = "/?tid=myretval&rid=macit";

            }*/
            fullurl.innerHTML = getPath();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
  <br />
   <table border="1" cellspacing="0" cellpadding="4" >
        <tr><td colspan="3">Domain: 
            <select name="strDomain" id ="strDomain"  onchange="updateDiv();">
               
                <option value="http://localhost:49273/api">http://localhost:49273/api</option>
                <option value="http://www.myloopme.com/api">http://www.myloopme.com/api</option>
                </select> 

                &nbsp Provider &nbsp: <select name="strProvider" id ="strProvider" onchange="updateDiv(this);">
                                            <option value=""  >-   PROVIDER   -</option>    
                                            <option value="1000" selected>Test - 1000</option>                         
                                            <option value="1003">clickDealer - 1003</option>
                                             <option value="1010">adCash - 1010</option>
                                            <option value="1015">PropellerAds - 1015</option>
                                            <option value="1008">Mondoo Media - 1008</option>
                                            <option value="1033">Avazu- 1033</option>
                                            <option value="2020">YTZ - 2020</option>

             



                                    </select>
                 &nbsp; Page &nbsp: <select name="strPage" id ="strPage" onchange="updateDiv(this);">
                                            <option value="9999" selected>Test page - 9999</option>                         
                                             <option value="1005">Flash Player- 1005</option>
                                            <option value="2005">Flash Player- 2005</option>
                                            <option value="3001">FLV Video downloader - 3001</option>
                                            <option value="5000">Air Installer - 5000</option>
                                            <option value="5010">Iron Source - 5010</option>
                                            <option value="5011">Air Installer - 5011</option>
                                            <option value="3002">AdKnowledge - 3002</option>
                                    </select>
            &nbsp;Params: <input type="text" id ="txt5" name="txt5" value="/?affid=34&tranid=134" style="width: 359px"  onchange="updateDiv();" />
            <input type="button" value="go" onclick="window.open(getPath(), '', 'width=600, height=400');" /><br />
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="Get coutry by IP" />
            <asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
            <asp:Label ID="lblIP" runat="server" Text="Label"></asp:Label>
            &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="load d ip" />
            <asp:Label ID="lblCountry" runat="server" Text="Label"></asp:Label>
            <br />
            
            <asp:Button ID="rfrCache" runat="server" OnClick="rfrCache_Click" Text="Refresh Cache" />
            <asp:Label ID="label1" runat="server" Text="Label"></asp:Label>
&nbsp;<h3><b><div id="fullurl" align="center" > </div></b></h3>
            </td>

        </tr>    
            <tr>
                <td >Request to landing page</td>
                <td>Good: 
                    <input type="text" id ="txt1" name="txt1" value="/r/10031005/?affid=34&tranid=134" style="width: 359px"/>
                <input type="button" value="go" onclick="window.open(strDomain.value + txt1.value, '', 'width=600, height=400');" />
                </td>
                <td>Bad
                     <input type="text" id ="txt2" name="txt2" value="/r/10031008/?affid=34&tranid=134"  style="width: 359px"/>
                    <input type="button" value="go" onclick="window.open(strDomain.value+txt2.value, '', 'width=600, height=400');" />
                    </td>
            </tr>
            <tr>
                <td class="auto-style1">Response from DLM</td>
                <td>Good
                     <input type="text" id ="txt3" name="txt3" value="/s?uid=PUT_UID"  style="width: 359px"/>
                    <input type="button" value="go" onclick="window.open(strDomain.value+txt3.value, '', 'width=600, height=400');" /></td>
                <td>Bad
                     <input type="text" id ="txt4" name="txt4" value="/s?uid=399b1bea-81b1-4ad9-b55f-a8ff9eebd2a" style="width: 359px"/>
                    <input type="button" value="go" onclick="window.open(strDomain.value+txt4.value, '', 'width=600, height=400');" /></td>
            </tr>
        </table>
        <hr>

    
     
    </form>
</body>
</html>
