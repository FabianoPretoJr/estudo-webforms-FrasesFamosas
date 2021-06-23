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
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            DALUsuario usuarioDAL = new DALUsuario();
            gvDados.DataSource = usuarioDAL.Listar();
            gvDados.DataBind();
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            btnSalvar.Text = "Inserir";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimparCampos();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DALUsuario usuarioDAL = new DALUsuario();
                ModeloUsuario usuario = new ModeloUsuario();
                string msg = "";
                usuario.Nome = txtNome.Text;
                usuario.Email = txtEmail.Text;
                usuario.Senha = txtSenha.Text;

                ModeloUsuario validaEmail = usuarioDAL.ObterPorEmail(usuario.Email);

                if (btnSalvar.Text == "Inserir")
                {
                    if (validaEmail.Email == null)
                    {
                        usuarioDAL.Inserir(usuario);
                        msg = $"<script>alert('Inserido com sucesso! Código do registro gerado: {usuario.Id}');</script>";
                    }
                    else
                        msg = $"<script>alert('E-mail já existente na base de dados!');</script>";
                }
                else
                {
                    usuario.Id = Convert.ToInt32(txtId.Text);
                    if (validaEmail.Email != null && validaEmail.Id == usuario.Id)
                    {                      
                        usuarioDAL.Alterar(usuario);
                        msg = $"<script>alert('Alterado com sucesso! Código do registro alterado: {usuario.Id}');</script>";
                    }
                    else
                        msg = $"<script>alert('E-mail já existente na base de dados!');</script>";
                }

                Response.Write(msg);
                this.AtualizarGrid();
                this.LimparCampos();
            }
            catch (Exception ex)
            {
                Response.Write("ERRO: " + ex.Message);
            }
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = gvDados.SelectedIndex;
                int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

                DALUsuario usuarioDAL = new DALUsuario();
                ModeloUsuario usuario = usuarioDAL.ObterPorId(cod);

                txtId.Text = usuario.Id.ToString();
                txtNome.Text = usuario.Nome;
                txtEmail.Text = usuario.Email;
                txtSenha.Text = usuario.Senha;
                btnSalvar.Text = "Alterar";
            }
            catch (Exception ex)
            {
                Response.Write("ERRO: " + ex.Message);
            }
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

            DALUsuario usuarioDAL = new DALUsuario();
            usuarioDAL.Excluir(cod);
            this.AtualizarGrid();
            this.LimparCampos();
        }
    }
}