using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DALFrase
    {
        Conexao con = new Conexao();

        public void Inserir(ModeloFrase frase)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"INSERT INTO Frases (Frase, Autor, Categoria) VALUES (@frase, @autor, @categoria); SELECT @@IDENTITY;";
                cmd.Parameters.AddWithValue("@frase", frase.Texto);
                cmd.Parameters.AddWithValue("@autor", frase.Autor);
                cmd.Parameters.AddWithValue("@categoria", frase.Categoria);

                frase.Id = Convert.ToInt32(cmd.ExecuteScalar());
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

        public void Alterar(ModeloFrase frase)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"UPDATE Frases SET Frase = @frase, Autor = @autor, Categoria = @categoria WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", frase.Id);
                cmd.Parameters.AddWithValue("@frase", frase.Texto);
                cmd.Parameters.AddWithValue("@autor", frase.Autor);
                cmd.Parameters.AddWithValue("@categoria", frase.Categoria);

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

                cmd.CommandText = @"DELETE FROM Frases WHERE Id = @id;";
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
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT f.*, a.nome as 'nome autor', c.categoria as 'nome categoria' 
                                                         FROM Frases f
                                                             INNER JOIN Autores a
	                                                             ON  f.autor = a.id
                                                             INNER JOIN Categorias c
	                                                             ON f.categoria = c.id;", con.Conectar());
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
                SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM Frases WHERE frase LIKE '%{valor}%';", con.Conectar());
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

        public ModeloFrase ObterPorId(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con.Conectar();

                cmd.CommandText = @"SELECT * FROM Frases WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                ModeloFrase frase = new ModeloFrase();

                if (dr.HasRows)
                {
                    dr.Read();
                    frase.Id = Convert.ToInt32(dr["Id"]);
                    frase.Texto = dr["Frase"].ToString();
                    frase.Autor = Convert.ToInt32(dr["Autor"]);
                    frase.Categoria = Convert.ToInt32(dr["Categoria"]);
                    dr.Close();
                }

                return frase;
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