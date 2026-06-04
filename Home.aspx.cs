using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRM
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUsername.Text = Convert.ToString(Session["Username"]);
                if (string.IsNullOrEmpty(lblUsername.Text))
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}