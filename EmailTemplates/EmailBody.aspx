<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailBody.aspx.cs" Inherits="EmailBody" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Engineering Conference 2015</title>
    <style type="text/css">
/* DEFAULTS
----------------------------------------------------------*/

body   
{
/*    background: #b6b7bc; */
    font-size: .80em;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    margin: 0px;
    padding: 0px;
    color: #696969;
}

a:link, a:visited
{
    color: #034af3;
}

a:hover
{
    color: #1d60ff;
    text-decoration: none;
}

a:active
{
    color: #034af3;
}

p
{
    margin-bottom: 10px;
    line-height: 1.6em;
}


/* HEADINGS   
----------------------------------------------------------*/

h1, h2, h3, h4, h5, h6
{
    font-size: 1.5em;
    color: #666666;
    font-variant: small-caps;
    text-transform: none;
    font-weight: 200;
    margin-bottom: 0px;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;

}

h1
{
    font-size: 2.0em;
    padding-bottom: 0px;
    margin-bottom: 0px;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    font-weight:bold;
}

h2
{
    font-size: 1.5em;
    font-weight: 600;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
}

h3
{
    font-size: 1.2em;
}

h4
{
    font-size: 1.1em;
}

h5, h6
{
    font-size: 1em;
}

/* this rule styles <h1> and <h2> tags that are the 
first child of the left and right table columns */
.rightColumn > h1, .rightColumn > h2, .leftColumn > h1, .leftColumn > h2
{
    margin-top: 0px;
}


/* PRIMARY LAYOUT ELEMENTS   
----------------------------------------------------------*/

.page
{
    background-color: #fff;
    margin: 20px auto 0px auto;
}

.header
{
    position: relative;
    margin: 0px;
    padding: 0px;
    background-image: url(../Images/RSVP_Header.png);
    
    width: 100%;
    height:244px;
}

.header h1
{
    font-weight: 700;
    margin-top: 180px;
    padding: 0px 0px 0px 20px;
    color: #f9f9f9;
    border: none;
    line-height: 2em;
    font-size: 2em;
}

.main
{
    padding: 0px 12px;
    margin: 12px 8px 8px 8px;
    min-height: 420px;
}

.leftCol
{
    padding: 6px 0px;
    margin: 12px 8px 8px 8px;
    width: 200px;
    min-height: 200px;
}

.footer
{
    color: #4e5766;
    margin: 0px auto;
    text-align: center;
    line-height: normal;
}


/* TAB MENU   
----------------------------------------------------------*/

div.hideSkiplink
{
    background-color:#3a4f63;
    width:100%;
}

div.menu
{
    padding: 4px 0px 4px 8px;
}

div.menu ul
{
    list-style: none;
    margin: 0px;
    padding: 0px;
    width: auto;
}

div.menu ul li a, div.menu ul li a:visited
{
    background-color: #465c71;
    border: 1px #4e667d solid;
    color: #dde4ec;
    display: block;
    line-height: 1.35em;
    padding: 4px 20px;
    text-decoration: none;
    white-space: nowrap;
}

div.menu ul li a:hover
{
    background-color: #bfcbd6;
    color: #465c71;
    text-decoration: none;
}

div.menu ul li a:active
{
    background-color: #465c71;
    color: #cfdbe6;
    text-decoration: none;
}

/* FORM ELEMENTS   
----------------------------------------------------------*/

fieldset
{
    margin: 1em 0px;
    padding: 1em;
    border: 1px solid #ccc;
}

fieldset p 
{
    margin: 2px 12px 10px 10px;
}

fieldset.login label, fieldset.register label, fieldset.changePassword label
{
    display: block;
}

fieldset label.inline 
{
    display: inline;
}

legend 
{
    font-size: 1.1em;
    font-weight: 600;
    padding: 2px 4px 8px 4px;
}

input.textEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

input.passwordEntry 
{
    width: 320px;
    border: 1px solid #ccc;
}

div.accountInfo
{
    width: 42%;
}


/* MISC  
----------------------------------------------------------*/

.clear
{
    clear: both;
}

.title
{
    display: block;
    float: left;
    text-align: left;
    width: auto;
}

.loginDisplay
{
    font-size: 1.1em;
    display: block;
    text-align: right;
    padding: 10px;
    color: White;
}

.loginDisplay a:link
{
    color: white;
}

.loginDisplay a:visited
{
    color: white;
}

.loginDisplay a:hover
{
    color: white;
}

.failureNotification
{
    font-size: 1.2em;
    color: Red;
}

.bold
{
    font-weight: bold;
}

.submitButton
{
    text-align: right;
    padding-right: 10px;
}

.dropdown
{
    border:1px solid silver;
    height:30px;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    color: #696969;
}

.sortButtonDiv
{
    float:right; 
    width:12px; 
    margin-left:5px; 
    margin-top:2px;
    margin-top:9px; 
    margin-bottom:9px;
}
.linkButtonDiv
{
    float:left; 
    margin-top:7px; 
    margin-bottom:7px;
}

.itemData
{
     border:1px solid silver;
}

.sortasc
{
 display:block; padding:0 4px 0 15px; 
  background:url(../Images/asc.gif) no-repeat;  
 }

 .sortdesc
 {
     display:block; padding:0 4px 0 15px; 
    background:url(../Images/desc.gif) no-repeat;
}

.sortasc-header A { background:url(../Images/asc.gif) right center no-repeat; }
.sortdesc-header A { background:url(../Images/desc.gif) right center no-repeat; }

.rounded-corners {
     -moz-border-radius: 8px;
    -webkit-border-radius: 8px;
    -khtml-border-radius: 8px;
    border-radius: 8px;
}

.hidden
{
    display:none;
}

.invisible
{
    visibility:hidden;
}

.headerTable
{
    width:600px;
}

.headerTable .leftColumn
{
    text-align: right; 
    padding-right: 15px; 
    padding-bottom: 8px; 
    vertical-align: top;
    width:140px;
}
.headerTable .rightColumn
{
    text-align: left; 
    padding-left: 15px; 
    vertical-align: top;
    width:600px;
}

.headerTable .mergedColumn
{
}

.headerTable .mergedRow
{
     padding-left: 5px;
}

.rsvpForm_LeftColumn
{
    text-align:right;
    padding-right:15px;
    padding-bottom:15px;
    padding-top:5px;    
    vertical-align:top;
    width:150px;
}

.rsvpForm_RightColumn
{
    text-align:left;
    padding-left:5px;
    padding-bottom:15px;
    padding-top:5px;    
    vertical-align:middle;
    width:950px;
}

.floaterL
{
    float:left;
}
.floaterLWide
{
    float:left;
    margin-left:15px;
    vertical-align:top;
}
.floaterR
{
    float:right;
}
.hyphen
{
    float:left;
    margin-left:3px;
    margin-right:3px;
}

.WindowsStyle .ajax__combobox_itemlist
{
    color: #696969;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    position:inherit !important; 
}
    
.WindowsStyle .ajax__combobox_inputcontainer .ajax__combobox_textboxcontainer input
{
    margin: 0;
    border: solid 1px #7F9DB9;
    border-right: 0px none;
    padding: 1px 0px 0px 5px;
    font-size: 13px;
    height: 18px;
    position: relative;       
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;

}
    
.WindowsStyle .ajax__combobox_inputcontainer .ajax__combobox_buttoncontainer button
{
    margin: 0;
    padding: 0;
    background-image: url(../Images/asc.gif);
    background-position: top left;
    border: 0px none;
    height: 21px;
    width: 21px;
    border: solid 1px #7F9DB9;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;

}
    
.dropDownList
{    
    border: 1px solid #7F9DB9;
    color: #696969;  
    background-image: url(../Images/asc.gif) !important; 
    background-position:top right;  
    background-repeat:no-repeat;  
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
}    

.input
{
    border: 1px solid #7F9DB9;
    color: #696969;  
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    margin-left:5px;
}

.checkBox label
{
    margin-left:5px;
}

.mealDatesItem
{
    border: 1px solid #7F9DB9;
    margin: 5px 5px 5px 5px;
    padding: 3px 3px 3px 3px;
    vertical-align:top;
    height:150px;
    width: 350px;
    font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
    float:left;
}

.cboContainer
{
    margin-left:5px;
}

.customSelectDiv
{
border: 1px solid #7F9DB9;
}
   
.customSelect
{
margin-left:5px;
font-size:small;
height:20px;
//border: 0px solid white;
border-radius: 0;
-webkit-appearance: none;
font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;

}

.selectContainerStyled {
  position: relative;
  border: 1px solid #b5b5b5;
  border-radius: 4px;
  background-clip: padding-box;
  background-color: #f1f1f1;
  display:inline-block;
  box-shadow: 0 1px 3px rgba(0,0,0,.13), inset 0 1px 0 #fff;
  color: 0 1px 3px rgba(0,0,0,.13), inset 0 1px 0 #fff;
  background-image: linear-gradient(bottom, #dcdcdc 0%, #dcdcdc 2%, #f3f3f3 100%);
}
.selectContainerStyled select {
  float: left;
  position: relative;
  z-index: 2;
  height: 30px;
  display: block;
  line-height: 14px;
  padding: 5px 25px 4px 5px;
  margin: 0;
  -moz-appearance: window;
  -moz-padding-end: 19px;
  background: transparent;
  background-color: transparent;
  border: none;
  -webkit-appearance: none;
  appearance: none;
}

.itemWithDetail:hover {
    text-decoration: underline;
    color:Blue;
    cursor:default;
}

.horizontalRadioButtonList input
{
    margin-right:15px;
    margin-left:15px;
}

.techPanelChoiceLabels
{
    margin-left:11px;
}


.bnFormButtons
{
    float:right;
    margin: 5px 5px 5px 5px; 
    border:1px solid #7F9DB9;
}

.bnFormButtons: hover
{
   background-color:#00FF99;
}

 .modalBackground {
            background-color:Gray;
            filter:alpha(opacity=70);
            opacity:0.7;
        }
        
.paperItem
{
    width: 145px; 
    height: 150px; 
    border: 1px solid silver; 
    padding: 5px; 
    vertical-align: top;
}
        
.paperOptionBox
{
float: right; 
width: 145px; 
height:50px;
display: block; 
vertical-align:bottom !important;
}

.paperOptionsLabelDiv
{
    float: right; 
    width: 145px; 
    text-align: right;
    background-color
}

.paperRoomHeaderCell
{
    vertical-align: middle; 
    width: 145px; 
    text-align: center
}

.paperRooomHeaderText
{
    width:163px;
}

.paperSymposiumDiv
{
    border:1px solid silver;
    padding-top:5px;
    padding-bottom:5px;
}

.firstChoiceKey
{
    float:left; 
    width:10px;
    height:10px; 
    background-color: #98FB98; 
    border: 1px solid silver; 
    margin-right:5px;
    margin-top:5px;
    margin-left:5px;
}

.secondChoiceKey
{
    float:left; 
    width:10px;
    height:10px; 
    background-color: #D5D8F5; 
    border: 1px solid silver; 
    margin-right:8px;
    margin-top:5px;
    margin-left:5px;
}

.thirdChoiceKey
{
    float:left; 
    width:10px;
    height:10px; 
    background-color: #FFE4B5; 
    border: 1px solid silver; 
    margin-right:8px;
    margin-top:5px;
    margin-left:5px;
}

.error
{
    color: Red;
    font-style:italic;
}

    .searching
    {
        font-family: Century Gothic, "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        border:1px solid silver;
        width: 175px;
        height: 75px;
        background-color: White;
        z-index: 999;
        margin: auto;
        position:absolute; 
        top:50%; 
        margin-top:-38px;
        left:50%;
        margin-left:-90px;
    }
    
.validationMsg
{
    margin-left:10px; 
    color:Red; 
    font-style:italic
}

.TechPanelItem:hover
{
    text-decoration:underline;
}

.execAdmin
{
    float: left; 
    margin-left:123px; 
    width:350px;
    margin-right: 15px; border: 1px solid silver;
}

.reqdNote
{
    color:Red;
}

.CheckBoxChecked
{
    width:21px;
    height:21px;
    background-image:url(../Images/CheckBox_Checked.png);
}
.CheckBoxClear
{
    width:21px;
    height:21px;
    background-image:url(../Images/CheckBox_Clear.png);
}
.successIcon
{
        width:75px;
    height:75px;
    background-image:url(../Images/green_checkmark.png);
}
.successMsg
{
    color: #173B0B;
}
.successHeader
{
    width: 100%; 
    background-color: #9FF781;
    border:1px solid #173B0B;
    padding-top:10px;
    padding-bottom:10px;
}
.errorIcon
{
        width:75px;
    height:75px;
    background-image:url(../Images/error.png);
}
.errorMsg
{
    color: #610B0B;
}
.errorHeader
{
    width: 100%; 
    background-color:#F5A9A9;
        border:1px solid #610B0B;
            padding-top:10px;
    padding-bottom:10px;


}

.h2Confirmation
{   font-size: 1.5em;
    font-variant: small-caps;
    text-transform: none;
    font-weight:  bold;
    margin-bottom: 0px;
}

.iconCol
{
    width:30px;
    text-align:center;
    vertical-align:middle;
}

.comboBoxInsideModalPopup
{
    position: relative;
}
.comboBoxInsideModalPopup ul
{
    position: absolute !important;
    left: 2px !important;
    top: 22px !important;
}    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        {imgheader}
        <div class="main">
            <div style="margin-bottom: 10px">
                <h1>
                    Engineering Conference 2015 - Registration Confirmation</h1>
            </div>
            <table style="width: 800px">
                <tr>
                    <td valign="top">
                    <div style="width:800px">
                        <div class="floaterL">
                            <h2>
                                <asp:Label ID="lbl_ConfirmationCode" runat="server" />
                            </h2>
                        </div></div>
                        <asp:Repeater ID="repeater_ConfMetaData" runat="server" OnItemDataBound="repeater_ConfMetaData_OnItemDataBound">
                            <HeaderTemplate>
                                <table class="headerTable">
                                    <tr>
                                        <td class="mergedColumn" colspan="2">
                                            <h2>
                                                <b>Dates & Location</b></h2>
                                        </td>
                                        <td class="mergedRow" rowspan="6">
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftColumn">
                                        Conference Dates:
                                    </td>
                                    <td class="rightColumn">
                                        <asp:Label ID="lbl_ConfStart" CssClass="floaterL" runat="server" Text="" />
                                        <div class="hyphen">
                                            -</div>
                                        <asp:Label ID="lbl_ConfStop" CssClass="floaterL" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftColumn">
                                        Venue:
                                    </td>
                                    <td class="rightColumn">
                                        <div class="floaterL">
                                            <asp:HyperLink ID="hl_VenueLink" runat="server" Target="_blank" />
                                            <br />
                                            <asp:Label ID="lbl_VenueAddress" runat="server" />
                                            <br />
                                            <div class="floaterL" style="margin-top: 2px;">
                                                <asp:HyperLink ID="hl_VenueMapLink" runat="server" CssClass="floaterL" Target="_blank"><table>
                                <tr><td></td><td> <div class="floaterL" style="width:350px; text-decoration:underline">Map and Directions</div></td></tr>
                                </table> </asp:HyperLink>
                                            </div>
                                        </div>
                                        <div class="floaterLWide">
                                            <%--                <img src="Images/GoogleMap.png" style="border:1px solid silver" />
                                            --%>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftColumn">
                                        Welcome Reception:
                                    </td>
                                    <td class="rightColumn">
                                        <asp:Label ID="lbl_WelcomeReceptionStart" CssClass="floaterL" runat="server" Text="" />
                                        <div class="hyphen">
                                            -</div>
                                        <asp:Label ID="lbl_WelcomeReceptionStop" CssClass="floaterL" runat="server" Text="" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td valign="top">
                        <asp:Panel runat="server" ID="pnl_POC" Style="width: 225px; height: 225px; z-index: 1">
                            <asp:DataList ID="dl_ContactInfo" runat="server" DataSourceID="sqlds_POCs">
                                <HeaderTemplate>
                                    <h2>
                                        Contact Information</h2>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="margin-top: 4px">
                                        <asp:Label runat="server" ID="lbl_POCName" Font-Bold="true" Text='<%# Eval("name") %>' /><br />
                                        <asp:Label runat="server" ID="lbl_Phone" Text='<%# Eval("userWorkPhone") %>' /><br />
                                        <asp:HyperLink ID="bl_EmailPOC" Text='<%# "Email " + Eval("userFirstName") %>' NavigateUrl='<%# "mailto:" + Eval("userEmail") %>'
                                            runat="server"></asp:HyperLink></div>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <div style="height: 4px">
                                    </div>
                                </SeparatorTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="sqlds_POCs" runat="server" ConnectionString="<%$ ConnectionStrings:EngConferenceDB %>"
                                SelectCommand="sp_GetConferencePOCs" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="1" Name="conferenceID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <div style="text-align: left">
            </div>
            <asp:Panel runat="server" ID="rsvp_form" Style="margin-top: 10px;">
                <table style="width: 1100px;">
                    <tr>
                        <td colspan="2" style="padding-right: 5px; padding-left: 5px;">
                            <h2>
                                <b>Personal Information</b></h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 5px">
                            Name:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_FullName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 5px">
                            Division:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_Division" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn">
                            Work Location:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_WorkLocation" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 5px">
                            Job Role:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_JobRole" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 5px">
                            Mobile Phone:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_MobilePhone" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 5px">
                            Shirt Size:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_ShirtSize" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 5px; padding-left: 5px;">
                            <div style="width: 630px; border-top: 1px solid silver; margin-bottom: 5px;" />
                            <h2>
                                <b>Hospitality and Catering</b></h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn">
                            Attending Sunday<br />
                            Evening Reception?
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:CheckBox ID="cb_Reception" runat="server" Checked="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 10px">
                            Interested in Sunday Golf?
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:CheckBox ID="cb_Golfing" runat="server" Checked="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 10px">
                            Hotel Reservations:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        Checking In:
                                    </td>
                                    <td style="width: 540px">
                                        <asp:Label ID="lbl_HotelCheckin" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Checking Out:
                                    </td>
                                    <td style="width: 540px">
                                        <asp:Label ID="lbl_HotelCheckout" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        Total Nights:
                                    </td>
                                    <td style="width: 540px">
                                        <asp:Label ID="lbl_HotelDuration" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="float: left; margin-top: 7px; width: 510px">
                                            <span>NOTE: Conference administrators will make your reservations for the nights selected.
                                                <u><i>Please do not contact the hotel directly.</i></u></span></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 12px">
                            Transportation:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <table>
                                <tr>
                                    <td style="width: 250px">
                                        <div style="width: 250px; float: left; margin: 5px 5px 5px 5px">
                                            KT Milpitas to Hyatt Regency Monterey:
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_FromKT" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 250px">
                                        <div style="width: 250px; float: left; margin: 5px 5px 5px 5px">
                                        Hyatt Regency Monterey to KT Milpitas:
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ToKT" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 6px">
                            Meal Choices:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:DataList ID="dl_MealDates" runat="server" RepeatDirection="Vertical" OnItemDataBound="dl_MealDates_ItemDataBound">
                                <ItemTemplate>
                                    <div>
                                        <div style="margin-bottom: 5px">
                                            <asp:Label ID="lbl_MealDate" runat="server" Font-Bold="true" /><br />
                                        </div>
                                        <asp:DataList ID="dl_MealsOnDate" runat="server" RepeatDirection="Vertical" OnItemDataBound="dl_MealsOnDate_ItemDataBound">
                                            <ItemTemplate>
                                                <table style="margin-bottom: 5px">
                                                    <tr>
                                                        <td style="width: 70px; text-align: left; margin-right: 5px">
                                                            <asp:Label ID="lbl_MealType" runat="server" />
                                                        </td>
                                                        <td style="width: 100px; margin-left: 5px;">
                                                            <asp:Label ID="lbl_MealChoice" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
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
                            <asp:Label ID="lbl_FoodRestrictions" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn">
                            Special Requirements or Needs:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:Label ID="lbl_SpecialNeeds" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-right: 5px; padding-left: 5px;">
                            <div style="width: 630px; border-top: 1px solid silver; margin-bottom: 5px;" />
                            <h2>
                                <b>Conference Participation Requests</b></h2>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn">
                            Sunday Deep Learning Tutorial:
                        </td>
                        <td class="rsvpForm_RightColumn">
                        <asp:CheckBox ID="cb_SundayDeepLearning" runat="server" Checked="false" />
                         <asp:HyperLink ID="hl_DeepLearningCalendar" runat="server" 
                                        NavigateUrl="http://collaboration.kla-tencor.com/EngineeringConference/SiteAssets/SundayDeepLearningTutorial.ics" Text="Add to your calendar"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn">
                            Technical Panels:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:DataList ID="dl_TechPanelsList" OnItemDataBound="dl_TechPanelsList_ItemDataBound"
                                RepeatDirection="Vertical" runat="server">
                                <HeaderTemplate>
                                    <div style="width: 510px; margin-left: 6px; margin-bottom: 5px">
                                        <i>Please Note: We'll strive to get you into your top choice, but seats will be allocated
                                            on a 'first-come, first-served' basis.)</i></div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server" id="div_TechPanelItem" style="margin-left: 6px">
                                        <table style="border-collapse: collapse; width: 530px">
                                            <tr>
                                                <td valign="top" style="width: 30px; border: 1px solid silver; padding: 5px">
                                                    <asp:Label ID="lbl_TechPanelChoice" runat="server" />
                                                </td>
                                                <td valign="top" style="width: 500px; border: 1px solid silver; padding: 5px">
                                                    <div>
                                                        <asp:Label ID="lbl_TechPanelTitle" CssClass="floaterL" runat="server" /></div>
                                                    <div>
                                                        <asp:Label ID="lbl_TechPanelDetail" runat="server" CssClass="TechPanelItem" Style="margin-left: 5px;
                                                            font-size: smaller" Text="(Detail)" />
                                                    </div>
                                                    <div>
                                                        <i>
                                                            <asp:Label ID="lbl_Moderator" runat="server" /></i></div>
                                                    <asp:Panel ID="pnl_ModOrMember" CssClass="hidden" runat="server">
                                                        <asp:Label ID="lbl_ModOrMember" CssClass="floaterL" Text="" runat="server" />
                                                        <i>
                                                            <asp:Label ID="lbl_TechPanelTopic" CssClass="hidden" runat="server" />
                                                        </i>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td class="rsvpForm_LeftColumn" style="padding-top: 10px">
                            Paper Symposia:
                        </td>
                        <td class="rsvpForm_RightColumn">
                            <asp:DataList ID="dl_PaperSymposiaList" Layout="Flow" OnItemDataBound="dl_PaperSymposiaList_ItemDataBound"
                                CellSpacing="5" RepeatDirection="Vertical" runat="server">
                                <HeaderTemplate>
                                    <div style="float: left; width: 510px">
                                        <i>Please Note: We'll strive to get you into your top choices, but seats will be allocated
                                            on a 'first-come, first-served' basis.</i></div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <b>
                                                    <asp:Label ID="lbl_SymposiaGroupTitle" runat="server" /><br />
                                                    <asp:Label ID="lbl_SymposiaGroupTimes" runat="server" />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:DataList ID="dl_PaperTimeSlots" runat="server" OnItemDataBound="dl_PaperTimeSlots_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table style="border-collapse: collapse; padding: 5px; width: 530px">
                                                            <tr>
                                                                <td valign="top" style="width: 65px; padding: 5px; padding-top: 8px; border: 1px solid silver">
                                                                    <asp:Label ID="lbl_PaperTimeStart" CssClass="paperTimeSlot" Width="65" runat="server" />
                                                                </td>
                                                                <td style="border: 1px solid silver; padding: 5px; width: 465px">
                                                                    <asp:DataList ID="dl_PaperTimeSlotChoices" runat="server" OnItemDataBound="dl_PaperTimeSlotChoices_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <table>
                                                                                <td valign="top" style="width: 30px">
                                                                                    <asp:Label ID="lbl_TimeSlotChoice" runat="server" />
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <asp:HyperLink ID="hl_PaperTitle" runat="server" />
                                                                                    <asp:Panel ID="pnl_Presenter" Visible="false" runat="server">
                                                                                        <asp:Label ID="lbl_Presenter" CssClass="floaterL" Text="You're presenting!" runat="server" />
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div class="clear">
        </div>
    </div>
                    <div style="width:100%">
                {imgsecuritymarking}
                </div>

    <div class="footer">
    {imgfooter}
    </div>
    </form>
</body>
</html>
