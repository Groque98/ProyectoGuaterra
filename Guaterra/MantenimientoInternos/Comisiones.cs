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
    public partial class Comisiones : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;

        public Comisiones()
        {
            InitializeComponent();
            llenarcombobox();
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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Comisiones_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (txt.Text=="")
            {
                MessageBox.Show("Hace Falta llenar el campo", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
           
            
            if(Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[0].Value)) != ""){

                try
                {
                    string tipopropiedad = cbotipopropiedad.SelectedItem.ToString();
                    cmd = new OdbcCommand("INSERT INTO comision(idTipoPropiedad,porcentaje)" +
                        "VALUES('"+ Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[0].Value)) + "','"+txt.Text.ToString()+ "')", nueva.nuevaConexion());
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
                    txt.Text = "";
                }
            }
        }

        private void cbotipopropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoporpiededad = cbotipopropiedad.Text.ToString();
            txt.Enabled = true;

            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT a.* , b.porcentaje FROM tipo_propiedad a INNER JOIN comision b ON " +
                    "a.idTipo_propiedad=b.idTipoPropiedad WHERE a.Tipo= '" + tipoporpiededad + "'", nueva.nuevaConexion());
                dat.Fill(ds);



                tablaprop.DataSource = ds.Tables[0];


                if (tablaprop.CurrentRow != null)
                {
                    txt.Text = Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[2].Value));
               
                    //el id del tipo propiedad está en tablaprop.CurrentRow.Cells[1].Value; se usa para actualizar o insertar
                    MessageBox.Show("Propiedad Encontrada");
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("No existen registros anteriores\nIngrese registros por primera vez");
                    txt.Text = "";
                   
                        try
                        {
                            OdbcDataAdapter dat2;
                            DataSet ds2;
                            ds2 = new DataSet();
                            dat2 = new OdbcDataAdapter("SELECT idTipo_Propiedad From tipo_propiedad WHERE tipo='" +
                               cbotipopropiedad.SelectedItem.ToString() + "'", nueva.nuevaConexion());
                            dat2.Fill(ds2);
                            tablaprop.DataSource = ds2.Tables[0];
                          

                            if (Convert.ToString(Convert.ToInt32(tablaprop.CurrentRow.Cells[0].Value)) == "")
                            {
                                MessageBox.Show("Imposible ingresar, Verifique los tipos de Propiedad e Intentelo de nuevo");
                            }
                        }
                        catch (OdbcException E)
                        {
                            MessageBox.Show(E.Message);
                            ingresoCorrecto = false;
                        }
                    
                }
                else

                    MessageBox.Show("Hubo algun error de conexión. Intente nuevamente");
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };

        }
    }
}
