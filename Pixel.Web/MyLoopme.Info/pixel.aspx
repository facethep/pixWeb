<%@ Page Language="C#"   %>

 <% string s = string.Empty;
    s = Request.QueryString["uid"];
    Response.Write(s); 

%>