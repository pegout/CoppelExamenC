using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using examen.BLL;

namespace examen
{
    public partial class Usuario : Form
    {
        bool editando = false;
        int usuarioid;
        int comentarioid;

        // ----------------------- METODOS ----------------------- //
        public Usuario()
        {
            InitializeComponent();
        }

        private void cargarUsuarios()
        {
            UsuarioBLL obj = new UsuarioBLL();
            this.dgvUsuario.DataSource = obj.obtenerUsuario();
            dgvUsuario.Columns["UsuarioID"].Width = 60;
            dgvUsuario.Columns["Clave"].Width = 40;
            dgvUsuario.Columns["Nombre"].Width = 150;
            dgvUsuario.Columns["Apellido"].Width = 150;
            dgvUsuario.Columns["FechaNacimiento"].Width = 80;
            dgvUsuario.Columns["FechaNacimiento"].HeaderText = "Fecha Nacimiento";
            
            if (dgvUsuario.Rows.Count > 0)
                dgvUsuario.Rows[0].Selected = false;
            else
                btnEliminar.Enabled = false;

            foreach (DataGridViewColumn column in dgvUsuario.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            limpiar();
        }

        private void cargarComentarios(int usuarioid)
        {
            ComentarioBLL obj = new ComentarioBLL();
            dgvComentario.DataSource = obj.obtenerComentario(usuarioid);
            dgvComentario.Columns["ComentarioID"].Width = 80;
            dgvComentario.Columns["Descripcion"].Width = 250;
            dgvComentario.Columns["Descripcion"].HeaderText = "Descripción";
            dgvComentario.Columns["UsuarioID"].Visible = false;
            editando = false;
            txtComentario.Text = String.Empty;

            foreach (DataGridViewColumn column in dgvComentario.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dgvComentario.Rows.Count > 0)
            {
                dgvComentario.Rows[0].Selected = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                btnEliminar.Enabled = true;
                btnEliminarComentario.Enabled = false;
            }
        }

        private void limpiar()
        {
            if (dgvUsuario.Rows.Count > 0)
                dgvUsuario.Rows[0].Selected = false;

            dgvComentario.DataSource = null;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEliminarComentario.Enabled = false;
            txtComentario.Text = String.Empty;
            editando = false;
            usuarioid = 0;
            comentarioid = 0;
        }

        // ----------------------- EVENTOS ----------------------- //
        private void Usuarios_Load(object sender, EventArgs e)
        {
            try
            {
                cargarUsuarios();
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al obtener los usuarios.","Error!");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ComentarioBLL comentario = new ComentarioBLL();
            comentario.ComentarioID = comentarioid;
            comentario.Descripcion = txtComentario.Text;
            comentario.UsuarioID = usuarioid;

            try
            {
                if (txtComentario.Text.Length > 0)
                {
                    if (editando)
                        comentario.actualizarComentario(comentario);
                     else
                        comentario.guardarComentario(comentario);
                }
                else
                    MessageBox.Show("Por favor, ingrese un comentario.", "Error!");
                
                cargarComentarios(usuarioid);
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al intentar guardar el comentario.", "Error!");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuario = new UsuarioBLL();
            DialogResult eliminar;

            try
            {
                if (usuarioid > 0)
                {
                    eliminar = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Eliminar Usuario", MessageBoxButtons.YesNo);
                    if (eliminar == DialogResult.Yes)
                    {
                        usuario.eliminarUsuarioLogico(usuarioid);
                        cargarUsuarios();
                    }
                }
                else
                    MessageBox.Show("Por favor, seleccione un usuario a eliminar.", "Atención!");
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al intentar eliminar el usuario.", "Error!");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            altaUsuario alta = new altaUsuario();
            if (alta.ShowDialog() != DialogResult.Cancel)
                cargarUsuarios();
        }

        private void dgvUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuario.SelectedRows.Count > 0 && dgvUsuario.RowCount > 0)
            {
                usuarioid = Convert.ToInt32(dgvUsuario.SelectedRows[0].Cells[0].Value.ToString());
                cargarComentarios(usuarioid);
                btnGuardar.Enabled = true;
                if (dgvComentario.Rows.Count == 0)
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;
            }
        }

        private void dgvComentario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvComentario.SelectedRows.Count >0 && dgvComentario.RowCount > 0)
            {
                comentarioid = Convert.ToInt32(dgvComentario.SelectedRows[0].Cells[0].Value.ToString());
                editando = true;
                txtComentario.Text = dgvComentario.SelectedRows[0].Cells[1].Value.ToString();
                btnEliminarComentario.Enabled = true;
            }
        }

        private void btnEliminarComentario_Click(object sender, EventArgs e)
        {
            ComentarioBLL comentario = new ComentarioBLL();
            DialogResult eliminar;

            try
            {
                if (editando && usuarioid > 0)
                {
                    eliminar = MessageBox.Show("¿Está seguro que desea eliminar este comentario?", "Eliminar Comentario", MessageBoxButtons.YesNo);
                    if (eliminar == DialogResult.Yes)
                    {
                        comentario.eliminarComentarioLogico(comentarioid);
                        cargarComentarios(usuarioid);
                    }
                }
                else
                    MessageBox.Show("Por favor, seleccione un comentario a eliminar.", "Atención!");
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al intentar eliminar el comentario.", "Error!");
            }
        }
    }
}