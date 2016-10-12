<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="EventManager.aspx.cs" Inherits="EventManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="up_Form" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnl_ManageAgenda" Width="1075" runat="server">
                <table>
                    <tr>
                        <td>
                            Use this feature to manage the events at the conference.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="innerTable">
                                <tr>
                                    <td class="menuCell">
                                        <img src="Images/calendar.gif" alt="Scheduler" />
                                        <div>
                                            Scheduler
                                        </div>
                                    </td>
                                    <td class="menuCell">
                                        <asp:ImageButton ID="imgbn_NewEvent" ImageUrl="Images/new_registration.png" OnClick="bn_CreateNewEvent"
                                            runat="server" />
                                        <div>
                                            <asp:LinkButton ID="lb_NewEvent" OnClick="bn_CreateNewEvent" runat="server">Create <br />New Event</asp:LinkButton></div>
                                    </td>
                                    <td class="menuCell">
                                        <asp:ImageButton ID="imgbn_RefreshTreeView" ImageUrl="Images/refresh.png" runat="server" />
                                        <div>
                                            <asp:LinkButton ID="lb_RefreshTreeView" runat="server">Refresh Events</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="up_tv_Event" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lb_RefreshTreeView" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="imgbn_RefreshTreeView" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:TreeView ID="tv_Events" runat="server" OnTreeNodeDataBound="tv_Events_TreeNodeDataBound"
                                        Width="1075" NodeWrap="true" HoverNodeStyle-Font-Underline="true" ShowLines="true"
                                        PathSeparator="/" OnSelectedNodeChanged="tv_Events_SelectedNodeChanged">
                                    </asp:TreeView>
                                    <asp:Button ID="bn_ShowEventDetailsModal" runat="server" Style="display: none" />
                                    <asp:ModalPopupExtender ID="mdl_EventDetailsModal" runat="server" TargetControlID="bn_ShowEventDetailsModal"
                                        PopupControlID="pnl_EventDetails" BackgroundCssClass="modalBackground" />
                                    <asp:UpdatePanel ID="up_ManageEvent" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnl_EventDetails" runat="server" Width="650" BackColor="White" BorderColor="Navy"
                                                BorderStyle="Solid" BorderWidth="2" Style="padding: 10px 10px 10px 10px">
                                                <asp:UpdatePanel ID="up_ManageEventControls" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="bn_EventDetailsEdit" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <table class="formTable">
                                                            <tr>
                                                                <td>
                                                                    Event Type:<br />
                                                                    <asp:DropDownList ID="ddl_EventTypes" Width="225" ForeColor="#696969" AutoPostBack="true"
                                                                        CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                        runat="server" Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td rowspan="2" style="padding-left: 75px">
                                                                    Event Location:<br />
                                                                    <asp:DropDownList ID="ddl_Venues" Width="225" CssClass="input" runat="server" Enabled="False">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddl_Rooms" Width="225" CssClass="input" runat="server" Enabled="False">
                                                                    </asp:DropDownList>
                                                                    <asp:CascadingDropDown ID="cascex_Venues" runat="server" TargetControlID="ddl_Venues"
                                                                        Category="VendorID" PromptText="Select Venue:" ServicePath="~/ConferenceWebService.asmx"
                                                                        ServiceMethod="GetConferenceVenues" />
                                                                    <asp:CascadingDropDown ID="cascex_VenueSpaces" runat="server" TargetControlID="ddl_Rooms"
                                                                        Category="roomName" PromptText="Select Space:" ParentControlID="ddl_Venues" ServicePath="~/ConferenceWebService.asmx"
                                                                        ServiceMethod="GetVenueSpaces" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <td>
                                                                <asp:CheckBox ID="cb_PublicEvent" Text="Public Event:  " TextAlign="Left" runat="server" />
                                                            </td>
                                                            <td>
                                                            </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    Event Title:<br />
                                                                    <asp:TextBox ID="tb_EventTitle" runat="server" Width="600" CssClass="input" TextMode="MultiLine"
                                                                        Enabled="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Event Start:<br />
                                                                    <table class="dateTimeTables">
                                                                        <tr>
                                                                            <td>
                                                                                Date:
                                                                            </td>
                                                                            <td colspan="3">
                                                                                Time:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-top: 2px">
                                                                                <asp:CalendarExtender ID="calex_EventStart_Date" TargetControlID="tb_EventStart_Date"
                                                                                    runat="server" PopupPosition="BottomLeft" />
                                                                                <asp:TextBox ID="tb_EventStart_Date" CssClass="input" runat="server" Enabled="False"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_EventStart_Hour" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_EventStart_Minute" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_Event_Start_Meridian" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False">
                                                                                    <asp:ListItem Text="AM" Value="AM" Selected="True" />
                                                                                    <asp:ListItem Text="PM" Value="PM" />
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    Event Stop:<br />
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                Date:
                                                                            </td>
                                                                            <td colspan="3">
                                                                                Time:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CalendarExtender ID="calex_EventStop_Date" TargetControlID="tb_EventStop_Date"
                                                                                    runat="server" PopupPosition="BottomLeft" />
                                                                                <asp:TextBox ID="tb_EventStop_Date" CssClass="input" runat="server" Enabled="False"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_EventStop_Hour" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_EventStop_Minute" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddl_Event_Stop_Meridian" Width="50" ForeColor="#696969" AutoPostBack="true"
                                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" DropDownStyle="DropDownList"
                                                                                    runat="server" Enabled="False" EnableTheming="True">
                                                                                    <asp:ListItem Text="AM" Value="AM" Selected="True" />
                                                                                    <asp:ListItem Text="PM" Value="PM" />
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="style1">
                                                                    Parent Event:<br />
                                                                    <asp:DropDownList ID="ddl_ParentEvent" Width="610" ForeColor="#696969" AutoPostBack="true"
                                                                        CssClass="WindowsStyle" AutoCompleteMode="Suggest" runat="server" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="up_mdlManageEvent_ActionButtons" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="mdl_ActionButtons" style="width: 100%">
                                                            <div align="left" style="width: 20%; margin-top: 15px; float: left;">
                                                                <asp:Button ID="bn_EventDetailsDelete" runat="server" Text="Delete" Width="50px"
                                                                    OnClick="bn_EventDetailsModalDelete_Click" CssClass="aspHoverRedButton" CausesValidation="false" />
                                                            </div>
                                                            <div align="right" style="width: 75%; margin-top: 15px; float: left">
                                                                <asp:Button ID="bn_EventDetailsEdit" runat="server" Text="Edit" Width="50px" OnClick="bn_EventDetailsModalEdit_Click"
                                                                    CssClass="aspHoverButton" CausesValidation="false" />
                                                                <asp:Button ID="bn_EventDetailsModalSave" runat="server" Text="Save" Width="50px"
                                                                    OnClick="bn_EventDetailsModalSave_Click" CssClass="aspHoverButton" CausesValidation="false" />
                                                                <asp:Button ID="bn_EventDetailsModaClose" runat="server" Text="Cancel" Width="50px"
                                                                    OnClick="bn_EventDetailsModalCancel_Click" BackColor="White" BorderColor="Silver"
                                                                    BorderWidth="1" CssClass="aspHoverButton" CausesValidation="false" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="bn_ShowUtilityModal" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="mdl_UtilityModal" runat="server" TargetControlID="bn_ShowUtilityModal"
                PopupControlID="pnl_UtilityModal" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="pnl_UtilityModal" runat="server" Width="500px" BackColor="White" BorderColor="Navy"
                BorderStyle="Solid" BorderWidth="2" Style="display: none; padding: 10px 10px 10px 10px">
                <asp:UpdatePanel ID="up_UtilityModal" runat="server">
                    <ContentTemplate>
                        <div id="container" style="display: table">
                            <h2>
                                <asp:Label ID="lbl_UtilityModalHeader" runat="server" Text=""></asp:Label></h2>
                            <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                                <asp:Label ID="lbl_UtilityModalMessage" runat="server" Text=""></asp:Label>
                            </div>
                            <br />
                            <asp:TextBox ID="tb_UtilityModalEntry" CssClass="input" Style="width: 90%;" runat="server"></asp:TextBox><br />
                            <asp:Label ID="lbl_Error" CssClass="error" Visible="false" runat="server" />
                            <asp:CustomValidator ID="val_TextEntry" ControlToValidate="tb_UtilityModalEntry"
                                Display="Dynamic" runat="server"></asp:CustomValidator>
                            <asp:HiddenField ID="hdn_UtilityModalPurpose" Value="" runat="server" />
                            <asp:UpdateProgress ID="progress_Modal" runat="server" DisplayAfter="100">
                                <ProgressTemplate>
                                    <div class="searching" align="center">
                                        <asp:Label ID="lbl_ModalProgress" runat="server" Text="Working...."></asp:Label>
                                        <br />
                                        <br />
                                        <img src="Images/searching.gif" alt="" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div align="right" style="width: 95%; margin-top: 15px">
                            <asp:Button ID="bn_UtilityModalSave" runat="server" Text="Save" Width="50px" OnClick="bn_UtilityModalSave_Click"
                                CssClass="aspHoverButton" CausesValidation="false" />
                            <asp:Button ID="bn_UtilityModaClose" runat="server" Text="Cancel" Width="50px" OnClick="bn_UtilityModalCancel_Click"
                                BackColor="White" BorderColor="Silver" BorderWidth="1" CssClass="aspHoverButton"
                                CausesValidation="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
