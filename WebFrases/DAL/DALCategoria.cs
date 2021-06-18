using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DALCategoria
    {
        Conexao con = new Conexao();

        public void Inserir(Categoria categoria)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"INSERT INTO Categorias (Categoria) VALUES (@categoria); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@categoria", categoria.Nome);

                categoria.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }

        public void Alterar(Categoria categoria)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"UPDATE Categorias SET Categoria = @categoria WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", categoria.Id);
                cmd.Parameters.AddWithValue("@categoria", categoria.Nome);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }

        public void Excluir(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"DELETE FROM Categorias WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }

        public DataTable Listar()
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT * FROM Categorias;", con.Conectar());
                da.Fill(tabela);
                return tabela;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }

        public DataTable Listar(string valor)
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Categorias WHERE Categoria LIKE '%{valor}%';", con.Conectar());
                da.Fill(tabela);
                return tabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }

        public Categoria ObterPorId(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"SELECT * FROM Categorias WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();
                
                Categoria categoria = new Categoria();

                if(dr.HasRows)
                {
                    dr.Read();
                    categoria.Id = Convert.ToInt32(dr["Id"]);
                    categoria.Nome = dr["Categoria"].ToString();
                    dr.Close();
                }

                return categoria;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Desconectar();
            }
        }
    }
}