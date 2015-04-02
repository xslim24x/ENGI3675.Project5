
namespace UserLogin
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Net;
    using App_Code.ServerConn;
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection nvc = Request.Form;
            if (ServerConn.Authenticated(nvc["username"], nvc["password"]))
            {
                postresults.InnerHtml = "<h1> Authenticated!</h1>";
            }

                
        }

    }
}