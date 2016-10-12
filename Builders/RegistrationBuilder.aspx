<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RegistrationBuilder.aspx.cs" Inherits="Builders_RegistrationBuilder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:UpdatePanel ID="up_Form" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <table>
        <tr>
        <td>
        Registration Header:
        </td>
            </tr>
        <tr>
        <td>        </td>
        </tr>
     
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

