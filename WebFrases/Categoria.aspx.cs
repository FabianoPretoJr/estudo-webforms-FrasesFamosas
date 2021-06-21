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
    public partial class Categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AtualizarGrid();
        }
        private void AtualizarGrid()
        {
            DALCategoria categoriaDAL = new DALCategoria();
            gvDados.DataSource = categoriaDAL.Listar();
            gvDados.DataBind();
        }
        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            btnSalvar.Text = "Inserir";
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DALCategoria categoriaDAL = new DALCategoria();
                ModeloCategoria categoria = new ModeloCategoria();
                string msg = "";
                categoria.Nome = txtNome.Text;
                if (btnSalvar.Text == "Inserir")
                {
                    categoriaDAL.Inserir(categoria);
                    msg = $"<script>alert('Inserido com sucesso! Código do registro gerado: {categoria.Id}');</script>";
                }
                else
                {
                    categoria.Id = Convert.ToInt32(txtId.Text);
                    categoriaDAL.Alterar(categoria);
                    msg = $"<script>alert('Alterado com sucesso! Código do registro alterado: {categoria.Id}');</script>";
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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimparCampos();
        }
        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

            DALCategoria categoriaDAL = new DALCategoria();
            categoriaDAL.Excluir(cod);
            this.AtualizarGrid();
            this.LimparCampos();
        }
        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = gvDados.SelectedIndex;
                int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

                DALCategoria categoriaDAL = new DALCategoria();
                ModeloCategoria categoria = categoriaDAL.ObterPorId(cod);

                txtId.Text = categoria.Id.ToString();
                txtNome.Text = categoria.Nome;
                btnSalvar.Text = "Alterar";
            }
            catch (Exception ex)
            {
                Response.Write("ERRO: " + ex.Message);
            }
        }
    }
}