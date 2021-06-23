using System;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class Frase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AtualizarGrid();
            if(!IsPostBack)
            {
                this.AtualizarAutor();
                this.AtualizarCategoria();
            }
        }
        private void AtualizarGrid()
        {
            DALFrase fraseDAL = new DALFrase();
            gvDados.DataSource = fraseDAL.Listar();
            gvDados.DataBind();
        }

        private void AtualizarAutor()
        {
            DALAutor autorDAL = new DALAutor();
            ddlAutor.DataSource = autorDAL.Listar();
            ddlAutor.DataTextField = "nome";
            ddlAutor.DataValueField = "id";
            ddlAutor.DataBind();
        }

        private void AtualizarCategoria()
        {
            DALCategoria categoriaDAL = new DALCategoria();
            ddlCategoria.DataSource = categoriaDAL.Listar();
            ddlCategoria.DataTextField = "categoria";
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataBind();
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtFrase.Text = "";
            ddlAutor.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
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
                DALFrase fraseDAL = new DALFrase();
                ModeloFrase frase = new ModeloFrase();
                string msg = "";
                frase.Texto = txtFrase.Text;
                frase.Autor = Convert.ToInt32(ddlAutor.SelectedValue);
                frase.Categoria = Convert.ToInt32(ddlCategoria.SelectedValue);

                if (btnSalvar.Text == "Inserir")
                {
                    fraseDAL.Inserir(frase);
                    msg = $"<script>alert('Inserido com sucesso! Código do registro gerado: {frase.Id}');</script>";
                }
                else
                {
                    frase.Id = Convert.ToInt32(txtId.Text);
                    fraseDAL.Alterar(frase);
                    msg = $"<script>alert('Alterado com sucesso! Código do registro alterado: {frase.Id}');</script>";
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

                DALFrase fraseDAL = new DALFrase();
                ModeloFrase frase = fraseDAL.ObterPorId(cod);

                txtId.Text = frase.Id.ToString();
                txtFrase.Text = frase.Texto;
                ddlAutor.SelectedValue = frase.Autor.ToString();
                ddlCategoria.SelectedValue = frase.Categoria.ToString();
                btnSalvar.Text = "Alterar";
            }
            catch (Exception ex)
            {
                Response.Write("ERRO: " + ex.Message);
            }
        }

        protected void gvDados_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[0].Text);

            DALFrase fraseDAL = new DALFrase();
            fraseDAL.Excluir(cod);
            this.AtualizarGrid();
            this.LimparCampos();
        }
    }
}