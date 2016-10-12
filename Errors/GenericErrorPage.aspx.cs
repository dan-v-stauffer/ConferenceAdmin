using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConferenceLibrary;


partial class Errors_GenericErrorPage : System.Web.UI.Page
{
    protected Exception ex = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the last error from the server
        Exception ex = Server.GetLastError();

        string src = (!Request.QueryString["aspxerrorpath"].Equals(null)) ? Request.QueryString["aspxerrorpath"] : string.Empty;


        // Log the exception and notify system operators
        ExceptionUtility.LogException(ex, src);
        ExceptionUtility.NotifySystemOps(ex, src);

        // Create a safe message
        string safeMsg = "A problem has occurred on the Engineering Conference 2014 Registration Website." +
            "If you were in the middle of registering, your data may have been saved to a certain point. Restart your browser and try again.<p>" 
            + "The system administrator has been notified of this error.";

        // Show Inner Exception fields for local access
        if (ex.InnerException != null)
        {
            innerTrace.Text = ex.InnerException.StackTrace;
            InnerErrorPanel.Visible = Request.IsLocal;
            innerMessage.Text = ex.InnerException.Message;
        }
        // Show Trace for local access
        if (Request.IsLocal)
            exTrace.Visible = true;
        else
            ex = new Exception(safeMsg, ex);

        // Fill the page fields
        exMessage.Text = ex.Message;
        exTrace.Text = ex.StackTrace;

        // Log the exception and notify system operators
        ExceptionUtility.LogException(ex, src);
        ExceptionUtility.NotifySystemOps(ex, src);

        // Clear the error from the server
        Server.ClearError();
    }
}