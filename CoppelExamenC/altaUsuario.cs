using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using examen.BLL;

namespace examen
{
    public partial class altaUsuario : Form
    {
        public altaUsuario()
        {
            InitializeComponent();
        }

        private void limpiarTextos()
        {
            txtClave.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            dtpFechaNacimiento.Text = DateTime.Today.ToString();
            txtClave.Focus();
        }

        private bool validaDatos()
        {
            if (txtClave.Text.Length < 3 || txtNombre.Text.Length < 3 || txtApellido.Text.Length < 3)
            {
                MessageBox.Show("Por favor, verifique que todos los campos tengan al menos 3 caracteres.", "Atención!");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarTextos();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuario = new UsuarioBLL();
            usuario.Clave = txtClave.Text;
            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.FechaNacimiento = dtpFechaNacimiento.Text;

            try
            {
                if (validaDatos())
                {
                    usuario.guardarUsuario(usuario);
                    limpiarTextos();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al intentar guardar.", "Error!");
            }
        }
    }
}
