using AppSecurity;
using ConferenceLibrary;
using DataUtilities;
using DataUtilities.SQLServer;
using DataUtilities.KTActiveDirectory;
using HelperFunctions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmailTemplates_iConnect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KTConferenceUser user = new KTConferenceUser(Request.QueryString["email"]);
        lbl_userName.Text = user.FirstName + ",";
    }
}