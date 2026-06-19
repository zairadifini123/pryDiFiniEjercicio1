using pryDiFiniEjercicio1;
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
    public partial class frmGestionDeLibros : Form
    {
        public frmGestionDeLibros()
        {
            InitializeComponent();
        }

        clsLibros Libros = new clsLibros();
      
        private void lnkInformacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInformacionEstudiante frmInformacionEstudiante = new frmInformacionEstudiante();
            frmInformacionEstudiante.ShowDialog();
        }

        private void frmGestionDeLibros_Load(object sender, EventArgs e)
        {
            Libros.GuardarDatos(cmbCategorias);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //Valida que el usuario seleccione un rubro
            if (cmbCategorias.SelectedIndex != -1)
            {
                //Guarda en una nueva variable el total del valor de stock para mostrarlo en el label
                Decimal Total;
                Total = Libros.CargarDatosGrilla(dgvGrilla, cmbCategorias.Text);
                lblTotalStock.Text = Total.ToString("0.00");

                Int32 Cantidad;
                Cantidad = Libros.ContarLibrosPorCategoria(cmbCategorias.Text);
                lblCantidadLibros.Text = Cantidad.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una categoria");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog objArchivo = new SaveFileDialog();

            objArchivo.Title = "Seleccione carpeta y escriba nombre de archivo";
            objArchivo.RestoreDirectory = true;
            objArchivo.Filter = "Archivo separado por coma (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";

            if (objArchivo.ShowDialog() == DialogResult.OK)
            {
                Libros.ExportarDatos(dgvGrilla, objArchivo.FileName);

                MessageBox.Show("El reporte se generó correctamente");
            } 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (prtVentana.ShowDialog() == DialogResult.OK)
                {
                    prtDocumento.PrinterSettings = prtVentana.PrinterSettings;
                    prtDocumento.Print();

                    MessageBox.Show("Reporte Impreso Correctamente",
                                    "Impresión",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    this.Focus(); // Para que vuelva al formulario
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prtDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            clsLibros x = new clsLibros();
            x.ImprimirLibros(e);
        }
    }
}
