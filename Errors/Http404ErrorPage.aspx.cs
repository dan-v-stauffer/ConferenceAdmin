using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Errors_Http404ErrorPage : System.Web.UI.Page
{
    protected HttpException ex = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string src = (!Request.QueryString[0].Equals(null) ? Request.QueryString[0] : string.Empty);
        // Log the exception and notify system operators
        ex = new HttpException("HTTP 404");
        ExceptionUtility.LogException(ex, "Caught in Http404ErrorPage" + src);
        ExceptionUtility.NotifySystemOps(ex, "Caught in Http404ErrorPage" + src);
    }
}