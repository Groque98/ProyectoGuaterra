using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guaterra.MantenimientosExternos.Servicios
{
    public partial class Arrendadores : Form
    {

        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;
        List<string> lista = new List<string>();

        public Arrendadores()
        {
            InitializeComponent();
        }

        void llenarcombobox()
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Tipo FROM tipo_propiedad", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {

                    cboxtiempoalquiler.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void GroupBox4_Enter(object sender, EventArgs e)
        {

        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {





        }

        private void button3_Click(object sender, EventArgs e)
        {


            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter(" SELECT propiedad.idPropiedad, propiedad.idTipo_Propiedad, " +
                    "propiedad.Descripcion,d.idDepartamento,propiedad.idMunicipio, propiedad.Direccion,propiedad.idClientes," +
                    "propiedad.MetrosCuadrados,propiedad.No_Niveles, propiedad.Costo FROM propiedad INNER JOIN " +
                    "municipio ON propiedad.idMunicipio=municipio.idMunicipio INNER JOIN departamento d ON municipio.idMunicipio=d.idDepartamento" +
                    " WHERE propiedad.disponibilidad=1 AND idPropiedad='" + txtbuscarpropiedad.Text.ToString() + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                tablaprop.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("¡Registro No Existe!");


                }

                txtid.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                txttipoprop.Text = Convert.ToString(tablaprop.CurrentRow.Cells[1].Value);
                txtdes.Text = Convert.ToString(tablaprop.CurrentRow.Cells[2].Value);
                txtdep.Text = Convert.ToString(tablaprop.CurrentRow.Cells[3].Value);
                txtmuni.Text = Convert.ToString(tablaprop.CurrentRow.Cells[4].Value);
                txtdireccion.Text = Convert.ToString(tablaprop.CurrentRow.Cells[5].Value);
                txtdueño.Text = Convert.ToString(tablaprop.CurrentRow.Cells[6].Value);
                mcuadrados.Text = Convert.ToString(tablaprop.CurrentRow.Cells[7].Value);
                niveles.Text = Convert.ToString(tablaprop.CurrentRow.Cells[8].Value);
                txtcostoprop.Text = Convert.ToString(tablaprop.CurrentRow.Cells[9].Value);
            }
            catch
            {
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter(" SELECT idClientes, nombre,apellido, DPI from clientes WHERE idClientes='" + txtid2.Text.ToString() + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                tablaprop.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("¡Registro No Existe!");


                }

                txtidClientes.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(tablaprop.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(tablaprop.CurrentRow.Cells[2].Value);
                txtDPI.Text = Convert.ToString(tablaprop.CurrentRow.Cells[3].Value);

            }
            catch
            {
            }

        }

        private void Arrendadores_Load(object sender, EventArgs e)
        {

        }

        private void cboxtiempoalquiler_SelectedItem(object sender, EventArgs e)
        {

            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter(" SELECT a.PorcentajeAlquiler b.tiempoaniocuota from interestiempopago a INNER JOIN cuotagarantía b ON b.idTipoPropiedad=a.idTipoPropiedad WHERE idTipoPropiedad='" + txttipoprop.Text.ToString() + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                tablaprop.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("¡Registro No Existe!");


                }

                txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                txtgarantia.Text = Convert.ToString(tablaprop.CurrentRow.Cells[1].Value);

                if (cboxtiempoalquiler.SelectedItem.ToString() == "1 año")
                {
                    txtcantidadinteres.Text = Convert.ToString(Convert.ToInt32(txtinteres.Text) * Convert.ToInt32(txtcostoprop));
                    txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                    txtgarantia.Text = Convert.ToString(tablaprop.CurrentRow.Cells[1].Value);


                }

                else if (cboxtiempoalquiler.SelectedItem.ToString() == "2 años")
                {
                    txtcantidadinteres.Text = Convert.ToString(Convert.ToInt32(txtinteres.Text) + 1 * Convert.ToInt32(txtcostoprop));
                    txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                    txtgarantia.Text = Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[1].Value) * ((Convert.ToInt32(txtcostoprop) / Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1)) / 12);
                    txtmensualidad.Text = Convert.ToString((Convert.ToInt32(txtcostoprop.Text)) / Convert.ToInt32((cboxtiempoalquiler.SelectedIndex.ToString()) + 1) / 12);
                }
                else if (cboxtiempoalquiler.SelectedItem.ToString() == "3 años")
                {
                    txtcantidadinteres.Text = Convert.ToString(Convert.ToInt32(txtinteres.Text) + 2 * Convert.ToInt32(txtcostoprop));
                    txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                    txtgarantia.Text = Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[1].Value) * ((Convert.ToInt32(txtcostoprop) / Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1)) / 12);
                    txtmensualidad.Text = Convert.ToString((Convert.ToInt32(txtcostoprop.Text)) / Convert.ToInt32((cboxtiempoalquiler.SelectedIndex.ToString()) + 1) / 12);

                }

                else if (cboxtiempoalquiler.SelectedItem.ToString() == "4 años")
                {
                    txtcantidadinteres.Text = Convert.ToString(Convert.ToInt32(txtinteres.Text) + 3 * Convert.ToInt32(txtcostoprop));
                    txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                    txtgarantia.Text = Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[1].Value) * ((Convert.ToInt32(txtcostoprop) / Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1)) / 12);
                    txtmensualidad.Text = Convert.ToString((Convert.ToInt32(txtcostoprop.Text)) / Convert.ToInt32((cboxtiempoalquiler.SelectedIndex.ToString()) + 1) / 12);

                }
                else if (cboxtiempoalquiler.SelectedItem.ToString() == "5 años")
                {
                    txtcantidadinteres.Text = Convert.ToString(Convert.ToInt32(txtinteres.Text) + 4 * Convert.ToInt32(txtcostoprop));
                    txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                    txtgarantia.Text = Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[1].Value) * ((Convert.ToInt32(txtcostoprop) / Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1)) / 12);
                    txtmensualidad.Text = Convert.ToString((Convert.ToInt32(txtcostoprop.Text)) / Convert.ToInt32((cboxtiempoalquiler.SelectedIndex.ToString()) + 1) / 12);

                }

            }
            catch { };
        }

        private void button5_Click(object sender, EventArgs e)
        {

            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter(" SELECT a.PorcentajeAlquiler, b.tiempoaniocuota from interestiempopago a INNER JOIN cuotagarantia b ON b.idTipoPropiedad=a.idTipoPropiedad WHERE a.idTipoPropiedad='" + txttipoprop.Text.ToString() + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                tablaprop.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("¡Registro No Existe!");


                }

                txtinteres.Text = Convert.ToString(tablaprop.CurrentRow.Cells[0].Value);
                txtgarantia.Text = Convert.ToString(tablaprop.CurrentRow.Cells[1].Value);
                
                double costo = Convert.ToDouble(txtcostoprop.Text);
                int tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString())+1;
                double interes = Convert.ToInt32(txtinteres.Text);
                double Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);


                if (cboxtiempoalquiler.SelectedIndex == 0)
                {
                 
                    costo = Convert.ToDouble(txtcostoprop.Text);
                     tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1;
                  interes = Convert.ToInt32(txtinteres.Text);
                    Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);
                }

                if (cboxtiempoalquiler.SelectedIndex == 1)
                {
                 
                    costo = Convert.ToDouble(txtcostoprop.Text);
                     tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1;
                    interes = Convert.ToInt32(txtinteres.Text);
                     Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);
                }
                if (cboxtiempoalquiler.SelectedIndex == 2)
                {
                    costo = Convert.ToDouble(txtcostoprop.Text);
                    tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1;
                    interes = Convert.ToInt32(txtinteres.Text);
                    Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);
                }

                if (cboxtiempoalquiler.SelectedIndex == 3)
                {
                  costo = Convert.ToDouble(txtcostoprop.Text);
                    tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1;
                    interes = Convert.ToInt32(txtinteres.Text);
                    Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);
                }
                if (cboxtiempoalquiler.SelectedIndex == 4)
                {
                   costo = Convert.ToDouble(txtcostoprop.Text);
                    tiempo = Convert.ToInt32(cboxtiempoalquiler.SelectedIndex.ToString()) + 1;
                    interes = Convert.ToInt32(txtinteres.Text);
                    Cuotagarantía = ((costo / tiempo) / 12) * Convert.ToDouble(txtgarantia.Text);
                }

                txtgarantia.Text = Convert.ToString(Cuotagarantía);
                double mensualidad = ((costo + interes) / tiempo)/12;
                txtmensualidad.Text = Convert.ToString(mensualidad);
            }
            catch(Exception E)
            {
                Console.WriteLine(E);
            }
        }

    }
    }
    



    
    

