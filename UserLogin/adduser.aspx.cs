// <copyright file="adduser.aspx.cs" company="Lakehead">
//    Lakehead.com. All rights reserved.
// </copyright>
// <author>Slim and Leví</author>
namespace UserLogin
{
    using System;
    using UserLogin.App_Code.ServerConn;
  
    /// <summary>
    /// Public class AddUser
    /// </summary>
    public partial class AddUser : System.Web.UI.Page
    {     
        /// <summary>
        /// It's giving the action the the Button_Insert used in the ASP page
        /// </summary>
        /// <param name="sender"> object sender</param>
        /// <param name="e"> EventArgs e </param>
        protected void Button_Insert(object sender, EventArgs e)
        {
            ServerConn.SpAdd(this.Username.Text, this.Password.Text);

            this.Username.Text = string.Empty;
            this.Password.Text = string.Empty;
        }
    }
}