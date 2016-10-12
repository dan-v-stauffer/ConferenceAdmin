<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultRedirectErrorPage.aspx.cs"  
MasterPageFile="~/ErrorMaster.master" Inherits="Errors_DefaultRedirectErrorPage" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
  <tr>
  <td  valign="top" style="padding-right:5px">
  <img src="../Images/warning.png" alt="Webpage error" />
  </td>
  <td valign="top" style="padding-left:5px">
  <div style="width:800px">
    <h2>
      Engineering Conference 2014 Registration - Error</h2>
      <p>
        An error has occurred on the Engineering Conference 2014 Registration Website.<br />We apologize for the inconvenience.</p>
        <p>
        If you were in the middle of registering, your data may have been saved to a certain point. Restart your browser and try again.</p>
    <p>
    We've been notified of this error and will work to resolve it. Please contact <a href="mailto:daniel.stauffer@kla-tencor.com">Dan Stauffer (Corp PLC)</a> if you cannot complete or access your registration.
    </p>
    
    <p>
    Return to the <a href='../Default.aspx'>Engineering Conference 2014 Registration Home Page.</a></p>
  </div>
  
  </td>
  </tr></table>
  
</asp:Content>