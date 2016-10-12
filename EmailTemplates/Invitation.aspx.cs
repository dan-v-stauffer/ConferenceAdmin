using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmailTemplates_Invitation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Conference conference = Conference.Instance;

        KTConferenceUser user = new KTConferenceUser(Request.QueryString["user"]);
        
        lbl_userName.Text = user.FirstName;

        DateTime registrationEnd = conference.CurrentRegistrationWindowClosed.AddHours(-8);

        lbl_RegistrationEndDate.Text = registrationEnd.ToString("ddd, MMM d, yyyy");
   

    }
}