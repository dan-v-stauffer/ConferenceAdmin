using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmailTemplates_InviteReminder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Conference conference = Conference.Instance;

        KTConferenceUser user = new KTConferenceUser(Request.QueryString["user"]);
        
        lbl_userName.Text = user.FirstName;

        DateTime registrationEnd = conference.PrimaryRegistrationClosed;

        lbl_RegistrationEndDate.Text = registrationEnd.ToLocalTime().ToString("h:mm tt (PDT), dddd, MMMM d, yyyy");
    }
}