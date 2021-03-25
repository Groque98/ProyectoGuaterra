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
    public partial class Empleados : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;
        public Empleados()
        {
            InitializeComponent();
            llenarTabla();
            llenarcombobox();
        }

        public void llenarcombobox()
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Nombre FROM puesto", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read()==true) {
                    cboPuesto.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void llenarTabla()
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT e.idEmpleado AS Codigo,e.Nombre, e.Apellido,e.DPI,e.Direccion," +
                     "te.Telefono," +
                    "c.Correo, " +
                    "p.Nombre AS Puesto" +
                    " FROM empleado e INNER JOIN telefono_e te" +
                    " ON te.idEmpleado=e.idEmpleado" +
                    " JOIN correo_t c ON c.idEmpleado=e.idEmpleado" +
                    " JOIN puesto p ON e.idPuesto=p.idPuesto", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaEmpleados.DataSource = ds.Tables[0];
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            };
        }

        private void Empleados_Load(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            txtTelefono.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            txtidEmpleado.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDPI.Enabled = false;
            txtDireccion.Enabled = false;
            txtCorreo.Enabled = false;
            cboPuesto.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDPI.Enabled = true;
            txtDireccion.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            cboPuesto.Enabled = true;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
                string puesto = "";
            bool validacionPuesto;

            if (cboPuesto.SelectedItem != null)
            {
                string nPuesto = cboPuesto.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idPuesto FROM puesto WHERE nombre= '" + nPuesto + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        puesto = almacena.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                validacionPuesto = true;
            }

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDPI.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "" || txtCorreo.Text == "" || cboPuesto.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                cmd = new OdbcCommand("UPDATE empleado SET Nombre='"
                    + txtNombre.Text + "',Apellido='"
                    + txtApellido.Text + "',DPI='"
                    + txtDPI.Text + "',Direccion='"
                    + txtDireccion.Text + "',idPuesto='"
                    + puesto + "' WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                cmd = new OdbcCommand("UPDATE correo_t SET Correo='"
                    + txtCorreo.Text + "' WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'",nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                cmd = new OdbcCommand("UPDATE telefono_e SET Telefono='"
                    + txtTelefono.Text + "' WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Empleado Modificado Correctamente");
                    txtidEmpleado.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDPI.Text = "";
                    txtDireccion.Text = "";
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";
                    cboPuesto.Text = "";

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDPI.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;
                    cboPuesto.Enabled = false;

                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validacion Puesto
            string puesto = "";
            bool validacionPuesto = false;

            //validacion de combobox vacio
            if (cboPuesto.SelectedItem!=null)
            {
                string nPuesto = cboPuesto.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try {
                    OdbcCommand sql = new OdbcCommand("SELECT idPuesto FROM puesto WHERE nombre= '"+ nPuesto + "'"
                        ,nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read()==true) {
                        puesto = almacena.GetString(0);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                validacionPuesto = true;
            }
            //Ingreso De Empleados
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDPI.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "" || txtCorreo.Text == "" || cboPuesto.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {

                try
                {

                    cmd = new OdbcCommand("INSERT INTO empleado(Nombre,Apellido,DPI,Direccion,idPuesto)" +
                        "VALUES('" + txtNombre.Text + "','"
                        + txtApellido.Text + "','"
                        + txtDPI.Text + "','"
                        + txtDireccion.Text + "', '"
                        + puesto + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();

                    OdbcCommand sql = new OdbcCommand("SELECT MAX(idEmpleado) FROM empleado", nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        txtContador.Text = almacena["MAX(idEmpleado)"].ToString();
                    }
                    almacena.Close();

                    cmd = new OdbcCommand("INSERT INTO correo_t(Correo,idEmpleado)"+
                        "VALUES('"+txtCorreo.Text+ "','"
                       + txtContador.Text + "')",nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();

                    cmd = new OdbcCommand("INSERT INTO telefono_e(Telefono,idEmpleado)" +
                        "VALUES('" + txtTelefono.Text + "','"
                        + txtContador.Text + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Empleado Ingresado Correctamente");
                    txtidEmpleado.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDPI.Text = "";
                    txtDireccion.Text = "";
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";
                    cboPuesto.Text = "";

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDPI.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;
                    cboPuesto.Enabled = false;

                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }
        }
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                //ELimina Correo 
                cmd = new OdbcCommand("DELETE FROM correo_t WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
                //Elimina Telefono
                cmd = new OdbcCommand("DELETE FROM telefono_e WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
                //Elimina Empleado
                cmd = new OdbcCommand("DELETE FROM empleado WHERE idEmpleado='"
                    + txtidEmpleado.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto)
            {
                MessageBox.Show("Empleado Eliminado Correctamente");
                txtidEmpleado.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDPI.Text = "";
                txtDireccion.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                cboPuesto.Text = "";

                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDPI.Enabled = false;
                txtDireccion.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                cboPuesto.Enabled = false;

                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = false;
            }

            llenarTabla();
        }
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;

            txtidEmpleado.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDPI.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
            cboPuesto.Enabled = false;


            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDPI.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtidEmpleado.Text = "";
            cboPuesto.Text = "";
        }

        private void TablaEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaEmpleados.CurrentRow.Cells[0].Value.ToString()!="") {
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = true;
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtDPI.Enabled = true;
                txtDireccion.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                cboPuesto.Enabled = true;
                txtidEmpleado.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[2].Value);
                txtDPI.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[3].Value);
                txtDireccion.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[4].Value);
                txtTelefono.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[5].Value);
                txtCorreo.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[6].Value);
                cboPuesto.Text = Convert.ToString(TablaEmpleados.CurrentRow.Cells[7].Value);
            }
            else {
                MessageBox.Show("Campo Vacio","Advertencia",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //this.MinimizeBox;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT e.idEmpleado AS Codigo,e.Nombre, e.Apellido,e.DPI,e.Direccion," +
                     "te.Telefono," +
                    "c.Correo, " +
                    "p.Nombre AS Puesto" +
                    " FROM empleado e INNER JOIN telefono_e te" +
                    " ON te.idEmpleado=e.idEmpleado" +
                    " JOIN correo_t c ON c.idEmpleado=e.idEmpleado" +
                    " JOIN puesto p ON e.idPuesto=p.idPuesto" +
                    " WHERE e.Nombre='" + txtBuscar.Text + "' OR e.idEmpleado='" + txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaEmpleados.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)

                    MessageBox.Show("¡Registro No Existe!");
                else

                    MessageBox.Show("¡Empleado Encontrado!");

                txtBuscar.Text = "";
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
