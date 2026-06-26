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
    public partial class frmBuscar : Form
    {
        public frmBuscar()
        {
            InitializeComponent();
        }

        clsLibros Libros = new clsLibros();
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodigoBuscar.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }

            Int32 Codigo = Convert.ToInt32(txtCodigoBuscar.Text);

            Libros.BuscarLibro(Codigo);

            if (Libros.Codigo != Codigo)
            {
                MessageBox.Show("Libro no existente");
                txtCodigoBuscar.Text = "";
                lblTitulo.Text = "";
                lblAutor.Text = "";
                lblPrecio.Text = "";
                lblStock.Text = "";
                lblCategoria.Text = "";
            }
            else
            {
                txtCodigoBuscar.Text = "";
                lblTitulo.Text = Libros.Titulo;
                lblAutor.Text = Libros.Autor;
                lblPrecio.Text = Libros.Precio.ToString();
                lblStock.Text = Libros.Stock.ToString();
                lblCategoria.Text = Libros.IdCategoria.ToString(); 

            }
        }
    }
}
