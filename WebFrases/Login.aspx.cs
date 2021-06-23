using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            DALUsuario usuarioDAL = new DALUsuario();
            ModeloUsuario usuario = usuarioDAL.ObterPorEmail(email);

            if (email == usuario.Email && senha == usuario.Senha)
            {
                Session["id"] = usuario.Id;
                Session["nome"] = usuario.Nome;
                Session["email"] = usuario.Email;
                Response.Redirect("~/Default.aspx");
            }
            else
                Response.Write("<script>alert('Acesso negado!')</script>");
        }
    }
}