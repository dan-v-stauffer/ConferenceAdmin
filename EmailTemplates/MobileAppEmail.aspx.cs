using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MobileAppEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string confirmationCode = Request.QueryString["confCode"];
        lbl_ConfirmationCode.Text = confirmationCode;
    }
}