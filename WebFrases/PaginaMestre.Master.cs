using System;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class PaginaMestre : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] == null)
                Response.Redirect("~/Login.aspx");
        }
    }
}