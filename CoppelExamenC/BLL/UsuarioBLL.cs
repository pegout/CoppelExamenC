using System;
using System.Collections.Generic;
using System.Data;
using examen.DAL;

namespace examen.BLL
{
    class UsuarioBLL
    {

        public int UsuarioID { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }

        public UsuarioBLL()
        {
        }

        public DataTable obtenerUsuario()
        {
            try
            {
                UsuarioDAL obj = new UsuarioDAL();
                return obj.obtenerUsuario();
            }
            catch
            {
                throw;
            }
        }

        public void guardarUsuario(UsuarioBLL usuario)
        {
            try
            {
                UsuarioDAL obj = new UsuarioDAL();
                obj.guardarUsuario(usuario);
            }
            catch
            {
                throw;
            }
        }

        public void actualizarUsuario(UsuarioBLL usuario)
        {
            try
            {
                UsuarioDAL obj = new UsuarioDAL();
                obj.actualizarUsuario(usuario);
            }
            catch
            {
                throw;
            }
        }

        public void eliminarUsuarioLogico(int usuarioid)
        {
            try
            {
                UsuarioDAL obj = new UsuarioDAL();
                obj.eliminarUsuarioLogico(usuarioid);
            }
            catch
            {
                throw;
            }
        }

        public void eliminarUsuarioFisico(int usuarioid)
        {
            try
            {
                UsuarioDAL obj = new UsuarioDAL();
                obj.eliminarUsuarioFisico(usuarioid);
            }
            catch
            {
                throw;
            }
        }
    }
}
