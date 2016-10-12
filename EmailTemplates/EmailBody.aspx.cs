using AppSecurity;
using ConferenceLibrary;
using DataUtilities;
using DataUtilities.SQLServer;
using DataUtilities.KTActiveDirectory;
using HelperFunctions;
using System;
using System.Collections.Generic;
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

public partial class EmailBody : System.Web.UI.Page
{

    EncryptDecryptQueryString security = EncryptDecryptQueryString.Instance;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //repeater_ConfMetaData
            Conference confMetaData = (Conference)Application["Conference"];

            repeater_ConfMetaData.DataSource = confMetaData.ToTable();
            repeater_ConfMetaData.DataBind();

            KTConferenceUser user = null;

            string strReq = "";
            strReq = Request.RawUrl;
            strReq = strReq.Substring(strReq.IndexOf('?') + 1);
            string[] strUserName = null;

            try
            {
                string qs = security.Decrypt(strReq, user.Email.Substring(0, 8));

                if (qs.Contains('='))
                    strUserName = qs.Split('=');

            }
            catch (Exception ex)
            {
            }

            if (Session["user"] == null)
                user = new KTConferenceUser(Request.QueryString["user"]);
            else
                user = (KTConferenceUser)Session["user"];


            //does user have a rsvp submitted
            //if yes, load form
            RSVP rsvp = new RSVP(user, "Guest");
            Session["verifyRSVP"] = rsvp;
            //if no, error msg and redirect to Default.aspx
            DataTable adminRSVP = WebDataUtility.Instance.webAppTable("sp_GetRsvpsForAdmin", new GenericCmdParameter[] { 
            new GenericCmdParameter("@conferenceID", Convert.ToInt32(Session["conferenceID"])),
            new GenericCmdParameter("@adminEmail", user.Email) });

            int adminCount = adminRSVP.Rows.Count;

            //adminCount = 1;

            //if (pnl_POC != null)
            //{
            //    pnl_POC.CssClass = "hidden";
            //}


            loadForm(rsvp);

            //is user an 'admin' for other rsvp's?
            //if yes, populate combo box

            //if no then hide listbutton (lb_ShowAdminRsvps)

        }

    }

    protected void repeater_ConfMetaData_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            String strRegCloses = Convert.ToString(drv["conferenceRegistrationClosed"]);
            DateTime dateRegCloses;
            Session["registrationCloses"] = DateTime.TryParse(strRegCloses, out dateRegCloses);

            Label confCheckInStart = (Label)e.Item.FindControl("lbl_ConfCheckInStart");
            Label confStart = (Label)e.Item.FindControl("lbl_ConfStart");
            Label confStop = (Label)e.Item.FindControl("lbl_ConfStop");
            Label receptionStart = (Label)e.Item.FindControl("lbl_WelcomeReceptionStart");
            Label receptionStop = (Label)e.Item.FindControl("lbl_WelcomeReceptionStop");
            Label regCloses = (Label)e.Item.FindControl("lbl_RegistrationCloses");
            HyperLink venueLink = (HyperLink)e.Item.FindControl("hl_VenueLink");
            Label venueAddress = (Label)e.Item.FindControl("lbl_VenueAddress");
            HyperLink venueMapLink = (HyperLink)e.Item.FindControl("hl_VenueMapLink");
            //conferenceWelcomeReception
            confStart.Text = Convert.ToDateTime(drv["conferenceStartTime"].ToString()).ToString("dddd, MMMM d, yyyy h:mm tt");
            confStop.Text = Convert.ToDateTime(drv["conferenceEndTime"].ToString()).ToString("dddd, MMMM d, yyyy h:mm tt");
            receptionStart.Text = Convert.ToDateTime(drv["receptionStart"].ToString()).ToString("dddd, MMMM d, yyyy h:mm");
            receptionStop.Text = Convert.ToDateTime(drv["receptionStop"].ToString()).ToString("h:mm tt");
            venueLink.NavigateUrl = drv["venueWebAddress"].ToString();
            venueLink.Text = drv["venueName"].ToString();
            venueAddress.Text = String.Format("{0}<br>{1}, {2} {3} ",
                new object[] { drv["venueStreetAddress"].ToString(), 
                             drv["venueCity"].ToString(), 
                             drv["venueState"].ToString(), 
                             drv["venueZip"].ToString() });

            venueMapLink.NavigateUrl = drv["venueMapHyperlink"].ToString();

        }
    }

    private void loadForm(RSVP rsvp)
    {
        KTConferenceUser ktUser = (KTConferenceUser)rsvp.User;
        lbl_ConfirmationCode.Text = "Confirmation Code: " + rsvp.ConfirmationCode;
        lbl_FullName.Text = ktUser.FullName;
        lbl_Division.Text = ktUser.Division;
        lbl_WorkLocation.Text = ktUser.HomeOffice;
        lbl_MobilePhone.Text = ktUser.MobilePhone;
        lbl_JobRole.Text = ktUser.JobRole;
        lbl_SpecialNeeds.Text = ktUser.SpecialNeeds;
        lbl_FoodRestrictions.Text = ktUser.FoodAllergies;
        lbl_HotelCheckin.Text = ((DateTime)rsvp.CheckInDate).Year == 1900 ? "N/A" : ((DateTime)rsvp.CheckInDate).ToString("dddd, MMMM d, yyyy");
        lbl_HotelCheckout.Text = ((DateTime)rsvp.CheckOutDate).Year == 1900 ? "N/A" : ((DateTime)rsvp.CheckOutDate).ToString("dddd, MMMM d, yyyy");
        lbl_HotelDuration.Text = Math.Floor((((DateTime)rsvp.CheckOutDate) - ((DateTime)rsvp.CheckInDate)).TotalDays).ToString("G") + " nights";
        lbl_ShirtSize.Text = ktUser.ShirtSize.Replace("M", "Mens").Replace("W", "Womens");
        cb_Golfing.Checked = rsvp.Golfing;
        cb_Reception.Checked = rsvp.WelcomeReception;
        //dl_MealDates=rsvp.Meals.s
        cb_SundayDeepLearning.Checked = rsvp.ContainsEvent(313);

        DataView view = new DataView(rsvp.GetMealsDetails());
        DataTable distinctDates = view.ToTable(true, "simpleDate");
        dl_MealDates.DataSource = distinctDates;
        dl_MealDates.DataBind();

        foreach (DataRow row in rsvp.GetTransporationDetails().Rows)
        {
            string direction = Convert.ToString(row["transportationDirection"]);
            string modeText = Convert.ToString(row["transportationmodeText"]);
            DateTime depart = Convert.ToDateTime(row["transportationDepartTime"]);
            bool hideTime = modeText.Equals("Self");
            if (direction == "Inbound")
                lbl_FromKT.Text = modeText + (hideTime ? String.Empty : " (" + depart.ToString("MM/dd h:mm tt") + ")");
            if (direction == "Outbound")
                lbl_ToKT.Text = modeText + (hideTime ? String.Empty : " (" + depart.ToString("MM/dd h:mm tt") + ")");

        }

        //lbl_FromKT.Text = rsvp.Transportation

        dl_TechPanelsList.DataSource = rsvp.GetTechPanelsDetails();
        dl_TechPanelsList.DataBind();


        DataView symposiumView = new DataView(rsvp.GetPaperDetails());
        DataTable sypmosiums = symposiumView.ToTable(true, new string[] { "symposiumText", "symposiumStart", "symposiumStop" });

        Session["rsvpPaperDetails"] = symposiumView;

        dl_PaperSymposiaList.DataSource = sypmosiums;
        dl_PaperSymposiaList.DataBind();

    }

    protected void dl_MealDates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["verifyRSVP"];

            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            DateTime day = Convert.ToDateTime(drv["simpleDate"]);

            Label lbl_MealDate = (Label)e.Item.FindControl("lbl_MealDate");
            lbl_MealDate.Text = Convert.ToDateTime(drv["simpleDate"]).ToString("dddd, MMMM dd, yyyy");

            DataList dl_MealsOnDate = (DataList)e.Item.FindControl("dl_MealsOnDate");
            DataView view = new DataView(rsvp.GetMealsDetails());
            view.RowFilter = "simpleDate = '" + day + "'";
            dl_MealsOnDate.DataSource = view;
            dl_MealsOnDate.DataBind();

        }

    }

    protected void dl_MealsOnDate_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            Label lbl_MealType = (Label)e.Item.FindControl("lbl_MealType");
            Label lbl_MealChoice = (Label)e.Item.FindControl("lbl_MealChoice");

            lbl_MealType.Text = Convert.ToString(drv["mealType"]) + ": ";

            if (Convert.ToInt32(drv["optionCount"]) == 1)
                lbl_MealChoice.Text = "Yes";
            else
                lbl_MealChoice.Text = Convert.ToString(drv["mealOptionName"]);


        }
    }

    protected void dl_PaperSymposiaList_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["verifyRSVP"];

            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            DataList dl_PaperTimeSlots = (DataList)e.Item.FindControl("dl_PaperTimeSlots");
            Label lbl_SymposiaGroupTitle = (Label)e.Item.FindControl("lbl_SymposiaGroupTitle");
            Label lbl_SymposiaGroupTimes = (Label)e.Item.FindControl("lbl_SymposiaGroupTimes");

            lbl_SymposiaGroupTitle.Text = Convert.ToString(drv["symposiumText"]);
            lbl_SymposiaGroupTimes.Text = Convert.ToDateTime(drv["symposiumStart"]).ToString("dddd, MMMM dd yyyy hh:mm -") +
                Convert.ToDateTime(drv["symposiumStop"]).ToString(" hh:mm tt");

            DataView allData = (DataView)Session["rsvpPaperDetails"];
            allData.RowFilter = "symposiumText = '" + Convert.ToString(drv["symposiumText"]) + "'";
            DataTable timeSlots = allData.ToTable(true, new string[] { "slotID", "slotStart", "slotStop" });

            dl_PaperTimeSlots.DataSource = timeSlots;
            dl_PaperTimeSlots.DataBind();
        }

    }

    protected void dl_PaperTimeSlots_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["verifyRSVP"];

            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            DataList dl_PaperTimeSlotChoices = (DataList)e.Item.FindControl("dl_PaperTimeSlotChoices");
            Label lbl_PaperTimeStart = (Label)e.Item.FindControl("lbl_PaperTimeStart");


            lbl_PaperTimeStart.Text = Convert.ToDateTime(drv["slotStart"]).ToString("hh:mm tt");

            DataView allData = (DataView)Session["rsvpPaperDetails"];
            allData.RowFilter = "slotID = " + Convert.ToString(drv["slotID"]);
            DataTable papers = allData.ToTable(true, new string[] { "eventRequestOrder", "paperID", "paperTitle", "paperHyperLink", "authorID", "authorFirstName", "authorLastName", "authorDivision" });
            dl_PaperTimeSlotChoices.DataSource = papers;
            dl_PaperTimeSlotChoices.DataBind();
        }
    }

    protected void dl_PaperTimeSlotChoices_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["verifyRSVP"];

            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            Label lbl_TimeSlotChoice = (Label)e.Item.FindControl("lbl_TimeSlotChoice");
            HyperLink hl_PaperTitle = (HyperLink)e.Item.FindControl("hl_PaperTitle");

            Panel pnl_Presenter = (Panel)e.Item.FindControl("pnl_Presenter");
            Label lbl_Presenter = (Label)e.Item.FindControl("lbl_Presenter");

            int order = Convert.ToInt32(drv["eventRequestOrder"]);
            string sup = "<sup>" + (order == 1 ? "st" : "nd") + "</sup>";

            lbl_TimeSlotChoice.Text = order.ToString() + sup;
            hl_PaperTitle.Text = Convert.ToString(drv["paperTitle"]);
            hl_PaperTitle.NavigateUrl = Convert.ToString(drv["paperHyperLink"]);

            if (Convert.ToInt32(drv["authorID"]) == rsvp.User.UserID)
            {
                pnl_Presenter.Visible = true;
                lbl_Presenter.Text = "**You're presenting this paper!";
            }

        }
    }

    protected void dl_TechPanelsList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RSVP rsvp = (RSVP)Session["verifyRSVP"];

            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            Label lbl_TechPanelChoice = (Label)e.Item.FindControl("lbl_TechPanelChoice");
            Label lbl_TechPanelTitle = (Label)e.Item.FindControl("lbl_TechPanelTitle");
            Label lbl_Moderator = (Label)e.Item.FindControl("lbl_Moderator");

            Panel pnl_ModOrMember = (Panel)e.Item.FindControl("pnl_ModOrMember");
            Label lbl_ModOrMember = (Label)e.Item.FindControl("lbl_ModOrMember");
            
            int choice = Convert.ToInt32(drv["eventRequestOrder"]);
            string super = (choice == 1 ? "st" : choice == 2 ? "nd" : "rd");
            lbl_TechPanelChoice.Text = Convert.ToString(choice + "<sup>" + super + "</sup>:");
            lbl_TechPanelTitle.Text = Convert.ToString(drv["techpanelTitle"]);
            lbl_Moderator.Text = "Moderated by: " + Convert.ToString(drv["modFirstName"]) + " " + Convert.ToString(drv["modLastName"]) + ", " + Convert.ToString(drv["modDivision"]);
            if (Convert.ToString(drv["userRole"]).Contains("Panel M"))
                pnl_ModOrMember.Visible = true;
            if (Convert.ToString(drv["userRole"]).Contains("Moderator"))
                lbl_ModOrMember.Text = "**You're moderating this panel!";
            else
                lbl_ModOrMember.Text = "**You're a member on this panel!";
        }

    }

    private KTConferenceUser setUser()
    {
        KTConferenceUser user = null;

        if (Session["user"] == null)
        {
            user = new KTConferenceUser(new KTActiveDirectoryUser(new KTLogin(HttpContext.Current.User.Identity.Name)));
#if DEBUG
                user = new KTConferenceUser(new KTActiveDirectoryUser(new KTLogin("KLASJ\\arangle")));
#endif

            Session["user"] = user;
            return user;
        }
        else
        {
            user = (KTConferenceUser)Session["user"];
        }

        return user;
    }




}