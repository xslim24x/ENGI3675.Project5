using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserLogin
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Insert(object sender, EventArgs e)
        {
            Stored_Procedure db = new Stored_Procedure();
            var item = new Model_Table();

            item.username = Username.Text;
            item.password = Password.Text;

            Stored_Procedure.INSERT(item);


        }
    }
}