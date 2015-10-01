<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated)
    {
        DateTime expirydate = OOTS.Classes.UtilityOperations.GetExpiryDate();
%>
Expiry Date:
<%:  expirydate.ToString()%>
[
<%: Html.ActionLink("Log Off", "LogOff", "Account") %>
] [
<%: Html.ActionLink("Change Password", "ChangePassword", "Account") %>
]
<%
    }
    else
    {
%>
[
<%: Html.ActionLink("Log On", "LogOn", "Account") %>
]
<%
    }
%>
