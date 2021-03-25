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
    public partial class Usuarios : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;
        public Usuarios()
        {
            InitializeComponent();
            llenarTabla();
            llenarComboBox();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtContraseña.Enabled = true;
            txtUsuario.Enabled = true;
            cbmTipo.Enabled = true;
            cbmEmpleado.Enabled = true;

            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void llenarTabla() {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT u.idUsuario AS Codigo,u.Nombre_Usuario AS Usuario,u.Contraseña_Usuario AS Contraseña, t.Nombre AS Tipo,e.Nombre AS Empleado FROM usuario u" +
                    " INNER JOIN tipo_usuario t ON u.idTipo_Usuario=t.idTipo_Usuario INNER JOIN empleado e ON u.idEmpleado=e.idEmpleado", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaUsuarios.DataSource = ds.Tables[0];
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            };
        }

        public void llenarComboBox() {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Nombre FROM tipo_usuario", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cbmTipo.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }

            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Nombre FROM empleado", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cbmEmpleado.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void Usuarios_Load(object sender, EventArgs e)
        {
            txtContraseña.Enabled = false;
            txtUsuario.Enabled = false;
            cbmEmpleado.Enabled = false;
            cbmTipo.Enabled = false;

            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            txtBuscar.Text = "";
            cbmEmpleado.Items.Clear();
            cbmTipo.Items.Clear();
            llenarComboBox();

            txtContraseña.Enabled = false;
            txtUsuario.Enabled = false;
            cbmEmpleado.Enabled = false;
            cbmTipo.Enabled = false;

            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnNuevo.Enabled = true;
        }

        private void TablaUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaUsuarios.CurrentRow.Cells[0].Value.ToString() !="") {
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnCancelar.Enabled = true;
                btnGuardar.Enabled = false;

                txtContraseña.Enabled = true;
                txtUsuario.Enabled = true;
                cbmEmpleado.Enabled = true;
                cbmTipo.Enabled = true;

                txtId.Text= Convert.ToString(TablaUsuarios.CurrentRow.Cells[0].Value);
                txtUsuario.Text= Convert.ToString(TablaUsuarios.CurrentRow.Cells[1].Value);
                txtContraseña.Text= Convert.ToString(TablaUsuarios.CurrentRow.Cells[2].Value);
                cbmTipo.Text=Convert.ToString(TablaUsuarios.CurrentRow.Cells[3].Value);
                cbmEmpleado.Text=Convert.ToString(TablaUsuarios.CurrentRow.Cells[4].Value);
            }
            else {
                MessageBox.Show("Campo Vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Validacion Tipo
            string tipo = "";
            bool validacionTipo = false;

            //validacion de combobox vacio
            if (cbmTipo.SelectedItem != null)
            {
                string nTipo = cbmTipo.SelectedItem.ToString();
                //Consulta El Codigo Del Tipo Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idTipo_Usuario FROM tipo_usuario WHERE Nombre= '" + nTipo + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        tipo = almacena.GetString(0);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
            }
            else
            {
                validacionTipo = true;
            }
            //Validacion Empleado
            string empleado = "";
            bool validacionEmpleado = false;

            //validacion de combobox vacio
            if (cbmEmpleado.SelectedItem != null)
            {
                string nEmpleado = cbmEmpleado.SelectedItem.ToString();
                //Consulta El Codigo Del Empleado Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idEmpleado FROM empleado WHERE Nombre= '" + nEmpleado + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        empleado = almacena.GetString(0);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
            }
            else
            {
                validacionEmpleado = true;
            }

            //Ingreso De Usuario
            if (txtUsuario.Text == "" || txtContraseña.Text == "" || cbmEmpleado.Text == "" || cbmTipo.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {

                try
                {

                    cmd = new OdbcCommand("INSERT INTO usuario(Nombre_Usuario,Contraseña_Usuario,idTipo_Usuario, idEmpleado)" +
                        "VALUES('" + txtUsuario.Text + "','"
                        + txtContraseña.Text + "','"
                        + tipo + "','"
                        + empleado + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Usuario Ingresado Correctamente");
                    txtId.Text = "";
                    txtUsuario.Text = "";
                    txtContraseña.Text = "";
                    cbmTipo.Items.Clear();
                    cbmEmpleado.Items.Clear();
                    llenarComboBox();

                    txtId.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtContraseña.Enabled = false;
                    cbmTipo.Enabled = false;
                    cbmEmpleado.Enabled = false;

                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //Validacion Tipo
            string tipo = "";
            bool validacionTipo = false;

            //validacion de combobox vacio
            if (cbmTipo.SelectedItem != null)
            {
                string nTipo = cbmTipo.SelectedItem.ToString();
                //Consulta El Codigo Del Tipo Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idTipo_Usuario FROM tipo_usuario WHERE Nombre= '" + nTipo + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        tipo = almacena.GetString(0);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                validacionTipo = true;
            }
            //Validacion Empleado
            string empleado = "";
            bool validacionEmpleado = false;

            //validacion de combobox vacio
            if (cbmEmpleado.SelectedItem != null)
            {
                string nEmpleado = cbmEmpleado.SelectedItem.ToString();
                //Consulta El Codigo Del Empleado Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idEmpleado FROM empleado WHERE Nombre= '" + nEmpleado + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        empleado = almacena.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                validacionEmpleado = true;
            }

            //Modificar Usuario
            if (txtUsuario.Text == "" || txtContraseña.Text == "" || cbmTipo.Text == "" || cbmEmpleado.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                cmd = new OdbcCommand("UPDATE usuario SET Nombre_Usuario='"
                    + txtUsuario.Text + "',Contraseña_Usuario='"
                    + txtContraseña.Text + "',idTipo_Usuario='"
                    + tipo + "',idEmpleado='"
                    + empleado + "' WHERE idUsuario='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Usuario Modificado Correctamente");
                    txtId.Text = "";
                    txtUsuario.Text = "";
                    txtContraseña.Text = "";
                    cbmTipo.Items.Clear();
                    cbmEmpleado.Items.Clear();
                    llenarComboBox();

                    txtId.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtContraseña.Enabled = false;
                    cbmTipo.Enabled = false;
                    cbmEmpleado.Enabled = false;

                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OdbcCommand("DELETE FROM usuario WHERE idUsuario='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException er) {
                MessageBox.Show(er.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto) {
                MessageBox.Show("Usuario Eliminado Correctamenter");
                txtId.Text = "";
                txtUsuario.Text = "";
                txtContraseña.Text = "";
                cbmTipo.Items.Clear();
                cbmEmpleado.Items.Clear();
                llenarComboBox();

                txtId.Enabled = false;
                txtUsuario.Enabled = false;
                txtContraseña.Enabled = false;
                cbmTipo.Enabled = false;
                cbmEmpleado.Enabled = false;

                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            llenarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT u.idUsuario AS Codigo,u.Nombre_Usuario AS Usuario,u.Contraseña_Usuario AS Contraseña, t.Nombre AS Tipo,e.Nombre AS Empleado FROM usuario u" +
                    " INNER JOIN tipo_usuario t ON u.idTipo_Usuario=t.idTipo_Usuario INNER JOIN empleado e ON u.idEmpleado=e.idEmpleado WHERE u.Nombre_Usuario='" + txtBuscar.Text + "' OR u.idUsuario='"+ txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaUsuarios.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)

                    MessageBox.Show("¡Usuario No Existe!");
                else

                    MessageBox.Show("¡Usuario Encontrado!");

                txtBuscar.Text = "";
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
