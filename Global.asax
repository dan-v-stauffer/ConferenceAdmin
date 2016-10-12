<%@ Application Language="C#" %>

<script runat="server">
    

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        HttpContext con = HttpContext.Current;
        string src = con.Request.Url.ToString();
        
        Exception exc = Server.GetLastError();
        // Log the exception and notify system operators
        
        ConferenceLibrary.ExceptionUtility.LogException(exc, src);
        ConferenceLibrary.ExceptionUtility.NotifySystemOps(exc, src);


        // Handle HTTP errors
        if (exc.GetType() == typeof(HttpException))
        {
            //Redirect HTTP errors to HttpError page
            Response.Redirect("~/Errors/HttpErrorPage.aspx?src=" + src);
        }
        else if (exc.GetType() == typeof(UnauthorizedAccessException))
        {
            Response.Redirect("~/Errors/AccessUnauthorizedErrorPage.aspx");
        }

        // Clear the error from the server
       // Server.ClearError();
    }

    void Session_Start(object sender, EventArgs e) 
    {
            // Code that runs when a new session is started
            Application["Conference"] = ConferenceLibrary.Conference.Instance;

            ConferenceLibrary.KTConferenceUser user = new ConferenceLibrary.KTConferenceUser(
                                new DataUtilities.KTActiveDirectory.KTActiveDirectoryUser(
                                    new DataUtilities.KTActiveDirectory.KTLogin(System.Web.HttpContext.Current.User.Identity.Name)));
            Session["user"] = user;
            Session["conferenceID"] = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ConferenceID"].ToString());

            object retval = null;

            DataUtilities.SQLServer.WebDataUtility.Instance.webAppCmd("sp_LogSession",
                new DataUtilities.SQLServer.GenericCmdParameter[] { 
                    new DataUtilities.SQLServer.GenericCmdParameter("@sessionID", Session.SessionID), 
                    new DataUtilities.SQLServer.GenericCmdParameter("@sessionUser", user.Email),
                    new DataUtilities.SQLServer.GenericCmdParameter("@sessionStart", DateTime.Now) }, ref retval);

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        try
        {
            object retval = null;

            String connStr = ConfigurationManager.ConnectionStrings["EngConferenceDB"].ConnectionString;
            System.Data.SqlClient.SqlConnection cxn = null;

            using (cxn = new System.Data.SqlClient.SqlConnection(connStr))
            {
                cxn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("sp_CloseSession");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionID", Session.SessionID);
                cmd.Parameters.Add("@retval", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
        }
    }
       
</script>
