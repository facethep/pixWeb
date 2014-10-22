<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cacheObjets.aspx.cs" Inherits="Pixel.Web.sys.cacheObjets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h3>All object in Cache</h3></div>
        <asp:BulletedList ID="BulletedList1" runat="server">
        </asp:BulletedList>
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <br />

      
    </form>
</body>
</html>
