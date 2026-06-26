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
    public partial class frmEditarLibro : Form
    {
        public frmEditarLibro()
        {
            InitializeComponent();
        }

        clsLibros Libros = new clsLibros();
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Int32 Codigo = Convert.ToInt32(txtCodigo.Text);
            Libros.BuscarLibro(Codigo);

            if (Libros.Codigo == Codigo)
            {

                lblTitulo.Text = Libros.Titulo;
                lblAutor.Text = Libros.Autor;
                lblCategoria.Text = Libros.IdCategoria.ToString();
                txtPrecio.Text = Libros.Precio.ToString();
                txtStock.Text = Libros.Stock.ToString();
            }
            else
            {
                lblTitulo.Text = "";
                lblAutor.Text = "";
                lblCategoria.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                MessageBox.Show("Libro no existente");


            }
            txtPrecio.ReadOnly = true;
            txtStock.ReadOnly = true;
            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void frmEditarLibro_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            txtPrecio.ReadOnly = false;
            txtStock.ReadOnly = false;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int Codigo = Convert.ToInt32(txtCodigo.Text);

            Libros.Precio = Convert.ToDecimal(txtPrecio.Text);
            Libros.Stock = Convert.ToInt32(txtStock.Text);
            Libros.Modificar(Codigo);

            MessageBox.Show("Se modificaron los datos correctamente");
            Limpiar();
        }

        private void Limpiar()
        {
            txtPrecio.Text = "";
            txtStock.Text = "";
            lblTitulo.Text = "";
            lblAutor.Text = "";
            lblCategoria.Text = "";
            txtPrecio.ReadOnly = true;
            txtStock.ReadOnly = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int Codigo = Convert.ToInt32(txtCodigo.Text);

            Libros.Eliminar(Codigo);
            MessageBox.Show("Se eliminaron los datos correctamente");
            Limpiar();
        }
    }
}
