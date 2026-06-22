using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pryDiFiniEjercicio1
{
    internal class clsLibros
    {
        private OleDbConnection conexion = new OleDbConnection(); //Se crea un objeto para que la aplicación se conecte a la base de datos
        private OleDbCommand comando = new OleDbCommand(); //Objeto que almacena y ejecuta instrucciones SQL sobre una base de datos
        private OleDbDataAdapter adaptador = new OleDbDataAdapter(); //Adaptador para transferir datos entre la base de datos

        //Que tipo de base de datos se va a usar y cual es, donde esta
        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Ejercicio1.mdb";

        //Que tablas vamos a usar
        private String Tabla = "Categorias";
        private String Tabla2 = "Libros";
       

        //Variables para calcular etiquetas
        private Decimal total;
        private Int32 cantidad;

        //Variables de Artículos
        private String cod;
        private String tit;
        private String aut;
        private Decimal pre;
        private Int32 sto;
        private Int32 idCat;

        //Propiedades / Funciones para pasar las variables al formulario
        public Decimal Total
        {
            get { return total; }
        }

        public Int32 Cantidad
        {
            get { return cantidad; }
        }

        public Int32 IdCategoria
        {
            get { return idCat; }
            set { idCat = value; }
        }

        public String Codigo
        {
            get { return cod; }
            set { cod = value; }
        }

        public String Titulo
        {
            get { return tit; }
            set { tit = value; }
        }

        public String Autor
        {
            get { return aut; }
            set { aut = value; }
        }

        public Decimal Precio
        {
            get { return pre; }
            set { pre = value; }
        }

        public Int32 Stock
        {
            get { return sto; }
            set { sto = value; }
        }
        public Decimal ValorStock
        {
            get { return pre * sto; }
        }

        public void GuardarDatos(ComboBox Combo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla; // Categorias

                adaptador = new OleDbDataAdapter(comando);

                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                Combo.DataSource = DS.Tables[Tabla];
                Combo.DisplayMember = "NombreCategoria";
                Combo.ValueMember = "IdCategoria";

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        //Carga los datos de la base de datos en el DataGridView según el rubro
        public Decimal CargarDatosGrilla(DataGridView Grilla, String CategoriaBuscada)
        {
            Decimal TotalValorStock = 0;

            try
            {
                Int32 idCategoria = 0;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                //Buscar el Id del rubro seleccionado
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();

                while (DR.Read())
                {
                    if (DR.GetString(1) == CategoriaBuscada)
                    {
                        idCategoria = DR.GetInt32(0);
                    }
                }

                DR.Close();

                //Para leer Libros
                comando.CommandText = Tabla2;
                DR = comando.ExecuteReader();

                Grilla.Rows.Clear();

                while (DR.Read())
                {
                    if (DR.GetInt32(5) == idCategoria)
                    {
                        Decimal ValorStock = DR.GetDecimal(3) * DR.GetInt32(4);

                        TotalValorStock += ValorStock;

                        int i = Grilla.Rows.Add();

                        Grilla.Rows[i].Cells[0].Value = DR.GetString(0);
                        Grilla.Rows[i].Cells[1].Value = DR.GetString(1);
                        Grilla.Rows[i].Cells[2].Value = DR.GetString(2);
                        Grilla.Rows[i].Cells[3].Value = DR.GetDecimal(3);
                        Grilla.Rows[i].Cells[4].Value = DR.GetInt32(4);
                        Grilla.Rows[i].Cells[5].Value = ValorStock;
                    }
                }

                DR.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return TotalValorStock;
        }

        public Int32 ContarLibrosPorCategoria(String CategoriaBuscada)
        {
            Int32 CantidadLibros = 0;

            try
            {
                Int32 idCategoria = 0;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();

                while (DR.Read())
                {
                    if (DR.GetString(1) == CategoriaBuscada)
                    {
                        idCategoria = DR.GetInt32(0);
                    }
                }

                DR.Close();

                comando.CommandText = Tabla2;
                DR = comando.ExecuteReader();

                while (DR.Read())
                {
                    if (DR.GetInt32(5) == idCategoria)
                    {
                        CantidadLibros++;
                    }
                }

                DR.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return CantidadLibros;
        }


        //Método para exportar los datos de la grilla
        public void ExportarDatos(DataGridView Grilla, String NombreArchivo)
        {
            try
            {
                StreamWriter Exportacion = new StreamWriter(NombreArchivo, false, Encoding.UTF8);

                Exportacion.WriteLine("Codigo;Titulo;Autor;Precio;Stock; ValorStock");

                for (Int32 i = 0; i < Grilla.Rows.Count; i++)
                {
                    if (Grilla.Rows[i].Cells[0].Value != null)
                    {
                        Exportacion.Write(Grilla.Rows[i].Cells[0].Value);
                        Exportacion.Write(";");
                        Exportacion.Write(Grilla.Rows[i].Cells[1].Value);
                        Exportacion.Write(";");
                        Exportacion.Write(Grilla.Rows[i].Cells[2].Value);
                        Exportacion.Write(";");
                        Exportacion.Write(Grilla.Rows[i].Cells[3].Value);
                        Exportacion.Write(";");
                        Exportacion.Write(Grilla.Rows[i].Cells[4].Value);
                        Exportacion.Write(";");
                        Exportacion.WriteLine(Grilla.Rows[i].Cells[5].Value);
                    }
                }

                Exportacion.Close();
                Exportacion.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ImprimirLibros(PrintPageEventArgs reporte)
        {
            try
            {
                Font LetraTitulo1 = new Font("Arial", 20);
                Font LetraTitulo2 = new Font("Arial", 10);
                Font LetraTexto = new Font("Arial", 8);

                Int32 f = 200;

                reporte.Graphics.DrawString("Listado de Libros", LetraTitulo1, Brushes.Red, 100, 100);

                // Encabezados
                reporte.Graphics.DrawString("Código", LetraTitulo2, Brushes.Blue, 20, 180);
                reporte.Graphics.DrawString("Titulo", LetraTitulo2, Brushes.Blue, 220, 180);
                reporte.Graphics.DrawString("Autor", LetraTitulo2, Brushes.Blue, 600, 180);
                reporte.Graphics.DrawString("Prcio", LetraTitulo2, Brushes.Blue, 800, 180);
                reporte.Graphics.DrawString("Stock", LetraTitulo2, Brushes.Blue, 1000, 180);
                reporte.Graphics.DrawString("ValorStock", LetraTitulo2, Brushes.Blue, 1200, 180);
                reporte.Graphics.DrawString("idCategoria", LetraTitulo2, Brushes.Blue, 1400, 180);

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla2;

                adaptador = new OleDbDataAdapter(comando);

                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla2);

                if (DS.Tables[Tabla2].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla2].Rows)
                    {
                        reporte.Graphics.DrawString(fila["Codigo"].ToString(), LetraTexto, Brushes.Black, 20, f);
                        reporte.Graphics.DrawString(fila["Titulo"].ToString(), LetraTexto, Brushes.Black, 220, f);
                        reporte.Graphics.DrawString(fila["Autor"].ToString(), LetraTexto, Brushes.Black, 600, f);
                        reporte.Graphics.DrawString(fila["Precio"].ToString(), LetraTexto, Brushes.Black, 800, f);
                        reporte.Graphics.DrawString(fila["Stock"].ToString(), LetraTexto, Brushes.Black, 1000, f);
                        reporte.Graphics.DrawString(fila["idCategoria"].ToString(), LetraTexto, Brushes.Black, 1200, f);

                        f += 40;
                    }
                }

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void AgregarLibro()
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla2; // Libros

                adaptador = new OleDbDataAdapter(comando);

                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla2);

                DataTable tabla = DS.Tables[Tabla2];

                DataRow fila = tabla.NewRow();

                fila["Codigo"] = cod;
                fila["Titulo"] = tit;
                fila["Autor"] = aut;
                fila["Precio"] = pre;
                fila["Stock"] = sto;
                fila["idCategoria"] = idCat;

                tabla.Rows.Add(fila);

                OleDbCommandBuilder ConciliaCambios = new OleDbCommandBuilder(adaptador);

                adaptador.Update(DS, Tabla2);

                conexion.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("No se puede convertir el tipo de dato");
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("No se puede dividir por cero");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("El valor del argumento no puede estar vacío");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Índice fuera de la matriz");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
