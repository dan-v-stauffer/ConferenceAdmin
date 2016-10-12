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

public partial class PostConferenceEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KTConferenceUser user = new KTConferenceUser(Request.QueryString["email"]);
        lbl_userName.Text = user.FirstName;

    }
}