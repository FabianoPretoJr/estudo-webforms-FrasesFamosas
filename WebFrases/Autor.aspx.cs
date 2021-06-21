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
    public partial class Autor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AtualizarGrid();
        }
        private void AtualizarGrid()
        {
            DALAutor autorDAL = new DALAutor();
            gvDados.DataSource = autorDAL.Listar();
            gvDados.DataBind();
        }
        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            btnSalvar.Text = "Inserir";
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimparCampos();
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

            DALAutor autorDAL = new DALAutor();
            autorDAL.Excluir(cod);
            this.AtualizarGrid();
            this.LimparCampos();
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = gvDados.SelectedIndex;
                int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

                DALAutor autorDAL = new DALAutor();
                ModeloAutor autor = autorDAL.ObterPorId(cod);

                txtId.Text = autor.Id.ToString();
                txtNome.Text = autor.Nome;
                btnSalvar.Text = "Alterar";
            }
            catch (Exception ex)
            {
                Response.Write("ERRO: " + ex.Message);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DALAutor autorDAL = new DALAutor();
                ModeloAutor autor = new ModeloAutor();
                string msg = "";
                autor.Nome = txtNome.Text;
                autor.Foto = "";
                if (btnSalvar.Text == "Inserir")
                {
                    autorDAL.Inserir(autor);
                    msg = $"<script>alert('Inserido com sucesso! Código do registro gerado: {autor.Id}');</script>";
                }
                else
                {
                    autor.Id = Convert.ToInt32(txtId.Text);
                    autorDAL.Alterar(autor);
                    msg = $"<script>alert('Alterado com sucesso! Código do registro alterado: {autor.Id}');</script>";
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
    }
}