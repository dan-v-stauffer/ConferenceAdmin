using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pinger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl_DateTime.Text = "Last updated: " + DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt");
        }
    }

    protected void tmr_Main_Tick(object sender, EventArgs e)
    {
        lbl_DateTime.Text = "Last updated: " +  DateTime.Now.ToString("dddd, MMMM d, yyyy h:mm:ss tt");
    }

}