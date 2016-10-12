<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Pinger.aspx.cs" Inherits="Pinger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="tmr_Main" EventName="Tick" />
    </Triggers>
    <ContentTemplate>
    <asp:Label ID="lbl_DateTime" runat="server" />
    </ContentTemplate>

    </asp:UpdatePanel>


    <asp:Timer ID="tmr_Main" OnTick="tmr_Main_Tick" Interval="10000" runat="server">
    </asp:Timer>

</asp:Content>

