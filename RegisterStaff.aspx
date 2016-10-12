<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RegisterStaff.aspx.cs" Inherits="RegisterStaff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">

    <asp:UpdatePanel ID="up_Body" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
        <ContentTemplate>

    <asp:Panel runat="server" ID="rsvp_form" Width="1075" Style="margin-top: 10px; border: 1px solid silver;">
        <table>
            <tr>
                <td colspan="2">
                    <div style="float: left; margin-right: 10px">
                        <h2>
                            Registration</h2>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right: 5px; padding-left: 5px;">
                    <h3>
                        <b>Personal Information</b></h3>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 30px">
                    Name<span class="reqdNote">*</span>:
                </td>
                <td class="rsvpForm_RightColumn">
                    <div style="float: left; width: 800px">
                        <table>
                            <tr>
                                <td style="padding-left: 5px">
                                    First
                                </td>
                                <td style="padding-left: 5px">
                                    Last
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="up_FirstName" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="tb_FirstName" runat="server" Width="250" CssClass="input" Text=""
                                                ClientIDMode="Static" onblur="__doPostBack(this.id,'OnBlur');" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="up_LastName" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="tb_LastName" runat="server" Width="250" CssClass="input" Text=""
                                                ClientIDMode="Static" onblur="__doPostBack(this.id,'OnBlur');" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    (As you want your name to appear on your conference badge.)
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="val_Name" runat="server" InitialValue="" Display="Dynamic"
                            CssClass="validationMsg" ControlToValidate="tb_FirstName" ErrorMessage="Name*: This is a required field. We have to know your name so we can print name tags!"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblName" runat="server" CssClass="hidden" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 8px">
                    Mobile Phone:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:UpdatePanel ID="up_MobilePhone" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="tb_MobilePhone" CssClass="input" runat="server" Width="250" ClientIDMode="Static"
                                onblur="__doPostBack(this.id,'OnBlur');">
                            </asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 18px">
                    Shirt Size<span class="reqdNote">*</span>:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:UpdatePanel runat="server" ID="up_ShirtInfo" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rb_ShirtType" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cbo_ShirtSize" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="floaterL">
                                <asp:RadioButtonList ID="rb_ShirtType" runat="server" OnSelectedIndexChanged="rb_ShirtType_SelectedIndexChanged"
                                    AutoPostBack="true" RepeatDirection="Vertical" RepeatLayout="Flow">
                                    <asp:ListItem Text="Men's" Value="M" Selected="True" />
                                    <asp:ListItem Text="Women's" Value="W" />
                                </asp:RadioButtonList>
                            </div>
                            <div class="floaterLWide">
                                <asp:ComboBox ID="cbo_ShirtSize" Width="125" Font-Names="Century Gothic" ForeColor="#696969"
                                    OnSelectedIndexChanged="cbo_ShirtSize_SelectedIndexChanged" AutoPostBack="true"
                                    ItemInsertLocation="OrdinalText" CssClass="WindowsStyle" AutoCompleteMode="SuggestAppend"
                                    DropDownStyle="DropDownList" runat="server">
                                    <asp:ListItem Text="Please select:" Value="Please select:" Selected="True" />
                                    <asp:ListItem Text="Small" Value="S" />
                                    <asp:ListItem Text="Medium" Value="M" />
                                    <asp:ListItem Text="Large" Value="L" />
                                    <asp:ListItem Text="X-Large" Value="XL" />
                                    <asp:ListItem Text="XX-Large" Value="XXL" />
                                    <asp:ListItem Text="XXX-Large" Value="XXXL" />
                                </asp:ComboBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="width: 100%">
                        <asp:RequiredFieldValidator ID="val_ShirtSize" runat="server" InitialValue="Please select:"
                            Display="Dynamic" CssClass="validationMsg" val_field="Shirt Size: " ControlToValidate="cbo_ShirtSize"
                            ErrorMessage="Shirt Size*: This is a required field. It's a great shirt- you'll want it to fit!"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right: 5px; padding-left: 5px;">
                    <div style="width: 100%; border-top: 1px solid silver; margin-bottom: 5px;" />
                    <h3>
                        <b>Hospitality and Catering</b></h3>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 14px">
                    Hotel Reservations:
                </td>
                <td class="rsvpForm_RightColumn" style="width: 800px">
                    <div class="floaterL">
                        <asp:UpdatePanel ID="up_HotelReservations" runat="server">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="cbl_HotelReservations" CssClass="checkBox" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="cbl_HotelReservations_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="float: left; width: 600px; margin-top: 7px; margin-left: 20px;">
                        <span>NOTE: Conference administrators will make your reservations for the nights selected.
                            <u><i>Please do not contact the hotel directly.</i></u></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 12px">
                    Transportation:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:UpdatePanel runat="server" ID="up_Transportation" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbl_MonteryInbound" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbl_MonteryOutbound" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="mealDatesItem">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="width: 325px; float: left; margin: 5px 5px 5px 5px">
                                                <b>From KT Milpitas to Hyatt Regency Monterey:</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 325px; vertical-align: top; height: 85px">
                                                <asp:UpdatePanel ID="up_MontereyInbound" runat="server">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rbl_MonteryInbound" Direction="Inbound" runat="server" SelectedIndexChanged="rbl_TransporationOptions_SelectedIndexChanged"
                                                            CausesValidation="false" RepeatDirection="Vertical" RepeatLayout="Flow" AutoPostBack="true">
                                                        </asp:RadioButtonList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: bottom">
                                            <div style="width: 325px; vertical-align: top; height: 85px; padding-bottom: 5px">
                                                <i>
                                                    <asp:Label ID="lbl_DepartLocationInbound" runat="server" /></i>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="mealDatesItem">
                                <table>
                                    <tr>
                                        <td>
                                            <div style="width: 325px; float: left; margin: 5px 5px 5px 5px">
                                                <b>From Hyatt Regency Monterey to KT Milpitas:</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 325px; vertical-align: top; height: 85px;">
                                                <asp:RadioButtonList ID="rbl_MonteryOutbound" Direction="Outbound" runat="server"
                                                    CausesValidation="false" Width="280" SelectedIndexChanged="rbl_TransporationOptions_SelectedIndexChanged"
                                                    RepeatDirection="Vertical" RepeatLayout="Flow" AutoPostBack="true">
                                                </asp:RadioButtonList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 325px; vertical-align: top; height: 85px; margin-bottom: 5px">
                                                <i>
                                                    <asp:Label ID="lbl_DepartLocationOutbound" runat="server" /></i>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn" style="padding-top: 12px">
                    Meal Choices:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:DataList ID="dl_MealDates" runat="server" RepeatDirection="Horizontal" RepeatColumns="7">
                        <ItemTemplate>
                            <div class="mealDatesItem">
                                <div style="margin-bottom: 5px">
                                    <asp:Label ID="lbl_MealDate" Font-Bold="true" runat="server" /><br />
                                </div>

<%--                                  <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rbl_SingleMealChoice" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="cbo_MultipleMealChoices" EventName="SelectedIndexChanged" />
                                                        </Triggers>
--%>
                                <asp:UpdatePanel ID="up_MealOptions" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:DataList ID="dl_MealsOnDate" runat="server" RepeatDirection="Vertical">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td style="width: 70px; text-align: right; margin-right: 5px">
                                                            <asp:Label ID="lbl_MealType" runat="server" />
                                                        </td>
                                                        <td> 
<%--                                                            <asp:UpdatePanel ID="up_rbl_SingleMealChoice" runat="server" UpdateMode="Conditional">
                                                            <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rbl_SingleMealChoice"  EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                            --%>
                                                            <asp:Panel ID="pnl_SingleMealChoice" Visible="false" runat="server">
                                                            
                                                                <asp:RadioButtonList ID="rbl_SingleMealChoice" runat="server" AutoPostBack="true" ClientIDMode="AutoID"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rbl_SingleMealChoice_SelectedIndexChanged"
                                                                    Visible="false">
                                                                    <asp:ListItem Selected="True" Text="Yes" Value="True"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="No" Value="False"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </asp:Panel>
                                                          <%--  </ContentTemplate>
                                                            </asp:UpdatePanel>--%>
                                                            <asp:Panel ID="pnl_MultipleMealChoices" Visible="false" runat="server">
                                                                <asp:ComboBox ID="cbo_MultipleMealChoices" Width="135" Visible="false" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="cbo_MultipleMealChoices_SelectedIndexChanged" ItemInsertLocation="OrdinalText"
                                                                    CssClass="WindowsStyle" AutoCompleteMode="Suggest" runat="server">
                                                                </asp:ComboBox>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn">
                    Food Allergies or Restrictions:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:UpdatePanel ID="up_FoodRestrictions" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="tb_FoodRestrictions" CssClass="input" Width="725" TextMode="MultiLine"
                                ClientIDMode="Static" onblur="__doPostBack(this.id,'OnBlur');" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="rsvpForm_LeftColumn">
                    Special Requirements or Needs:
                </td>
                <td class="rsvpForm_RightColumn">
                    <asp:UpdatePanel ID="up_SpecialNeeds" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="tb_SpecialNeeds" CssClass="input" Width="725" TextMode="MultiLine"
                                runat="server" ClientIDMode="Static" onblur="__doPostBack(this.id,'OnBlur');" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right: 35px; vertical-align: top">
                    <asp:Button ID="bn_Dummy" Height="35" Width="100" Style="display: none" CausesValidation="false"
                        runat="server" />
                    <div class="bnFormButtons">
                        <asp:Button ID="bn_Cancel" Height="35" Width="100" CssClass="aspHoverButton" CausesValidation="false"
                            Text="Cancel" runat="server" OnClick="bn_Cancel_Click" />
                    </div>
                    <div class="bnFormButtons">
                        <asp:Button ID="bn_SubmitRSVP" Height="35" Width="100" CssClass="aspHoverButton"
                            CausesValidation="false" Text="Submit RSVP" runat="server" OnClick="bn_SubmitRSVP_Click" />
                    </div>
                    <div style="float: right; margin-right: 10px">
                        <asp:UpdateProgress ID="progress_Submit" runat="server" DisplayAfter="100">
                            <ProgressTemplate>
                                <div align="left">
                                    Working....<br />
                                    <img src="Images/searching.gif" alt="" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <asp:UpdatePanel ID="up_ExecAdmin" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_ExecAdmin" runat="server" Width="375" CssClass="hidden">
                                <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <div style="width: 125px; float: left; text-align: center">
                                                First Name</div>
                                            <div style="width: 125px; float: left; margin-left: 3px; text-align: center">
                                                Last Name</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100px">
                                                Admin Name:</div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_ExecAdminFirstName" runat="server" Text="" CssClass="input" Width="123"
                                                Style="float: left" />
                                            <asp:TextBox ID="tb_ExecAdminLastName" runat="server" Text="" CssClass="input" Width="124"
                                                Style="margin-left: 3px; float: left" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Admin Email:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_ExecAdminEmail" runat="server" Text="" CssClass="input" Width="253" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Admin Phone:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tb_ExecAdminPhone" runat="server" Text="" CssClass="input" Width="253" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
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

    <asp:Button ID="bn_ShowSuccessModal" runat="server" Style="display: none" />
    <asp:ModalPopupExtender ID="modal_Success" runat="server" TargetControlID="bn_ShowSuccessModal"
        PopupControlID="pnl_SuccessModal" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnl_SuccessModal" runat="server" Width="500px" BackColor="White" BorderColor="Navy"
        BorderStyle="Solid" BorderWidth="2" Style="display: none; padding: 10px 10px 10px 10px">
        <asp:UpdatePanel ID="up_SuccessModal" runat="server">
            <ContentTemplate>
                <div id="div_Body" style="display: table">
                    <h2>
                        <asp:Label ID="lbl_SuccessMdgHeader" runat="server" Text=""></asp:Label></h2>
                    <div style="margin-top: 10px; margin-bottom: 5px; width: 95%">
                        <asp:Label ID="lbl_SuccessMsg" runat="server" Text=""></asp:Label>
                    </div>
                  
                </div>
                <div align="right" style="width: 95%; margin-top: 15px">
                    <asp:Button ID="bn_SuccessModalOK" runat="server" Text="OK" Width="50px" OnClick="bn_SuccessModalOK_Click"
                        CssClass="aspHoverButton" CausesValidation="false" />
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <%--        </ContentTemplate>
    </asp:UpdatePanel>
    --%>
</asp:Content>