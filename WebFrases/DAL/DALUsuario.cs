using System;
using System.Data;
using System.Data.SqlClient;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DALUsuario
    {
        Conexao con = new Conexao();

        public void Inserir(Usuario usuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@nome, @email, @senha); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);

                usuario.Id = Convert.ToInt32(cmd.ExecuteScalar());
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

        public void Alterar(Usuario usuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"UPDATE Usuarios SET Nome = @nome, Email = @email, Senha = @senha WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", usuario.Id);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);

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

                cmd.CommandText = @"DELETE FROM Usuarios WHERE Id = @id;";
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
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT * FROM Usuarios;", con.Conectar());
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
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Usuarios WHERE Nome LIKE '%{valor}%';", con.Conectar());
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

        public Usuario ObterPorId(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"SELECT * FROM Usuarios WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                Usuario usuario = new Usuario();

                if (dr.HasRows)
                {
                    dr.Read();
                    usuario.Id = Convert.ToInt32(dr["Id"]);
                    usuario.Nome = dr["Nome"].ToString();
                    usuario.Email = dr["Email"].ToString();
                    usuario.Senha = dr["Senha"].ToString();
                    dr.Close();
                }

                return usuario;
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