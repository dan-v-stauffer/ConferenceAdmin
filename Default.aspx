<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 43px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="up_Form" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="menuTable" cellspacing="5px">
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        People:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_ManageAttendees" runat="server" OnClick="lb_ManageAttendees_Click">
                        <img src="Images/attendees.png" class="buttonImage" alt="Register KT Staff" />
                        <div>
                            Manage Attendees</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_RegisterKTStaff" runat="server" OnClick="lb_RegisterKTStaff_Click">
                        <img src="Images/staff.png" class="buttonImage" alt="Register KT Staff" />
                        <div>
                            Register<br />
                            KT Staff</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_AddInvitee" runat="server" OnClick="lb_AddInvitee_Click">
                        <img src="Images/add_invitee.png" class="buttonImage" alt="Add Invitee" />
                        <div>
                            Add
                            <br />
                            Invitee</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <img src="Images/vip.png" alt="Manage Vendors" />
                        <div>
                            Register
                            <br />
                            Guest Speaker</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/worker.png" alt="Manage Vendors" />
                        <div>
                            <asp:HyperLink ID="hl_RegExtStaff" NavigateUrl="~/RegisterExtStaff.aspx" runat="server">Register<br />External Staff</asp:HyperLink>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        Communications:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_SendInvitations" runat="server" OnClick="lb_SendGuestInvitations_Click">
                        <img src="Images/invite.jpg" class="buttonImage" alt="Send Invites" />
                        <div>
                            Send<br />
                            Guest Invitations</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_SendReminders" runat="server" OnClick="lb_SendReminders_Click">
                        <img src="Images/reminder.jpg" class="buttonImage" alt="Send Invites" />
                        <div>
                            Send<br />
                            Reminders</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_SendBatchConfirmations" runat="server" OnClick="lb_SendBatchConfirmations_Click">
                        <img src="Images/confirmation.png" class="buttonImage" alt="Send Invites" />
                        <div>
                            Send<br />
                            Confirmations</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_SendSurveyEmails" runat="server" OnClick="lb_SendSurveyEmails_Click">
                        <img src="Images/survey.jpg" class="buttonImage" alt="Send Invites" />
                        <div>
                            Send<br />
                            Survey Notices</div></asp:LinkButton>
                    </td>
                    <td class="menuCell">
                        <asp:LinkButton ID="lb_SendStaffInvitations" runat="server" OnClick="lb_SendStaffInvitations_Click">
                        <img src="Images/staff_invite.png" class="buttonImage" alt="Send Staff Invites" />
                        <div>
                            Send<br />
                            Staff Invitations</div></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        Events &amp; Scheduling:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <img src="Images/microphone.png" alt="Manage Vendors" />
                        <div>
                            Conference<br />
                            Metadata</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/hotel.png" alt="Manage Vendors" />
                        <div>
                            Manage<br />
                            Venue</div>
                    </td>
                    <td class="menuCell">
                        <asp:HyperLink ID="hl_ManageEvent" NavigateUrl="~/EventManager.aspx" runat="server">
                            <img src="Images/event.png" alt="Manage Agenda" style="border:none" />
                            <div>Manage Agenda</div>
                        </asp:HyperLink>
                    </td>
                    <td class="menuCell">
                        <img src="Images/ticket.png" alt="Allocate Events" />
                        <div>
                            Allocate<br />
                            Events</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/task.png" alt="Manage Vendors" />
                        <div>
                            <asp:LinkButton ID="lb_CreateSchedules" OnClick="lb_CreateSchedules_Click" runat="server">Create Staff Schedules</asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        Builders:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <img src="Images/registration.png" alt="Manage Vendors" />
                        <div>
                            Registration<br />
                            Builder</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/reports.png" alt="Manage Vendors" />
                        <div>
                            Report<br />
                            Builder</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/email.gif" alt="Manage Vendors" />
                        <div>
                            Email<br />
                            Builder</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/web.jpg" alt="Manage Vendors" />
                        <div>
                            Webpage<br />
                            Builder</div>
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        Data:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <img src="Images/reports.png" alt="Manage Vendors" />
                        <div>
                            Reports</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/crowdCompass.png" alt="Manage Vendors" />
                        <div>
                            Export Crowd
                            <br />
                            Compass Data</div>
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="font-weight: bold">
                        Social:
                    </td>
                </tr>
                <tr>
                    <td class="menuCell">
                        <img src="Images/twitter.jpg" alt="Manage Vendors" />
                        <div>
                            Twitter</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/facebook.jpg" alt="Manage Vendors" />
                        <div>
                            Facebook</div>
                    </td>
                    <td class="menuCell">
                        <img src="Images/pinterest.jpg" alt="Manage Vendors" />
                        <div>
                            Pinterest</div>
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                    <td class="menuCellEmpty">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="bn_ShowStaffModal" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modal_Staff" runat="server" TargetControlID="bn_ShowStaffModal"
        PopupControlID="pnl_StaffModal" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnl_StaffModal" runat="server" Width="500px" BackColor="White" BorderColor="Navy"
        BorderStyle="Solid" BorderWidth="2" Style="display: none; padding: 10px 10px 10px 10px">
        <asp:UpdatePanel ID="up_StaffModal" runat="server">
            <ContentTemplate>
                <div id="div_Body" style="display: table">
                    <h2>
                        <asp:Label ID="lbl_StaffModalHeader" runat="server" Text="Select a KT Staff Member to register/update:"></asp:Label></h2>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:LinkButton ID="lb_SendStaffInvites" runat="server" Enabled="false" OnClick="lb_SendStaffInvites_Click"
                            Text="Send Invites" />
                        <asp:DataList ID="dl_Staff" runat="server" RepeatDirection="Vertical" OnItemDataBound="dl_Staff_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="hl_Staff" runat="server" /></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul></FooterTemplate>
                        </asp:DataList>
                    </div>
                </div>
                <div align="right" style="width: 95%; margin-top: 15px">
                    <asp:Button ID="bn_SuccessModalOK" runat="server" Text="OK" Width="50px" OnClick="bn_SuccessModalOK_Click"
                        CssClass="aspHoverButton" CausesValidation="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="bn_ShowAttendeeModal" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modal_Attendee" runat="server" TargetControlID="bn_ShowAttendeeModal"
        PopupControlID="pnl_AttendeeModal" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnl_AttendeeModal" runat="server" Width="500px" BackColor="White"
        BorderColor="Navy" BorderStyle="Solid" BorderWidth="2" Style="display: none;
        padding: 10px 10px 10px 10px">
        <asp:UpdatePanel ID="up_AttendeeModal" runat="server">
            <ContentTemplate>
                <div id="div1" style="display: table">
                    <h2>
                        <asp:Label ID="lbl_SelectAttendee" runat="server" Text="Select a Conference Attendee to register/update:"></asp:Label></h2>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:DropDownList ID="ddl_DivisionSelect" Width="225" CssClass="input" runat="server">
                        </asp:DropDownList>
                        <asp:CascadingDropDown ID="cascex_Divisions" runat="server" TargetControlID="ddl_DivisionSelect"
                            Category="divisionText" PromptText="Select Division:" ServicePath="~/ConferenceWebService.asmx"
                            ServiceMethod="GetKTDivisions" />
                        <asp:DropDownList ID="ddl_AttendeeSelect" Width="225" CssClass="input" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_AttendeeSelect_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CascadingDropDown ID="cascex_VenueSpaces" runat="server" TargetControlID="ddl_AttendeeSelect"
                            Category="userLogin" PromptText="Select Attendee:" ParentControlID="ddl_DivisionSelect"
                            ServicePath="~/ConferenceWebService.asmx" ServiceMethod="GetDivisionAttendees" />
                    </div>
                </div>
                <div align="right" style="width: 95%; margin-top: 15px">
                    <asp:Button ID="Button2" runat="server" Text="OK" Width="50px" OnClick="bn_SuccessModalOK_Click"
                        CssClass="aspHoverButton" CausesValidation="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="bn_ShowSendEmailModal" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modal_SendEmail" runat="server" TargetControlID="bn_ShowSendEmailModal"
        PopupControlID="pnl_VerifySendEmails" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnl_VerifySendEmails" runat="server" Width="750px" BackColor="White"
        BorderColor="Navy" BorderStyle="Solid" BorderWidth="2" Style="display: none;
        padding: 10px 5px 10px 5px">
        <asp:UpdatePanel ID="up_UtilityModal" runat="server">
            <ContentTemplate>
                <div id="container" style="display: table">
                    <h2>
                        <asp:Label ID="lbl_SendEmailHeader" runat="server" Text=""></asp:Label></h2>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:Label ID="lbl_SendEmailMessage" runat="server" Text="" Width="650"></asp:Label>
                    </div>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:ListBox ID="lb_EmailRecipients" runat="server" Width="650"></asp:ListBox>
                    </div>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 80%; float: left">
                        <table>
                            <tr>
                                <td>
                                    Testing Only?
                                </td>
                                <td>
                                    <asp:CheckBox ID="cb_TestEmailsOnly" runat="server" Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="hdn_SendEmailPurpose" Value="" runat="server" />
                    <asp:UpdateProgress ID="progress_Modal" runat="server" DisplayAfter="100">
                        <ProgressTemplate>
                            <div class="Sending" align="center">
                                <asp:Label ID="lbl_ModalProgress" runat="server" Text="Sending...."></asp:Label>
                                <br />
                                <br />
                                <img src="Images/searching.gif" alt="" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <div align="right" style="width: 95%; margin-top: 15px; text-align: left;">
                    <asp:Button ID="bn_SendEmailClose" runat="server" Text="Cancel" Width="75px" OnClick="bn_SendEmailModalCancel_Click"
                        BackColor="White" BorderColor="Silver" BorderWidth="1" CssClass="aspHoverButton"
                        CausesValidation="false" />
                    <asp:Button ID="bn_SendEmailSave" runat="server" Text="OK" Width="75px" OnClick="bn_SendEmailModalSave_Click"
                        CssClass="aspHoverButton" CausesValidation="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Button ID="bn_AddInviteeModal" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modal_AddInvitee" runat="server" TargetControlID="bn_AddInviteeModal"
        PopupControlID="pnl_AddInviteeModal" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnl_AddInviteeModal" runat="server" Width="750px" BackColor="White"
        BorderColor="Navy" BorderStyle="Solid" BorderWidth="2" Style="display: none;
        padding: 10px 5px 10px 5px">
        <asp:UpdatePanel ID="up_AddInviteeModal" runat="server">
            <ContentTemplate>
                <div id="div2" style="display: table">
                    <h2>
                        <asp:Label ID="lbl_AddInvitee" runat="server" Text="Add invitee email address to register/update:"></asp:Label></h2>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        Select Conference:
                    </div>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:DropDownList ID="ddl_ConferenceSelect" Width="225" CssClass="input" runat="server"
                            OnSelectedIndexChanged="ddl_ConferenceSelect_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                            Select Division:
                        </div>
                        <asp:DropDownList ID="ddl_Divisions" Width="225" CssClass="input" runat="server" />
                    </div>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        Enter Email:
                    </div>
                    <asp:TextBox ID="tb_InviteeEmail" Width="225" runat="server"></asp:TextBox>
                </div>
                <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                    Executive?:
                </div>
                <asp:CheckBox ID="cb_IsExec" runat="server" />
                </div>
                <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                    Select Invite Type:
                </div>
                <asp:DropDownList ID="ddl_InviteType" Width="225" CssClass="input" runat="server" />
                </div> </div>
                <div align="right" style="width: 95%; margin-top: 15px">
                    <asp:Button ID="bn_AddInvitee" runat="server" Text="OK" Width="50px" OnClick="bn_AddInviteeOK_Click"
                        CssClass="aspHoverButton" CausesValidation="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
