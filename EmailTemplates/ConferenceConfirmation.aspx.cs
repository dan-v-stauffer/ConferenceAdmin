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

public partial class ConferenceConfirmation : System.Web.UI.Page
{

    EncryptDecryptQueryString security = EncryptDecryptQueryString.Instance;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //repeater_ConfMetaData
            repeater_ConfMetaData.DataSource = Conference.Instance.ToTable();
            repeater_ConfMetaData.DataBind();

            DataList poc = dl_ContactInfo;

            dl_ContactInfo.DataSource = WebDataUtility.Instance.webAppTable("sp_GetConferencePOCs",
                new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });
            dl_ContactInfo.DataBind();



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
                user = new KTConferenceUser(Request.QueryString["user"]);



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
            HyperLink venueMapLinkImg = (HyperLink)e.Item.FindControl("hl_VenueMapLink_img");
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
            venueMapLinkImg.NavigateUrl = drv["venueMapHyperlink"].ToString();
        }
    }

    private void loadForm(RSVP rsvp)
    {
        KTConferenceUser ktUser = (KTConferenceUser)rsvp.User;
        userFirstNameGreeting.Text = ktUser.FirstName + ",";
        lbl_ConfirmationCode.Text = "Confirmation Code: " + rsvp.ConfirmationCode;
        lbl_FullName.Text = ktUser.FullName;
        lbl_Division.Text = ktUser.Division;
        lbl_SpecialNeeds.Text = ktUser.SpecialNeeds.Length==0?"None":ktUser.SpecialNeeds;
        lbl_FoodRestrictions.Text = ktUser.FoodAllergies.Length == 0 ? "None" : ktUser.FoodAllergies;
        lbl_HotelCheckin.Text = ((DateTime)rsvp.CheckInDate).Year == 1900 ? "N/A" : ((DateTime)rsvp.CheckInDate).ToString("dddd, MMMM d, yyyy");
        lbl_HotelCheckout.Text = ((DateTime)rsvp.CheckOutDate).Year == 1900 ? "N/A" : ((DateTime)rsvp.CheckOutDate).ToString("dddd, MMMM d, yyyy");
        lbl_HotelDuration.Text = Math.Floor((((DateTime)rsvp.CheckOutDate) - ((DateTime)rsvp.CheckInDate)).TotalDays).ToString("G") + " nights";
        lbl_ShirtSize.Text = ktUser.ShirtSize.Replace("M", "Mens").Replace("W", "Womens");
        cb_Reception.Checked = rsvp.WelcomeReception;
        //dl_MealDates=rsvp.Meals.s

        if(ktUser.City == "Milpitas")
            pnl_OutofCA_Travellers.Style["display"] = "none";


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

        object techPanelID = null;


        WebDataUtility.Instance.webAppScalar("sp_GetAssignedTechPanel",
                                new GenericCmdParameter[] { new GenericCmdParameter("@userID", ktUser.UserID)}, ref techPanelID );


        DataTable techPanel = WebDataUtility.Instance.webAppTable("sp_GetTechPanelDetails",
                                                new GenericCmdParameter[] { new GenericCmdParameter("@eventID", DBNullable.ToInt(techPanelID)) });
        
        dl_ConferenceDays.DataSource = WebDataUtility.Instance.webAppTable("sp_GetConferenceDateRange",
                                                new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID   ) });
        dl_ConferenceDays.DataBind();
    }

    protected void dl_ConferenceDays_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            Label date = (Label)e.Item.FindControl("lbl_conferenceDay");
            if (date == null)
                return;
            DateTime conferenceDay = Convert.ToDateTime(drv["conferenceDay"]);
            date.Text = conferenceDay.ToLongDateString();

            DataList dl_DaysEvents = (DataList)e.Item.FindControl("dl_DaysEvents");
            
            if (dl_DaysEvents == null)
                return;

            RSVP rsvp = (RSVP)Session["verifyRSVP"];
            if (rsvp == null)
                return;

            dl_DaysEvents.DataSource = WebDataUtility.Instance.webAppTable("sp_GetPersonalSchedule",
                                                new GenericCmdParameter[] { 
                                                    new GenericCmdParameter("@conferenceID", Conference.Instance.ID),
                                                    new GenericCmdParameter("@userID", rsvp.User.UserID),
                                                    new GenericCmdParameter("@invitationType", "Guest"),
                                                    new GenericCmdParameter("@date", conferenceDay),
                                                });
            dl_DaysEvents.DataBind();
        }
    }
    protected void dl_DaysEvents_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            Label lbl_eventTime = (Label)e.Item.FindControl("lbl_eventTime");
            if (lbl_eventTime == null) return;

            Label lbl_eventTitle = (Label)e.Item.FindControl("lbl_eventTitle");
            if (lbl_eventTitle == null) return;

            string eventText = Convert.ToString(drv["eventText"]);
            if(eventText == "TBD" || eventText.Length==0)
                return;

            lbl_eventTime.Text = Convert.ToDateTime(drv["eventStart"]).ToShortTimeString();
            lbl_eventTitle.Text = eventText;

        }

    }


    protected void dl_TechPanelMembers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;
            Label lbl_Panelist = (Label)e.Item.FindControl("lbl_Panelist");

            lbl_Panelist.Text = String.Format("{0} {1}, {2}",
                DBNullable.ToString(drv["techpanelmemberFirstName"]),
                DBNullable.ToString(drv["techpanelmemberLastName"]),
                DBNullable.ToString(drv["techpanelmemberDivision"]));
        }

    }

    protected void dl_WhitePapers_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataList me = (DataList)sender;
            DataRowView drv = (DataRowView)e.Item.DataItem;

            DataTable details = WebDataUtility.Instance.webAppTable("sp_GetPaperDetails",
                                                new GenericCmdParameter[] { new GenericCmdParameter("@paperID", DBNullable.ToInt(drv["paperID"])) });

            if (details.Rows.Count > 0)
            {

                Label lbl_PaperTime = (Label)e.Item.FindControl("lbl_PaperTime");
                Label lbl_PaperTitle = (Label)e.Item.FindControl("lbl_PaperTitle");
                Label lbl_Author = (Label)e.Item.FindControl("lbl_Author");


                DateTime start = DBNullable.ToDateTime(details.Rows[0]["eventStart"]);
                DateTime stop = DBNullable.ToDateTime(details.Rows[0]["eventStop"]);

                lbl_PaperTime.Text = String.Format("{0}-{1}",
                    start.ToString("dddd, hh:mm"),
                    stop.ToString("hh:mm tt"));
                lbl_PaperTitle.Text = DBNullable.ToString(details.Rows[0]["paperTitle"]);
                lbl_Author.Text = string.Format("Presented by {0} {1}, {2} in the {3} Conference Room",
                    DBNullable.ToString(details.Rows[0]["userFirstName"]),
                    DBNullable.ToString(details.Rows[0]["userLastName"]),
                    DBNullable.ToString(details.Rows[0]["userDivision"]),
                    DBNullable.ToString(details.Rows[0]["eventRoom"]));
            }


        }
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