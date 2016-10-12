using AppSecurity;
using ConferenceLibrary;
using DataUtilities;
using DataUtilities.SQLServer;
using DataUtilities.KTActiveDirectory;
using HelperFunctions;
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

using System.DirectoryServices;
public partial class RegisterStaff : System.Web.UI.Page
{
    EncryptDecryptQueryString security = EncryptDecryptQueryString.Instance;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["page"] = this;


        if (!IsPostBack)
        {
            bn_Dummy.Focus();

            //if (Request.QueryString["user"] == null)
            //    showUtilityModalPopup("invalidRequest", 
            //        "Invalid Staff Login", "Please use the link provided in email to access this page, <br> or enter the staff member's KT Login code here:", string.Empty, "OK", true);

            KTConferenceUser user = new KTConferenceUser(new KTActiveDirectoryUser(
                new KTLogin(System.Security.Principal.WindowsIdentity.GetCurrent().Name)
                    ));
            Session["user"] = user;

            tb_UtilityModalEntry.Attributes["onfocus"] = "javascript:this.select();";

            string strReq = "";
            strReq = Request.RawUrl;
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            string[] strUserName = null;

            if (!user.IsInvitee && user.Login != strUserName[1])
            {
                string msg = "Oops! We cannot find your name on the staff list. Please contact <a href='mailto:daniel.stauffer@kla-tencor.com'>Dan Stauffer, Corp PLC</a>,  " +
                     "to determine if the mistake is ours!<br><br>Are you registering for someone else? If so, please enter their KLA-Tencor email address.";
                showUtilityModalPopup("adminvite", "Registering for Someone Else?", msg, String.Empty, "OK", true);
            }
            else
            {
                loadForm();
            }

            Session["autoRefreshReg"] = false;
        }
        else
        {
            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

            HandleCustomPostbackEvent(ctrlName, args);
        }

        if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
        {
            rbl_TransporationOptions_SelectedIndexChanged(rbl_MonteryInbound, new EventArgs());
            rbl_TransporationOptions_SelectedIndexChanged(rbl_MonteryOutbound, new EventArgs());
        }
    }

    private void HandleCustomPostbackEvent(string ctrlName, string args)
    {
        //Since this will get called for every postback, we only
        // want to handle a specific combination of control
        // and argument.
        RSVP rsvp = (RSVP)Session["rsvp"];
        if (args == "OnBlur")
        {
            switch (ctrlName)
            {
                case ("tb_FirstName"):
                    {
                        if (rsvp.User.FirstName != tb_FirstName.Text)
                        {
                            rsvp.User.FirstName = tb_FirstName.Text;
                            //tb_LastName.Focus();
                        }
                        break;
                    }
                case ("tb_LastName"):
                    {
                        if (rsvp.User.LastName != tb_LastName.Text)
                        {
                        rsvp.User.LastName = tb_LastName.Text;
                        //cbo_Division.Focus();
                        }
                        break;
                    }
                case ("tb_MobilePhone"):
                    {
                        rsvp.User.MobilePhone = tb_MobilePhone.Text;
                        //rb_ShirtType.Focus();
                        break;
                    }
                case ("tb_SpecialNeeds"):
                    {
                        rsvp.User.SpecialNeeds = tb_SpecialNeeds.Text;
                        //dl_TechPanelsList.Focus();
                        break;
                    }
                case ("tb_FoodRestrictions"):
                    {
                        rsvp.User.FoodAllergies = tb_FoodRestrictions.Text;
                        //tb_SpecialNeeds.Focus();
                        break;
                    }
            }
        }
       
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        Label lbl = (Label)progress_Modal.FindControl("lbl_ModalProgress");

        if (hdn_UtilityModalPurpose.Value == "nonvite" && !val_TextEntry.IsValid)
            lbl.Text = "Redirecting you to conference website...";
        else
            lbl.Text = "Working...";
    }

    protected void up_Transportation_Load(object sender, EventArgs e)
    {

    }

    protected void upModal_PreRenderComplete(object sender, EventArgs e)
    {
        Label lbl = (Label)progress_Modal.FindControl("lbl_ModalProgress");

        if (hdn_UtilityModalPurpose.Value == "nonvite" && !val_TextEntry.IsValid)
            lbl.Text = "Redirecting you to conference website...";
        else
            lbl.Text = "Working...";
    }


    protected void dl_MealDates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Conference conference = (Conference)Application["Conference"];

            DataRowView drv = (DataRowView)e.Item.DataItem;
            DateTime date = (DateTime)drv["mealDate"];
            Label dateLabel = (Label)e.Item.FindControl("lbl_MealDate");
            dateLabel.Text = date.ToString("dddd, MMMM d, yyyy");

            DataList dl_MealsOnDate = (DataList)e.Item.FindControl("dl_MealsOnDate");

            dl_MealsOnDate.DataSource = WebDataUtility.Instance.webAppTable("sp_GetNumberMealOptions",
                new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", conference.ID),
                    new GenericCmdParameter("@dateOfMeal", date) });
            dl_MealsOnDate.ItemDataBound += new DataListItemEventHandler(dl_MealsOnDate_ItemDataBound);

            dl_MealsOnDate.DataBind();

        }
    }

    protected void dl_MealsOnDate_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["rsvp"];

            DataList me = (DataList)sender;
            
            DataRowView drv = (DataRowView)e.Item.DataItem;
            int mealID = Convert.ToInt32(drv["mealID"]);

            Label mealType = (Label)e.Item.FindControl("lbl_MealType");
            mealType.Text = Convert.ToString(drv["mealType"]) + ":" ;
            int numberOfOptions = (int)drv["numberOfOptions"];

            AjaxControlToolkit.ComboBox list = (AjaxControlToolkit.ComboBox)e.Item.FindControl("cbo_MultipleMealChoices");

            RadioButtonList rbl = (RadioButtonList)e.Item.FindControl("rbl_SingleMealChoice");
                Panel pnlSingle = (Panel)e.Item.FindControl("pnl_SingleMealChoice");
                Panel pnlMultiple = (Panel)e.Item.FindControl("pnl_MultipleMealChoices");

            list.Attributes.Add("mealID", Convert.ToString(mealID));
            rbl.Attributes.Add("mealID", Convert.ToString(mealID));

            rbl.SelectedIndexChanged += new EventHandler(rbl_SingleMealChoice_SelectedIndexChanged);

            if (numberOfOptions == 1)
            {
                pnlSingle.Visible = true;

                pnlMultiple.Visible = false;
                list.Visible = false;

                object retval = null;
                WebDataUtility.Instance.webAppScalar("sp_GetMealOptionsByMealID",
                     new GenericCmdParameter[] { new GenericCmdParameter("@mealID", mealID) }, ref retval);
                rbl.Attributes.Add("mealOptionID", Convert.ToString(retval));

                rbl.Visible = true;

                if (rsvp.Meals.Rows.Count > 0)
                {
                    DataRow[] selected = rsvp.Meals.Select("mealID = " + mealID);
                    rbl.Items[0].Selected = selected.Length > 0;
                    rbl.Items[1].Selected = selected.Length == 0;
                }
            }
            else
            {
                pnlMultiple.Visible = true;

                pnlSingle.Visible = false;
                rbl.Visible = false;

                list.Visible = true;

                list.DataSource = WebDataUtility.Instance.webAppTable("sp_GetMealOptionsByMealID",
                    new GenericCmdParameter[] { new GenericCmdParameter("@mealID", mealID) });
                list.DataValueField = "mealOptionID";
                list.DataTextField = "mealOptionName";
                list.DataBind();
                list.Items[0].Selected = true;

                DataRow[] selection = rsvp.Meals.Select("mealID = " + mealID);

                foreach (ListItem item in list.Items)
                {
                    foreach (DataRow row in ((DataTable)list.DataSource).Rows)
                    {
                        if (item.Value == Convert.ToString(row["mealOptionID"]))
                        {
                            item.Attributes.Add("onmouseover", "this.title='" + row["mealOptionDescription"].ToString() + "'");
                            item.Attributes.Add("mealID",Convert.ToString(mealID));
                            item.Attributes.Add("mealOptionID", Convert.ToString(row["mealOptionID"]));
                            break;
                        }
                    }
                    
                }
                if (selection.Length > 0)
                    list.SelectedIndex = findListIndex(list.Items, Convert.ToString(selection[0]["mealOptionID"]));
            }
        }
    }

    protected void rbl_SundayReception_SelectedIndexChanged(object sender, EventArgs e)
    {
        RSVP rsvp = (RSVP)Session["rsvp"];
    }

    protected void rbl_SingleMealChoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList me = (RadioButtonList)sender;

        int mealID = Convert.ToInt32(me.Attributes["mealID"]);
        int mealOptionID = Convert.ToInt32(me.Attributes["mealOptionID"]);
        RSVP rsvp = (RSVP)Session["rsvp"];
        if (me.SelectedValue == "True")
        {
            List<MealItem> meal = new List<MealItem>();
            meal.Add(new MealItem(mealID, mealOptionID));
            rsvp.SetMealItems(meal);
        }
        else
        {
            rsvp.ClearMealChoice(mealID);
        }
    }


    private void showUtilityModalPopup( string purpose, string heading, string msg, string initialInputText,string OKButtonText, bool needTextBox)
    {
        hdn_UtilityModalPurpose.Value = purpose;
        lbl_UtilityModalHeader.Text = heading;
        lbl_UtilityModalMessage.Text = msg;

        tb_UtilityModalEntry.Visible = needTextBox;
        if (needTextBox)
        {
            tb_UtilityModalEntry.Text =initialInputText;
            tb_UtilityModalEntry.Focus();
        }
        bn_UtilityModalSave.Text = OKButtonText;

        bn_UtilityModaClose.CssClass = (purpose == "validation" ? "hidden" : String.Empty);
        lbl_UtilityModalMessage.CssClass = (purpose == "validation" ? "validationMsg" : String.Empty);

       // up_UtilityModal.Update();
        mdl_UtilityModal.Show();

    }

    protected void tb_UtilityModalEntry_Validate()
    {
        if (hdn_UtilityModalPurpose.Value == "adminvite")
        {
            try
            {
                KTConferenceUser user = new KTConferenceUser(tb_UtilityModalEntry.Text);
                Session["user"] = null;
                if (user.IsInvitee)
                    Session["user"] = user;

                val_TextEntry.IsValid = (user.IsInvitee);
            }
            catch (Exception e)
            {
                Session["user"] = null;
                val_TextEntry.IsValid = false;
            }
        }
        else if (hdn_UtilityModalPurpose.Value == "invalidRequest")
        {
            try
            {

                KTActiveDirectoryUser adUser = new KTActiveDirectoryUser(tb_UtilityModalEntry.Text);
                if (!adUser.ValidLogin)
                {
                    KTConferenceUser user = new KTConferenceUser(adUser);

                    if (user.IsInvitee)
                        Session["user"] = user;
                    val_TextEntry.IsValid = (user.IsInvitee);
                }
                else
                {
                    Session["user"] = null;
                    val_TextEntry.IsValid = false;
                }
            }
            catch (Exception e)
            {
                Session["user"] = null;
                val_TextEntry.IsValid = false;
            }

        }
        else
        {
            val_TextEntry.IsValid = true;
        }
    }

    protected void bn_UtilityModalSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        switch (hdn_UtilityModalPurpose.Value)
        {
            case ("invalidRequest"):
                {
                    tb_UtilityModalEntry_Validate();
                    if (!val_TextEntry.IsValid)
                        lbl_Error.Text = "*Sorry, that email address is not on the current staff list.<br> Please double-check the spelling or <a href='mailto:daniel.stauffer@kla-tencor.com'>Dan Stauffer, Corp PLC</a>.";
                    else
                    {
                        loadForm();
                        pnl_ExecAdmin.CssClass = "execAdmin";
                        //up_Body.Update();
                        
                    }
                    break;
                }
            case("adminvite"): //this is use case where invited user is registering for someone else. big difference if user cancels then we just reload invited users's info rather than redirect off of page. Also need to tweak the wording of modal popup.
                {
                    tb_UtilityModalEntry_Validate();
                    if (!val_TextEntry.IsValid)
                        lbl_Error.Text = "*Sorry, that email address is not on the current staff list.<br> Please double-check the spelling or <a href='mailto:daniel.stauffer@kla-tencor.com'>Dan Stauffer, Corp PLC</a>.";
                    else
                    {
                        loadForm();
                        pnl_ExecAdmin.CssClass = "execAdmin";
                        //up_Body.Update();
                    }
                    break;  
                }
            case ("validation"):
                {
                    val_TextEntry.IsValid = true;
                    validatePage();
                    break;
                }
            case ("submit"):
                {
                    RSVP rsvp = (RSVP)Session["rsvp"];
                    rsvp.setPhotoWaiver();
                    Session["rsvp"] = rsvp;
                    writeRSVP();
//                    sendStaffConfirmationEmail(true);

                    showSuccessModalPopup("Succesful Registration/Update", "Your registration/update was successful.");

                    break;
                }
            default:
                {
                    break;
                }
        }
        if (val_TextEntry.IsValid)
        {
            hdn_UtilityModalPurpose.Value = string.Empty;
            lbl_UtilityModalMessage.Text = string.Empty;
            tb_UtilityModalEntry.Text = string.Empty;
            lbl_Error.Text = string.Empty;
            lbl_Error.Visible = false;
            mdl_UtilityModal.Hide();
        }
        else
        {
            lbl_Error.Visible = true;
            up_UtilityModal.Update();
            mdl_UtilityModal.Show();
        }

    }

    protected void bn_UtilityModalCancel_Click(object sender, EventArgs e)
    {
        RSVP rsvp = (RSVP)Session["rsvp"];
        if (hdn_UtilityModalPurpose.Value == "adminvite" || hdn_UtilityModalPurpose.Value == "invalidRequest")
        {
            Session["user"] = null;
            Session["rsvp"] = null;
            System.Threading.Thread.Sleep(2000);
            Response.Redirect(ConfigurationManager.AppSettings["RedirectToHome"]);
        }
        if (hdn_UtilityModalPurpose.Value == "submit")
        {

        }

        hdn_UtilityModalPurpose.Value = string.Empty;
        lbl_UtilityModalMessage.Text = string.Empty;
        tb_UtilityModalEntry.Text = string.Empty;
        lbl_Error.Text = string.Empty;
        lbl_Error.Visible = false;
        mdl_UtilityModal.Hide();
    }

    private void showSuccessModalPopup(string headerMsg, string bodyMsg)
    {
        lbl_SuccessMdgHeader.Text = headerMsg;
        lbl_SuccessMsg.Text = bodyMsg;
        modal_Success.Show();
    }

    protected void bn_SuccessModalOK_Click(object sender, EventArgs e)
    {
        lbl_SuccessMsg.Text = string.Empty;
        lbl_SuccessMdgHeader.Text = string.Empty;
        modal_Success.Hide();

        //clean up mdl_UtilityModal
        hdn_UtilityModalPurpose.Value = string.Empty;
        lbl_UtilityModalMessage.Text = string.Empty;
        tb_UtilityModalEntry.Text = string.Empty;
        lbl_Error.Text = string.Empty;
        lbl_Error.Visible = false;
        mdl_UtilityModal.Hide();

        Response.Redirect("Default.aspx");

    }
    protected void cbo_MultipleMealChoices_SelectedIndexChanged(object sender, EventArgs e)
    {
        string index = ((AjaxControlToolkit.ComboBox)sender).SelectedItem.Text;
        
        AjaxControlToolkit.ComboBox me = (AjaxControlToolkit.ComboBox)sender;
        
        int mealID = Convert.ToInt32(me.Attributes["mealID"]);

        RSVP rsvp = (RSVP)Session["rsvp"];
        List<MealItem> meal = new List<MealItem>();
        meal.Add(new MealItem(mealID, Convert.ToInt32(me.SelectedValue)));
        rsvp.SetMealItems(meal);
        Session["rsvp"] = rsvp;
    }

    protected void rb_ShirtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbo_ShirtSize.SelectedIndex > 0)
        {
            RSVP rsvp = (RSVP)Session["rsvp"];
            string shirtSize = rb_ShirtType.SelectedValue + "-" + cbo_ShirtSize.SelectedValue;
            ((KTConferenceUser)rsvp.User).ShirtSize = shirtSize;
        }
    }


    protected void cbo_ShirtSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbo_ShirtSize.SelectedIndex > 0)
        {

            RSVP rsvp = (RSVP)Session["rsvp"];
            string shirtSize = rb_ShirtType.SelectedValue + "-" + cbo_ShirtSize.SelectedValue;
            ((KTConferenceUser)rsvp.User).ShirtSize = shirtSize;
        }
    }
    protected void cbl_HotelReservations_SelectedIndexChanged(object sender, EventArgs e)
    {
        RSVP rsvp = (RSVP)Session["rsvp"];

        DateTime checkIn = new DateTime(9999, 1, 1);
        DateTime checkOut = new DateTime(1, 1, 1);
        foreach (ListItem day in cbl_HotelReservations.Items)
        {
            DateTime t = Convert.ToDateTime(day.Value);
            if (day.Selected)
            {
                if (t < checkIn)
                    checkIn = t;
                if (t > checkOut)
                    checkOut = t;
            }
        }

        rsvp.CheckInDate = checkIn;
        rsvp.CheckOutDate = checkOut.AddDays(1);
    }
    protected void rbl_TransporationOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList me = (RadioButtonList)sender;
        if (me == null)
            return;
       
        String direction = me.Attributes["Direction"];
        RSVP rsvp = null;

        if (Session["rsvp"] == null)
        {
            rsvp = new RSVP(setUser(), "Guest");
        }
        else
        rsvp = (RSVP)Session["rsvp"];

        List<TransportationItem> list = new List<TransportationItem>();

        list.Add(new TransportationItem(Convert.ToInt32(me.SelectedValue), direction));
        rsvp.SetTransporationItem(list);
        Session["rsvp"] = rsvp;
    }

    private KTConferenceUser setUser()
    {
        KTConferenceUser user = null;

        if (Session["user"] == null)
        {
            user = new KTConferenceUser(new KTActiveDirectoryUser(new KTLogin(HttpContext.Current.User.Identity.Name)));

            Session["user"] = user;

            return user;
        }
        else
        {
            user = (KTConferenceUser)Session["user"];
        }

        return user;
    }


    private EventItem createEventItem(int eventID, int parentEventID, int requestOrder, string userRole, bool eventAssigned)
    {
        return new EventItem(eventID, parentEventID, userRole, requestOrder, eventAssigned);
    }




    private bool validatePage()
    {
        if (tb_FirstName.Text.Trim() == string.Empty || tb_LastName.Text.Trim() == string.Empty)
            val_Name.IsValid = false;
        if (cbo_ShirtSize.SelectedItem.Text == "Please select:")
            val_ShirtSize.IsValid = false;

        return (val_ShirtSize.IsValid);

    }

   

    protected void bn_SubmitRSVP_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        if (!validatePage())
        {
            StringBuilder builder = new StringBuilder("Errors! There's some important information missing.\nPlease check the following fields and enter the following information:" + Environment.NewLine);
            builder.AppendLine(Environment.NewLine);
            foreach (IValidator val in Page.Validators)
            {
                if (!val.IsValid)
                {
                    builder.AppendLine(val.ErrorMessage + Environment.NewLine);
                }
            }
            showUtilityModalPopup("validation", "Missing Information", builder.ToString().Replace(Environment.NewLine, "<br />"), string.Empty, "OK", false);
        }
        else
        {
            RSVP rsvp = (RSVP)Session["rsvp"];

            if (rsvp.IsNew || !rsvp.PhotoWaiver)
            {
                string msg = "By submitting your registration, you agree to the terms of the <a href='Files/KT_photo_release_form.pdf' target='_blank'>KLA-Tencor Photograph and Video Waiver</a>.";
                showUtilityModalPopup("submit", "Confirm Registration Submission", msg, string.Empty, "OK", false);
            }
            else
            {
                bool sendChangeEmail = (!rsvp.IsNew); //if is a new registration or a re-register of cancelled rsvp - don't send update email.

                writeRSVP();

                sendConfirmationEmail(false);

                if (sendChangeEmail)
                    rsvp.SendChangeEmail();

                showSuccessModalPopup("Succesful Registration/Update", "Your registration/update was successful.");
            }
        }



    }

    private bool writeRSVP()
    {
        //update database
        //if successful update, give user success message
        //send confirmation email and redirect to finalized page
        RSVP rsvp = (RSVP)Session["rsvp"];

        rsvp.User.FirstName = tb_FirstName.Text;
        rsvp.User.LastName = tb_LastName.Text;
        rsvp.User.MobilePhone = tb_MobilePhone.Text;
        rsvp.User.FoodAllergies = tb_FoodRestrictions.Text;
        rsvp.User.SpecialNeeds = tb_SpecialNeeds.Text;

        string shirtSize = rb_ShirtType.SelectedValue + "-" + cbo_ShirtSize.SelectedValue;
        ((KTConferenceUser)rsvp.User).ShirtSize = shirtSize;
        


        DateTime checkIn = new DateTime(2100, 1, 1);
        DateTime checkOut = new DateTime(1900, 1, 1);

        
        foreach (ListItem day in cbl_HotelReservations.Items)
        {
            DateTime t = Convert.ToDateTime(day.Value);
            if (day.Selected)
            {
                if (t < checkIn)
                    checkIn = t;
                if (t > checkOut)
                    checkOut = t;
            }
        }
        //if year = 2100, no dates selected.
        if (checkIn.Year == 2100)
        {
            rsvp.CheckInDate = new DateTime(1900, 1, 1);
            rsvp.CheckOutDate = new DateTime(1900, 1, 1);
        }
        else
        {
            rsvp.CheckInDate = checkIn;
            rsvp.CheckOutDate = checkOut.AddDays(1);
        }

        if (rsvp.Admin != null)
        {
            rsvp.Admin.FirstName = (tb_ExecAdminFirstName.Text.Length > 0 ? tb_ExecAdminFirstName.Text : rsvp.Admin.FirstName);
            rsvp.Admin.LastName = (tb_ExecAdminLastName.Text.Length > 0 ? tb_ExecAdminLastName.Text : rsvp.Admin.LastName);
            rsvp.Admin.WorkPhone = (tb_ExecAdminPhone.Text.Length > 0 ? tb_ExecAdminPhone.Text : rsvp.Admin.WorkPhone);

        }

        return rsvp.Update();
    }

    private void sendConfirmationEmail(bool isNewRegistration)
    {
        if (Session["rsvp"] != null)
        {
            RSVP rsvp = (RSVP)Session["rsvp"];

            if (rsvp.IsValid)
            {
                string html = string.Empty;
                string url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/EmailBody.aspx?user=" + rsvp.User.Email;

                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Credentials = CredentialCache.DefaultCredentials;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    html = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    html = ex.Message;
                }
                //html = url;
                rsvp.SendEmailConfirmation(html, isNewRegistration);
            }
        }
    }


    protected void bn_Cancel_Click(object sender, EventArgs e)
    {
        Session.Clear();
        redirectToDefault();
    }

    private void redirectToDefault()
    {
        Session["rsvp"] = null;
        Session["user"] = null;

        HttpContext.Current.Response.Redirect("Default.aspx");
    }


    private void loadForm()
    {
        KTConferenceUser user = (KTConferenceUser)Session["user"];

        RSVP rsvp = null;

        if (Session["rsvp"] == null)
            rsvp = new RSVP(user, "KTStaff");
        else
            rsvp = (RSVP)Session["rsvp"];

        rsvp.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(rsvp_PropertyChanged);


        if (!user.Email.Equals(user.Email))
        {
            rsvp.Admin = user;
        }

        Session["rsvp"] = rsvp;

        lblName.Text = ((KTConferenceUser)Session["user"]).FullName;

        //cbl_HotelReservation
        Conference confMetaData = (Conference)Application["Conference"];
        Dictionary<string, Type> parameters = new Dictionary<string, Type>();
        parameters["dateValue"] = typeof(DateTime);
        parameters["dateText"] = typeof(String);
        cbl_HotelReservations.DataSource = Common.GetDateTableFromDateRange(
           Convert.ToDateTime(confMetaData.CheckInStart),
           Convert.ToDateTime(confMetaData.Stop),
           parameters, "dddd, MMMM d, yyyy", true);
        cbl_HotelReservations.DataValueField = "dateValue";
        cbl_HotelReservations.DataTextField = "dateText";
        cbl_HotelReservations.DataBind();
        foreach (ListItem item in cbl_HotelReservations.Items)
        {
            item.Selected = true;
        }
        cbl_HotelReservations.SelectedIndexChanged += new EventHandler(cbl_HotelReservations_SelectedIndexChanged);
        //dl_MealDates
        dl_MealDates.DataSource = WebDataUtility.Instance.webAppTable("sp_MealCountByDate", 
            new GenericCmdParameter[] {new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });
        dl_MealDates.ItemDataBound += new DataListItemEventHandler(dl_MealDates_ItemDataBound);
        dl_MealDates.DataBind();

        //if (!rsvp.IsNew)
        {
            bn_SubmitRSVP.Text = (rsvp.IsNew?"Register":"Update RSVP");
            tb_FirstName.Text = user.FirstName;
            tb_LastName.Text = user.LastName;
            tb_MobilePhone.Text = user.MobilePhone;
            if (user.ShirtSize.Length > 0)
            {
                rb_ShirtType.SelectedIndex = (user.ShirtSize.Substring(0, 1) == "M" ? 0 : 1);

                string size = user.ShirtSize.Substring(2, user.ShirtSize.Length - 2);
                ListItem shirtSize = cbo_ShirtSize.Items.FindByValue(size);
                cbo_ShirtSize.SelectedIndex = findListIndex(cbo_ShirtSize.Items, size);
            }

            tb_FoodRestrictions.Text = user.FoodAllergies;
            tb_SpecialNeeds.Text = user.SpecialNeeds;

            if (rsvp.Admin != null)
            {
                tb_ExecAdminFirstName.Text = rsvp.Admin.FirstName;
                tb_ExecAdminLastName.Text = rsvp.Admin.LastName;
                tb_ExecAdminEmail.Text = rsvp.Admin.Email;
                tb_ExecAdminPhone.Text = rsvp.Admin.WorkPhone;
            }

            foreach (ListItem item in cbl_HotelReservations.Items)
            {
                if (rsvp.CheckInDate == null || rsvp.CheckOutDate == null)
                    item.Selected = false;
                DateTime date = Convert.ToDateTime(item.Value);
                if (date.DayOfYear >= ((DateTime)rsvp.CheckInDate).DayOfYear && date.DayOfYear < ((DateTime)rsvp.CheckOutDate).DayOfYear)
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }
        //else
        //    bn_SubmitRSVP.Text = "Register";


        loadTransportation(rsvp);

    }

    private void rsvp_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Session["rsvp"] = (RSVP)sender;
    }



    private void loadTransportation(RSVP rsvp)
    {
        List<TransportationItem> transItem = new List<TransportationItem>();
        Conference conference = (Conference)Application["Conference"];

        string direction = "Inbound";

        GenericCmdParameter[] parameters = {new GenericCmdParameter("@conferenceID", conference.ID),
                new GenericCmdParameter("@direction", direction)};

        if(!IsPostBack)
        {
            DataTable tbl_Inbound = WebDataUtility.Instance.webAppTable("sp_GetConferenceTransportationOptions", parameters);
            tbl_Inbound.Columns.Add(new DataColumn("dataTextField", typeof(String)));
            lbl_DepartLocationInbound.Text = String.Empty;

            foreach (DataRow row in tbl_Inbound.Rows)
            {
                row["dataTextField"] = (Convert.ToString(row["transportationModeText"]) == "Self" ? "Self" : 
                          Convert.ToString(row["transportationModeText"]) + "* - " 
                          + Convert.ToDateTime(row["transportationDepartTime"]).ToString("MMMM d, yyyy, h:mm tt"));
                lbl_DepartLocationInbound.Text = Convert.ToString(row["transportationModeText"]) == "Self" && lbl_DepartLocationInbound.Text != String.Empty ?
                   lbl_DepartLocationInbound.Text : "*" + Convert.ToString(row["transportationDepartLocation"]);
            }
            rbl_MonteryInbound.DataSource = tbl_Inbound;
            rbl_MonteryInbound.DataValueField = "conferenceTransportationOptionID";
            rbl_MonteryInbound.DataTextField = "dataTextField";

            rbl_MonteryInbound.DataBind();
            rbl_MonteryInbound.AutoPostBack = true;
            rbl_MonteryInbound.SelectedIndexChanged += new EventHandler(rbl_TransporationOptions_SelectedIndexChanged);
        }
        //if (!rsvp.IsNew)
        {
            DataRow[] transOptions = rsvp.Transportation.Select("transportationDirection = 'Inbound'");
            if (transOptions.Length > 0)
            {
                foreach (ListItem item in rbl_MonteryInbound.Items)
                {
                    if (item.Value == Convert.ToString(transOptions[0]["userTransportationOptionID"]))
                        item.Selected = true;
                }
            }
            else
            {
                rbl_MonteryInbound.Items[0].Selected = true;
                transItem.Add(new TransportationItem(Convert.ToInt32(rbl_MonteryInbound.SelectedValue), direction));

            }
        }
        //else
        //    rbl_MonteryInbound.Items[0].Selected = true;
        if(!IsPostBack)
        {
            direction = "Outbound";
            parameters[1].ParamValue = direction;
            DataTable tbl_Outbound = WebDataUtility.Instance.webAppTable("sp_GetConferenceTransportationOptions", parameters);
            tbl_Outbound.Columns.Add(new DataColumn("dataTextField", typeof(String)));

            lbl_DepartLocationOutbound.Text = String.Empty;

            foreach (DataRow row in tbl_Outbound.Rows)
            {
                row["dataTextField"] = (Convert.ToString(row["transportationModeText"]) == "Self" ? "Self" : 
                          Convert.ToString(row["transportationModeText"]) + "* - " 
                          + Convert.ToDateTime(row["transportationDepartTime"]).ToString("MMMM d, yyyy, h:mm tt"));

                lbl_DepartLocationOutbound.Text = Convert.ToString(row["transportationModeText"]) == "Self" && lbl_DepartLocationOutbound.Text != String.Empty ?
                    lbl_DepartLocationOutbound.Text : "*" + Convert.ToString(row["transportationDepartLocation"]);
            }

            rbl_MonteryOutbound.DataSource = tbl_Outbound;
            rbl_MonteryOutbound.DataValueField = "conferenceTransportationOptionID";
            rbl_MonteryOutbound.DataTextField = "dataTextField";
            rbl_MonteryOutbound.AutoPostBack = true;
            rbl_MonteryOutbound.SelectedIndexChanged += new EventHandler(rbl_TransporationOptions_SelectedIndexChanged);
            rbl_MonteryOutbound.DataBind();
        }
        //if (!rsvp.IsNew)
        {
            DataRow[] transOptions = rsvp.Transportation.Select("transportationDirection = 'Outbound'");
            if (transOptions.Length > 0)
            {
                foreach (ListItem item in rbl_MonteryOutbound.Items)
                {
                    if (item.Value == Convert.ToString(transOptions[0]["userTransportationOptionID"]))
                        item.Selected = true;
                }
            }
            else
            {
                rbl_MonteryOutbound.Items[0].Selected = true;
                transItem.Add(new TransportationItem(Convert.ToInt32(rbl_MonteryOutbound.SelectedValue), direction));
            }
        }
        //else
        //{
        //    rbl_MonteryOutbound.Items[0].Selected = true;
        //    transItem.Add(new TransportationItem(Convert.ToInt32(rbl_MonteryOutbound.SelectedValue), direction));
        //    rsvp.SetTransporationItem(transItem);
        //}
    }

    private int findListIndex(ListItemCollection list, string value)
    {
        int i = 0;
        bool selected = false;
        foreach (ListItem item in list)
        {
            if (item.Value == value)
            {
                selected = true;
                break;
            }
            else
                i++;
        }
        if (!selected)
            i = 0;
        return i;
    }

    
    private Control FindControlRecursiveByClientID(Control rootControl, string controlClientID)
    {
        if (rootControl.ClientID == controlClientID) return rootControl;

        foreach (Control controlToSearch in rootControl.Controls)
        {
            Control controlToReturn =
                FindControlRecursiveByClientID(controlToSearch, controlClientID);
            if (controlToReturn != null) return controlToReturn;
        }
        return null;
    }

    private Control FindControlRecursiveByID(Control rootControl, string controlID)
    {
        if (rootControl.ID == controlID) return rootControl;

        foreach (Control controlToSearch in rootControl.Controls)
        {
            Control controlToReturn =
                FindControlRecursiveByID(controlToSearch, controlID);
            if (controlToReturn != null) return controlToReturn;
        }
        return null;
    }

}
