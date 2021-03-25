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

namespace Guaterra.MantenimientoInternos
{
    public partial class CuotasEngancheIntereses : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;

        public CuotasEngancheIntereses()
        {
            InitializeComponent();
            llenarcombobox();
        }

        void bloquear()
        {
            txtporcentajeañoalquiler.Enabled = false;
            txtporcentajeañoventa.Enabled = false;
            txtporcentajeenganche.Enabled = false;
            cbocuotaalquiler.Enabled = false;
         

        }

        void desbloquear()
        {
            txtporcentajeañoalquiler.Enabled = true;
            txtporcentajeañoventa.Enabled = true;
            txtporcentajeenganche.Enabled = true;
            cbocuotaalquiler.Enabled = true;
            cbotipopropiedad.Enabled = true;
            
        }
        void limpiar()
        {
            txtporcentajeañoalquiler.Text = "";
            txtporcentajeañoventa.Text = "";
            txtporcentajeenganche.Text = "";
            cbocuotaalquiler.Text="";
        }

        void llenarcombobox()
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Tipo FROM tipo_propiedad", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cbotipopropiedad.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtporcentajeañoalquiler.Text == "" || txtporcentajeañoventa.Text == "" || txtporcentajeenganche.Text == "")
            {
                MessageBox.Show("Hace Falta Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {

                try
                {
                    string tipopropiedad = cbotipopropiedad.SelectedItem.ToString();
                    cmd = new OdbcCommand("INSERT INTO enganche(Fk_idTipoPropiedad,porcentaje)" +
                        "VALUES('"+ Convert.ToString(Tablaintereses.CurrentRow.Cells[4].Value) + ",'"
                        + (Convert.ToInt32(txtporcentajeenganche.ToString())/100) + "'); INSERT INTO cuotagarantia(idTipoPropiedad,tiempoaniocuota)" +
                        "VALUES('"+ Convert.ToString(Tablaintereses.CurrentRow.Cells[4].Value) + ",'" +
                        +(Convert.ToInt32(cbocuotaalquiler.SelectedIndex)+1)+"'); INSERT INTO interestiempopago(idTipoPropiedad,porcentajeventa,porcentajealquiler)" +
                        "VALUES('"+ Convert.ToString(Tablaintereses.CurrentRow.Cells[4].Value)+ ",'" + 
                        Convert.ToInt32(txtporcentajeañoventa.Text.ToString())+"','"+Convert.ToInt32(txtporcentajeañoalquiler.Text.ToString())+"')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Puesto Ingresado Correctamente");
                    limpiar();
                }
            }
        }

        private void CuotasEngancheIntereses_Load(object sender, EventArgs e)
        {
            bloquear();
        }

        private void cbotipopropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoporpiededad = cbotipopropiedad.Text.ToString();
            desbloquear();

            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT enganche.Porcentaje*100, cuotagarantia.tiempoaniocuota, interestiempopago.PorcentajeVenta*100, interestiempopago.PorcentajeAlquiler*100,tipo_propiedad.idTipo_propiedad FROM " +
                    "enganche INNER JOIN cuotagarantia ON cuotagarantia.idTipopropiedad=enganche.Fk_idTipoPropiedad INNER JOIN interestiempopago ON cuotagarantia.idTipopropiedad=interestiempopago.idTipoPropiedad " +
                    "INNER JOIN tipo_propiedad ON cuotagarantia.idTipopropiedad=tipo_propiedad.idTipo_Propiedad AND tipo_propiedad.Tipo= '" + cbotipopropiedad.SelectedItem.ToString() + "'", nueva.nuevaConexion());
                dat.Fill(ds);

        

                Tablaintereses.DataSource= ds.Tables[0];


                if (Tablaintereses.CurrentRow != null)
                {
                    txtporcentajeenganche.Text = Convert.ToString(Convert.ToInt32(Tablaintereses.CurrentRow.Cells[0].Value));
                    cbocuotaalquiler.SelectedIndex = Convert.ToInt32(Tablaintereses.CurrentRow.Cells[1].Value) - 1;
                    txtporcentajeañoventa.Text = Convert.ToString(Tablaintereses.CurrentRow.Cells[2].Value);
                    txtporcentajeañoalquiler.Text = Convert.ToString(Tablaintereses.CurrentRow.Cells[3].Value);
                    //el id del tipo propiedad está en Tablaintereses.CurrentRow.Cells[4].Value; se usa para actualizar o insertar
                    MessageBox.Show("Propiedad Encontrada");
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("No existen registros anteriores\nIngrese registros por primera vez");
                    limpiar();
                }
                else

                    MessageBox.Show("Hubo algun error de conexión. Intente nuevamente");   
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };


        }

        private void Tablaintereses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
            bloquear();
        }

        private void cbocuotaalquiler_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
