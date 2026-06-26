using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDiFiniEjercicio1
{
    public partial class frmAgregarLibro : Form
    {
        public frmAgregarLibro()
        {
            InitializeComponent();
        }

        clsLibros Libros = new clsLibros(); 

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtAutor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la tecla
            }
        }

        private void frmAgregarLibro_Load(object sender, EventArgs e)
        {
            Libros.GuardarDatos(cmbCategorias);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Libros.Codigo = Convert.ToInt32(txtCodigo.Text);
            Libros.Titulo = txtTitulo.Text;
            Libros.Autor = txtAutor.Text;
            Libros.Precio = Convert.ToDecimal(txtPrecio.Text);
            Libros.Stock = Convert.ToInt32(txtStock.Text);

            Libros.IdCategoria = Convert.ToInt32(cmbCategorias.SelectedValue);

            Libros.AgregarLibro();

            MessageBox.Show("Datos grabados");

            txtCodigo.Clear();
            txtTitulo.Clear();
            txtAutor.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

            cmbCategorias.SelectedIndex = 0;
        }
    }
}
