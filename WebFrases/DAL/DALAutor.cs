using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DALAutor
    {
        Conexao con = new Conexao();

        public void Inserir(Autor autor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"INSERT INTO Autores (Nome, Foto) VALUES (@nome, @foto); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@nome", autor.Nome);
                cmd.Parameters.AddWithValue("@foto", autor.Foto);

                autor.Id = Convert.ToInt32(cmd.ExecuteScalar());
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

        public void Alterar(Autor autor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"UPDATE Autores SET Nome = @nome, Foto = @foto WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", autor.Id);
                cmd.Parameters.AddWithValue("@nome", autor.Nome);
                cmd.Parameters.AddWithValue("@foto", autor.Foto);

                cmd.ExecuteNonQuery();
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

        public void Excluir(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"DELETE FROM Autores WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
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

        public DataTable Listar()
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT * FROM Autores;", con.Conectar());
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

        public DataTable Listar(string valor)
        {
            try
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Autores WHERE Nome LIKE '%{valor}%';", con.Conectar());
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

        public Autor ObterPorId(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"SELECT * FROM Autores WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                Autor autor = new Autor();

                if (dr.HasRows)
                {
                    dr.Read();
                    autor.Id = Convert.ToInt32(dr["Id"]);
                    autor.Nome = dr["Nome"].ToString();
                    autor.Foto = dr["Foto"].ToString();
                    dr.Close();
                }

                return autor;
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
    }
}