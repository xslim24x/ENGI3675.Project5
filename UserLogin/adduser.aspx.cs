
namespace UserLogin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using UserLogin.App_Code.ServerConn;
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Insert(object sender, EventArgs e)
        {
            ServerConn.SafeAdd(this.Username.Text, this.Password.Text);

            Username.Text = string.Empty;
            Password.Text = string.Empty;
        }
    }
}