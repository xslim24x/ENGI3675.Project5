// <copyright file="adduser.aspx.cs" company="Lakehead">
//    Lakehead.com. All rights reserved.
// </copyright>
// <author>Slim and Leví</author>
namespace UserLogin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using UserLogin.App_Code.ServerConn;
  
    /// <summary>
    /// Public class index
    /// </summary>
    public partial class index : System.Web.UI.Page
    {     
        /// <summary>
        /// It's giving the action the the Button_Insert used in the ASP page
        /// </summary>
        /// <param name="sender"> object sender</param>
        /// <param name="e"> EventArgs e </param>
        protected void Button_Insert(object sender, EventArgs e)
        {
            ServerConn.SafeAdd(this.Username.Text, this.Password.Text);

            this.Username.Text = string.Empty;
            this.Password.Text = string.Empty;
        }
    }
}