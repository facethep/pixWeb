<%@ Page Language="C#"   %>

<h2>Show all applications counter per provider </h2><br>
 <% int iApp ;
   
    iApp = System.Web.HttpContext.Current.Application.Count;
    string key;
    for (int i = 0; i < iApp; i++)
        {
            key = System.Web.HttpContext.Current.Application.Keys[i];
            Response.Write(key + " = " + System.Web.HttpContext.Current.Application[key]  + "</br>");
        }

%>