using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KTConferenceUser user = (KTConferenceUser)Session["user"];

        //if (!user.IsConferenceAdministrator())
        //    throw new AccessViolationException(user.FullName + " attempted to access the conference admin site.");

    }
}
