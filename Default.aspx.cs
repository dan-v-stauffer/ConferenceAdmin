using AppSecurity;
using ConferenceLibrary;
using DataUtilities;
using DataUtilities.SQLServer;
using DataUtilities.KTActiveDirectory;
using HelperFunctions;
using Microsoft.Reporting.WebForms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConferenceUser user = (ConferenceUser)Session["user"];

            setLinkEnable(Conference.Instance.IsAdmin(user.Email));

            ddl_ConferenceSelect.DataSource = WebDataUtility.Instance.webAppTable("sp_GetConferences", null);
            
            ddl_ConferenceSelect.DataValueField = "conferenceID";
            ddl_ConferenceSelect.DataTextField = "conferenceTitle";

            ddl_ConferenceSelect.DataBind();

            ddl_InviteType.DataSource = Conference.Instance.InviteTypes();
            ddl_InviteType.DataValueField = "inviteType";
            ddl_InviteType.DataTextField = "inviteType";
            ddl_InviteType.DataBind();
        }
        else
        {

        }
    }
    protected void ddl_ConferenceSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Divisions.DataSource = WebDataUtility.Instance.webAppTable("sp_GetKTDivisions", 
                new GenericCmdParameter[] { new GenericCmdParameter("conferenceID", ddl_ConferenceSelect.SelectedValue) });
        ddl_Divisions.DataValueField = "userDivision";
        ddl_Divisions.DataTextField = "userDivision";
         ddl_Divisions.DataBind();
    }

    private void setLinkEnable(bool enable)
    {
        hl_ManageEvent.Enabled = enable;
        lb_SendStaffInvites.Enabled = enable;
        lb_ManageAttendees.Enabled = enable;
    }
    protected void lb_SendStaffInvites_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "staff_invites";
        showSendEmailModalPopup();
    }

    protected void lb_AddInvitee_Click(object sender, EventArgs e)
    {
        modal_AddInvitee.Show();
    }

    protected void lb_RegisterKTStaff_Click(object sender, EventArgs e)
    {
        DataTable ktStaff = WebDataUtility.Instance.webAppTable("sp_GetKTStaff",
            new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });

        dl_Staff.DataSource = ktStaff;
        dl_Staff.Attributes.Remove("inviteType");
        dl_Staff.Attributes.Add("inviteType", "KTStaff");
        dl_Staff.DataBind();
        modal_Staff.Show();
    }

    protected void lb_ManageAttendees_Click(object sender, EventArgs e)
    {
        modal_Attendee.Show();
    }

    protected void dl_Staff_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            
            HyperLink hl_Staff = (HyperLink)e.Item.FindControl("hl_Staff");

            ConferenceUser user = (ConferenceUser)Session["user"];

            if (hl_Staff != null)
            {
                string navURL = string.Empty;
                string inviteType = me.Attributes["inviteType"];

                switch (inviteType)
                {
                    case "KTStaff":
                        {
                            navURL = "RegisterStaff.aspx?user=" + DBNullable.ToString(drv["userLogin"]);

                            if (DBNullable.ToString(drv["userLogin"]) == String.Empty)
                            {
                                try
                                {
                                    KTActiveDirectoryUser adUser = new KTActiveDirectoryUser(DBNullable.ToString(drv["userEmail"]));
                                    ConferenceUser newUser = new KTConferenceUser(adUser);
                                    newUser.Update();
                                }
                                catch (Exception exc)
                                { }
                            }

                            if (Conference.Instance.IsAdmin(user.Email))
                            {
                                hl_Staff.Enabled = true;
                            }
                            else if (user.Email.Equals(DBNullable.ToString(drv["userEmail"])))
                            {
                                hl_Staff.Enabled = true;
                            }
                            else
                                hl_Staff.Enabled = false;

                            break;
                        }
                    case "ExternalStaff":
                        {
                            navURL = "RegisterExtStaff.aspx?user=" + DBNullable.ToString(drv["userEmail"]);

                            break;
                        }
                    case "VIP":
                        {
                            navURL = "RegisterVIP.aspx?user=" + DBNullable.ToString(drv["userEmail"]);
                            break;
                        }
                }
                
                
                hl_Staff.Text = String.Format("{0} {1}, {2}", 
                    DBNullable.ToString(drv["userFirstName"]), 
                    DBNullable.ToString(drv["userLastName"]),
                    DBNullable.ToString(drv["userOrganization"]));
                hl_Staff.NavigateUrl =navURL;
                

              

            }
        }
    }
    protected void ddl_AttendeeSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList me = (DropDownList)sender;
        string userEmail = me.SelectedValue;

        Response.Redirect("ManageAttendee.aspx?userEmail=" + userEmail);

    }
    protected void bn_SuccessModalOK_Click(object sender, EventArgs e)
    {
        modal_Staff.Hide();
    }

    protected void lb_SendGuestInvitations_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "invitations";
        showSendEmailModalPopup();
    }

    protected void lb_SendReminders_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "reminders";
        showSendEmailModalPopup();
    }
    protected void lb_SendSurveyEmails_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "surveys";
        showSendEmailModalPopup();
    }

    protected void lb_SendBatchConfirmations_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "confirmations";
        showSendEmailModalPopup();
    }

    protected void lb_SendStaffInvitations_Click(object sender, EventArgs e)
    {
        hdn_SendEmailPurpose.Value = "staff_invites";
        showSendEmailModalPopup();
    }
    protected void bn_AddInviteeOK_Click(object sender, EventArgs e)
    {
        if (tb_InviteeEmail.Text.Length == 0)
            return;
        try
        {
            KTConferenceUser user = new KTConferenceUser(tb_InviteeEmail.Text);
            
            Conference.Instance.AddInvitee(user, ddl_Divisions.SelectedValue, ddl_InviteType.SelectedValue, cb_IsExec.Checked);

        }
        catch (Exception exc)
        {
        }

    }

    protected void lb_CreateSchedules_Click(object sender, EventArgs e)
    {
                        Warning[] warnings;

        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;
        //setup report datasource


        DataTable guests = WebDataUtility.Instance.webAppTable("sp_GetRegisteredStaff",
                            new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });

        foreach (DataRow row in guests.Rows)
        {
                 KTConferenceUser user = new KTConferenceUser(DBNullable.ToString(row["userEmail"]));
           try
            {
                RSVP rsvp = new RSVP(user, DBNullable.ToString(row["rsvpInvitationType"]));

                int userID = user.UserID;
                string reportsFolder = Server.MapPath("Temp");
                string fileName = reportsFolder + "\\" + String.Format("{0}.{1}", user.FirstName, user.LastName) + ".pdf";

                if (File.Exists(fileName))
                    File.Delete(fileName);
                FileInfo file = new FileInfo(fileName);

                if (!file.Exists)
                {
                    DataTable rptData = WebDataUtility.Instance.webAppTable("sp_GetPersonalSchedule",
                        new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                                                new GenericCmdParameter("@userID", userID),
                                                new GenericCmdParameter("@invitationType", rsvp.InvitationType)        });
                    ReportDataSource rds = new ReportDataSource("DataSet1", rptData);

                    // Setup the report viewer object and get the array of bytes
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.DataSources.Add(rds);
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = "Reports/PersonalAgenda.rdlc";

                    ReportParameter Param0 = new ReportParameter("conferenceID", DBNullable.ToString(Conference.Instance.ID));
                    ReportParameter Param1 = new ReportParameter("userID", userID.ToString());
                    ReportParameter Param2 = new ReportParameter("userName", String.Format("{0} {1}",
                                                    user.FirstName, user.LastName));

                    viewer.LocalReport.SetParameters(new ReportParameter[] { Param0, Param1, Param2 });

                    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    //File.Delete(fileName);

                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

                string cc = (rsvp.Admin == null ? string.Empty : rsvp.Admin.Email);

       //        Conference.Instance.SendConfirmationEmail(this.Page, user, cc, fileName);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(new Exception("FAILED: Invite sent to " +
                    user.FullName.ToUpper()), "bn_TestEmail_Click(object sender, EventArgs e)");
                ExceptionUtility.LogException(ex, "bn_TestEmail_Click(object sender, EventArgs e)");
            }
        }

    }

    private void showSendEmailModalPopup()
    {
        switch(hdn_SendEmailPurpose.Value.ToString())
        {
            case ("invitations"):
                {
                    DataTable tbl = WebDataUtility.Instance.webAppTable("sp_GetUnsentInvitationsInClass",
                       new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                       new GenericCmdParameter("@inviteClass", DateTime.Now < Conference.Instance.PrimaryRegistrationClosed?1:2) });

                    lbl_SendEmailHeader.Text = "Send User Invitations";
                    lbl_SendEmailMessage.Text = "Select OK to send invitations to the following " + tbl.Rows.Count + " recipient(s).";

                    lb_EmailRecipients.Items.Clear();
                    lb_EmailRecipients.DataSource = tbl;
                    lb_EmailRecipients.DataValueField = "userID";
                    lb_EmailRecipients.DataTextField = "userFullName";
                    lb_EmailRecipients.DataBind();


                   break;
                }
            case ("reminders"):
                {

                    DataTable tbl = WebDataUtility.Instance.webAppTable("sp_GetUnregisteredInviteesInClass",
                       new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                       new GenericCmdParameter("@inviteClass", DateTime.Now < Conference.Instance.PrimaryRegistrationClosed?1:2) });

                    lbl_SendEmailHeader.Text = "Send User Invitation Reminders";
                    lbl_SendEmailMessage.Text = "Select OK to send reminders to the following " + tbl.Rows.Count + " recipient(s).";

                    lb_EmailRecipients.Items.Clear();
                    lb_EmailRecipients.DataSource = tbl;
                    lb_EmailRecipients.DataValueField = "userID";
                    lb_EmailRecipients.DataTextField = "userFullName";
                    lb_EmailRecipients.DataBind();


                    break;
                }
            case ("confirmations"):
                {
                    DataTable tbl = WebDataUtility.Instance.webAppTable("sp_GetRSVPConfirmationBatch",
                       new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                          new GenericCmdParameter("@ignoreSentEmails", false)});

                    lbl_SendEmailHeader.Text = "Send RSVP Confirmations";
                    lbl_SendEmailMessage.Text = "Select OK to send RSVP Confirmations to the following " + tbl.Rows.Count + " recipient(s).";

                    lb_EmailRecipients.Items.Clear();
                    lb_EmailRecipients.DataSource = tbl;
                    lb_EmailRecipients.DataValueField = "userID";
                    lb_EmailRecipients.DataTextField = "userFullName";
                    lb_EmailRecipients.DataBind();


                    break;
                }
            case ("surveys"): //this is use case where non-invited user is registering for an invited user. ie. - exec admin registering for boss.
                {

                    DataTable tbl = WebDataUtility.Instance.webAppTable("sp_GetRSVPConfirmationBatch",
                       new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                       new GenericCmdParameter("@ignoreSentEmails", true)});

                    lbl_SendEmailHeader.Text = "Send RSVP Confirmations";
                    lbl_SendEmailMessage.Text = "Select OK to send Conference Survey Emails to the following " + tbl.Rows.Count + " recipient(s).";

                    lb_EmailRecipients.Items.Clear();
                    lb_EmailRecipients.DataSource = tbl;
                    lb_EmailRecipients.DataValueField = "userID";
                    lb_EmailRecipients.DataTextField = "userFullName";
                    lb_EmailRecipients.DataBind();

                    break;
                }
            case ("staff_invites"): //this is use case where invited user is registering for someone else. big difference if user cancels then we just reload invited users's info rather than redirect off of page. Also need to tweak the wording of modal popup.
                {
                    DataTable tbl = WebDataUtility.Instance.webAppTable("sp_GetKTStaff",
                        new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID)});

                    lbl_SendEmailHeader.Text = "Send Staff Invitations";
                    lbl_SendEmailMessage.Text = "Select OK to send Staff Invitations to the following " + tbl.Rows.Count + " recipient(s).";

                    lb_EmailRecipients.Items.Clear();
                    lb_EmailRecipients.DataSource = tbl;
                    lb_EmailRecipients.DataValueField = "userID";
                    lb_EmailRecipients.DataTextField = "userFullName";
                    lb_EmailRecipients.DataBind();
                    break;
                }
            default:
                {
                    break;
                }
        }

        // up_SendEmail.Update();
        modal_SendEmail.Show();

    }

    protected void bn_SendEmailModalCancel_Click(object sender, EventArgs e)
    {
        lb_EmailRecipients.Items.Clear();
        lbl_SendEmailHeader.Text = string.Empty;
        lbl_SendEmailMessage.Text = string.Empty;
        hdn_SendEmailPurpose.Value = string.Empty;
        cb_TestEmailsOnly.Checked = true;
        modal_SendEmail.Hide();
    }

    protected void bn_SendEmailModalSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);

        bool testingOnly = cb_TestEmailsOnly.Checked;

        switch (hdn_SendEmailPurpose.Value)
        {
            case ("invitations"):
                {
                    int inviteClass = (DateTime.Now <= Conference.Instance.PrimaryRegistrationClosed ? 1 : (DateTime.Now <= Conference.Instance.LateRegistrationClosed ? 2 : 3));
                    int emailsSent = Conference.Instance.SendInvitations(sender, inviteClass, testingOnly);

                    break;
                }
            case ("reminders"):
                {
                    int inviteClass = (DateTime.Now <= Conference.Instance.PrimaryRegistrationClosed ? 1 : (DateTime.Now <= Conference.Instance.LateRegistrationClosed ? 2 : 3));
                    int emailsSent = Conference.Instance.SendReminderEmails(sender, inviteClass, testingOnly);

                    break;
                }
            case ("confirmations"):
                {
                    Control context = (Control)sender;
                    Conference.Instance.SendConfirmationEmailBatch(context, testingOnly);

                    break;
                }
            case ("surveys"): //this is use case where non-invited user is registering for an invited user. ie. - exec admin registering for boss.
                {
                    DataTable guests = WebDataUtility.Instance.webAppTable("sp_GetRegisteredGuests",
                        new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });

                    foreach (DataRow row in guests.Rows)
                    {
                        KTConferenceUser user = new KTConferenceUser(DBNullable.ToString(row["userEmail"]));

                        if (user.IsExec())
                            continue;

                        RSVP rsvp = new RSVP(user, "Guest");

                        try
                        {
                            Conference.Instance.SendGenericConferenceEmail(this.Page, rsvp, "/Survey.aspx",
                               "Reminder: 2015 Engineering Conference Survey", string.Empty, true, testingOnly);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    break;
                }
            case ("staff_invites"): //this is use case where invited user is registering for someone else. big difference if user cancels then we just reload invited users's info rather than redirect off of page. Also need to tweak the wording of modal popup.
                {
                    Conference.Instance.SendStaffInvitationEmails(testingOnly);

                    break;
                }
            default:
                {
                    break;
                }
        }
        modal_SendEmail.Hide();

    }




}
