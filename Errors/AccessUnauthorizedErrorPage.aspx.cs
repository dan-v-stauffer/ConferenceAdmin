using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Errors_AccessUnauthorizedErrorPage: System.Web.UI.Page
{
    protected HttpException ex = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear the error from the server
        Server.ClearError();
    }
}