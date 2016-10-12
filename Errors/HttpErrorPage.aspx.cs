using ConferenceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Errors_HttpErrorPage : System.Web.UI.Page
{
    protected HttpException ex = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        string src = (!Request.QueryString["src"].Equals(null))?Request.QueryString["src"]:string.Empty;
            
        ex = (HttpException)Server.GetLastError();
        int httpCode = ex.GetHttpCode();

        // Filter for Error Codes and set text
        if (httpCode == 404)
            Server.TransferRequest("Http404ErrorPage.aspx?" + (!src.Equals(string.Empty)?"src="+src:string.Empty));
        else if (httpCode >= 400 && httpCode < 500)
            ex = new HttpException
                (httpCode, "A web client  has occurred on the Engineering Conference 2014 Registration Website." +
                "If you were in the middle of registering, your data may have been saved to a certain point. Restart your browser and try again.<p>"
                + "The system administrator has been notified of this error.", ex);
        else if (httpCode > 499)
            ex = new HttpException
                (httpCode, "A server error has occurred on the Engineering Conference 2014 Registration Website." +
                "If you were in the middle of registering, your data may have been saved to a certain point. Restart your browser and try again.<p>"
                + "The system administrator has been notified of this error.", ex);
        else
            ex = new HttpException
                (httpCode, "An unexpected Http error has occurred on the Engineering Conference 2014 Registration Website." +
                "If you were in the middle of registering, your data may have been saved to a certain point. Restart your browser and try again.<p>"
                + "The system administrator has been notified of this error.", ex);
        // Log the exception and notify system operators

        ExceptionUtility.LogException(ex, src);
        ExceptionUtility.NotifySystemOps(ex, src);

        // Fill the page fields
        exMessage.Text = ex.Message;
        exTrace.Text = ex.StackTrace;

        // Show Inner Exception fields for local access
        if (ex.InnerException != null)
        {
            innerTrace.Text = ex.InnerException.StackTrace;
            InnerErrorPanel.Visible = Request.IsLocal;
            innerMessage.Text = string.Format("HTTP {0}: {1}",
              httpCode, ex.InnerException.Message);
        }
        // Show Trace for local access
        exTrace.Visible = Request.IsLocal;

        // Clear the error from the server
        Server.ClearError();
    }
}