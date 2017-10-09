using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using examen.BLL;

namespace examen.DAL
{
    class ComentarioDAL
    {
        public string ConString = ConfigurationManager.ConnectionStrings["examenConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection();
        DataTable dt = new DataTable();
        SqlCommand cmd;

        public DataTable obtenerComentario(int usuarioid)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("SELECT ComentarioID, Descripcion, UsuarioID " +
                                "FROM Comentario WHERE UsuarioID = @usuarioid AND Estatus = 1", con);
            cmd.Parameters.AddWithValue("usuarioid", usuarioid);
            try
            {
                SqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        public void guardarComentario(ComentarioBLL comentario)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("INSERT INTO Comentario(Descripcion, UsuarioID)" +
                                            " VALUES(@descripcion, @usuarioid)", con);
            cmd.Parameters.AddWithValue("descripcion", comentario.Descripcion);
            cmd.Parameters.AddWithValue("usuarioid", comentario.UsuarioID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void actualizarComentario(ComentarioBLL comentario)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("UPDATE Comentario SET Descripcion= @descripcion, UsuarioID= @usuarioid " +
                                "WHERE ComentarioID = @comentarioid", con);
            cmd.Parameters.AddWithValue("descripcion", comentario.Descripcion);
            cmd.Parameters.AddWithValue("usuarioid", comentario.UsuarioID);
            cmd.Parameters.AddWithValue("comentarioid", comentario.ComentarioID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void eliminarComentarioLogico(int comentarioid)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("UPDATE Comentario SET Estatus= 0 WHERE ComentarioID = @comentarioid", con);
            cmd.Parameters.AddWithValue("comentarioid", comentarioid);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
}
