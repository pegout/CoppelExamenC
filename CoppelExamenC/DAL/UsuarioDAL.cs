using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using examen.BLL;

namespace examen.DAL
{
    class UsuarioDAL
    {
        public string ConString = ConfigurationManager.ConnectionStrings["examenConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection();
        DataTable dt = new DataTable();
        SqlCommand cmd;

        public DataTable obtenerUsuario()
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("SELECT UsuarioID, Clave, Nombre, Apellido, FechaNacimiento " +
                                "FROM Usuario WHERE Estatus = 1", con);
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

        public void guardarUsuario(UsuarioBLL usuario)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("INSERT INTO Usuario(Clave, Nombre, Apellido, FechaNacimiento)" +
                                            " VALUES(@clave, @nombre, @apellido, CONVERT(DATE, @fechanac, 104))", con);
            cmd.Parameters.AddWithValue("clave", usuario.Clave);
            cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("fechanac", usuario.FechaNacimiento);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void actualizarUsuario(UsuarioBLL usuario)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("UPDATE Usuario SET Clave= @clave, Nombre= @nombre, Apellido= @apellido, " +
                                                    "FechaNacimiento= CONVERT(DATE, @fechanac, 104) " +
                                "WHERE UsuarioID = @usuarioid", con);
            cmd.Parameters.AddWithValue("clave", usuario.Clave);
            cmd.Parameters.AddWithValue("nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("fechanac", usuario.FechaNacimiento);
            cmd.Parameters.AddWithValue("usuarioid", usuario.UsuarioID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void eliminarUsuarioLogico(int usuarioid)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("UPDATE Usuario SET Estatus= 0 WHERE UsuarioID = @usuarioid", con);
            cmd.Parameters.AddWithValue("usuarioid", usuarioid);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void eliminarUsuarioFisico(int usuarioid)
        {
            con.ConnectionString = ConString;
            if (ConnectionState.Closed == con.State)
                con.Open();
            cmd = new SqlCommand("DELETE FROM Usuario WHERE UsuarioID = @usuarioid", con);
            cmd.Parameters.AddWithValue("usuarioid", usuarioid);
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
