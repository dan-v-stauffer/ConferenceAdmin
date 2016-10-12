<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccessUnauthorizedErrorPage.aspx.cs" 
MasterPageFile="~/ErrorMaster.master" Inherits="Errors_AccessUnauthorizedErrorPage" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
  <tr>
  <td  valign="top" style="padding-right:5px">
  <img src="../Images/error.png" alt="Webpage error" />
  </td>
  <td valign="top" style="padding-left:5px">
  <div style="width:800px">
    <h2>
      Engineering Conference 2014 Adminstration Site -<br /> Unauthorized Access Error</h2>
      <p>
       You do not have administrator priviliedges to the Engineering Conference 2014 Adminstration Website.</p>
            <p>
    Please contact <a href="mailto:daniel.stauffer@kla-tencor.com">Dan Stauffer (Corp PLC)</a> if you have questions concerning your registration.
    </p>
    
    <p>
    Return to the <a href='http://productivity/EngineeringConferenceRegistration/'>Engineering Conference 2014 Registration Home Page.</a></p>
  </div>
  
  </td>
  </tr></table>
  
</asp:Content>
