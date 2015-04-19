<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="insurance.Amin.Detail" %>

<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css">
     <script language="javascript">

         $(function () {
             $("[id$=txtFromDate]").datepicker({ showOn: 'button' });
         });

         $(function () {
             $("[id$=txtToDate]").datepicker({ showOn: 'button' });
         });

         function copyDate() {
             document.getElementById("txtToDate").value = document.getElementById("txtFromDate").value;
         }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <uc2:menu ID="menu1" runat="server" />
    <div>
    <h1>Detail Summary</h1>
       
        <br />

          From date (mm-dd-yyyy):
        <asp:TextBox ID="txtFromDate" runat="server" Width="74px"></asp:TextBox>
        &nbsp;<input id="Button4" type="button" value="&gt;" onclick="copyDate();" />&nbsp;To date:&nbsp;
        <asp:TextBox ID="txtToDate" runat="server" Width="74px"></asp:TextBox>
&nbsp;
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Show" />
    </div>
        <br /><br />
         <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
             <AlternatingRowStyle BackColor="White" />
             <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#EFF3FB" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <SortedAscendingCellStyle BackColor="#F5F7FB" />
             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
             <SortedDescendingCellStyle BackColor="#E9EBEF" />
             <SortedDescendingHeaderStyle BackColor="#4870BE" />
         </asp:GridView>
    </form>
</body>
</html>
