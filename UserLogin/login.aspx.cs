// <copyright file="login.aspx.cs" company="Lakehead">
//     Lakehead.com. All rights reserved.
// </copyright>
// <author>Slim and Leví</author>
namespace UserLogin
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using App_Code.ServerConn;

    /// <summary>
    /// class login 
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
       /// <summary>
       /// Protected void Page_Load 
       /// </summary>
       /// <param name="sender"> Object Sender</param>
       /// <param name="e"> EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection nvc = Request.Form;
            if (ServerConn.Authenticated(nvc["username"], nvc["password"]))
            {
                this.postresults.InnerHtml = "<h1> Authenticated!</h1>";
            }        
        }
    }
}