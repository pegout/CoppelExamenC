using System;
using System.Collections.Generic;
using System.Data;
using examen.DAL;

namespace examen.BLL
{
    class ComentarioBLL
    {
        public int ComentarioID { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioID { get; set; }

        public ComentarioBLL()
        {
        }

        public DataTable obtenerComentario(int usuarioid)
        {
            try
            {
                ComentarioDAL obj = new ComentarioDAL();
                return obj.obtenerComentario(usuarioid);
            }
            catch
            {
                throw;
            }
        }

        public void guardarComentario(ComentarioBLL comentario)
        {
            try
            {
                ComentarioDAL obj = new ComentarioDAL();
                obj.guardarComentario(comentario);
            }
            catch
            {
                throw;
            }
        }

        public void actualizarComentario(ComentarioBLL comentario)
        {
            try
            {
                ComentarioDAL obj = new ComentarioDAL();
                obj.actualizarComentario(comentario);
            }
            catch
            {
                throw;
            }
        }

        public void eliminarComentarioLogico(int usuarioid)
        {
            try
            {
                ComentarioDAL obj = new ComentarioDAL();
                obj.eliminarComentarioLogico(usuarioid);
            }
            catch
            {
                throw;
            }
        }
    }
}
