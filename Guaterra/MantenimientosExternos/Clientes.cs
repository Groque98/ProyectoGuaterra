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

namespace Guaterra.MantenimientosExternos
{
    public partial class Clientes : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;

        public Clientes()
        {
            InitializeComponent();
            llenarTabla();
        }
        public void llenarTabla()
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT cl.idClientes AS Codigo,cl.Nombre,cl.Apellido,cl.Direccion,cl.DPI," +
                    "tel.Telefono," +
                    "cor.Correo" +
                    " FROM clientes cl INNER JOIN telefono tel" +
                    " ON tel.idClientes=cl.idClientes" +
                    " JOIN Correo cor ON cor.idClientes=cl.idClientes", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaClientes.DataSource = ds.Tables[0];
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            };
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            txtTelefono.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            txtidClientes.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtDPI.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDireccion.Enabled = true;
            txtDPI.Enabled = true;
            txtTelefono.Enabled = true;
            txtCorreo.Enabled = true;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDireccion.Text == "" || txtDPI.Text == "" || txtTelefono.Text == "" || txtCorreo.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                cmd = new OdbcCommand("UPDATE clientes SET Nombre='"
                    + txtNombre.Text + "',Apellido='"
                    + txtApellido.Text + "',Direccion='"
                    + txtDireccion.Text + "',DPI='"
                    + txtDPI.Text + "' WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                cmd = new OdbcCommand("UPDATE correo SET Correo='"
                    + txtCorreo.Text + "' WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                cmd = new OdbcCommand("UPDATE telefono SET Telefono='"
                    + txtTelefono.Text + "' WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Cliente Modificado Correctamente");
                    txtidClientes.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDireccion.Text = "";
                    txtDPI.Text = "";
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtDPI.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;

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
            //Ingreso De Empleados
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtDireccion.Text == "" || txtDPI.Text == "" || txtTelefono.Text == "" || txtCorreo.Text == "")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {

                try
                {

                    cmd = new OdbcCommand("INSERT INTO clientes(Nombre,Apellido,Direccion,DPI)" +
                        "VALUES('" + txtNombre.Text + "','"
                        + txtApellido.Text + "','"
                        + txtDireccion.Text + "','"
                        + txtDPI.Text + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();

                    OdbcCommand sql = new OdbcCommand("SELECT MAX(idClientes) FROM clientes", nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        txtContador.Text = almacena["MAX(idClientes)"].ToString();
                    }
                    almacena.Close();
                    cmd = new OdbcCommand("INSERT INTO telefono(Telefono,idClientes)" +
                        "VALUES('" + txtTelefono.Text + "','"
                        + txtContador.Text + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();

                    cmd = new OdbcCommand("INSERT INTO correo(Correo,idClientes)" +
                        "VALUES('" + txtCorreo.Text + "','"
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
                    MessageBox.Show("Cliente Ingresado Correctamente");
                    txtidClientes.Text = "";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtDireccion.Text = "";
                    txtDPI.Text = "";
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";

                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtDireccion.Enabled = false;
                    txtDPI.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;

                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }

        }
        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                //ELimina Correo 
                cmd = new OdbcCommand("DELETE FROM correo WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
                //Elimina Telefono
                cmd = new OdbcCommand("DELETE FROM telefono WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
                //Elimina Cliente
                cmd = new OdbcCommand("DELETE FROM clientes WHERE idClientes='"
                    + txtidClientes.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto)
            {
                MessageBox.Show("Cliente Eliminado Correctamente");
                txtidClientes.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDireccion.Text = "";
                txtDPI.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";

                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDireccion.Enabled = false;
                txtDPI.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;

                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = false;
            }

            llenarTabla();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;

            txtidClientes.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccion.Enabled = false;
            txtDPI.Enabled = false;
            txtTelefono.Enabled = false;
            txtCorreo.Enabled = false;

            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDireccion.Text = "";
            txtDPI.Text = "";
            txtTelefono.Text = "";
            txtidClientes.Text = "";
            txtCorreo.Text = "";
        }

        private void TablaClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaClientes.CurrentRow.Cells[0].Value.ToString() != "") {
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnCancelar.Enabled = true;
                txtNombre.Enabled = true;
                txtApellido.Enabled = true;
                txtDireccion.Enabled = true;
                btnGuardar.Enabled = false;
                txtDPI.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtidClientes.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[2].Value);
                txtDireccion.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[3].Value);
                txtDPI.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[4].Value);
                txtTelefono.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[5].Value);
                txtCorreo.Text = Convert.ToString(TablaClientes.CurrentRow.Cells[6].Value);
            }
            else {
                MessageBox.Show("Campo Vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT cl.idClientes AS Codigo,cl.Nombre,cl.Apellido,cl.Direccion,cl.DPI," +
                    "tel.Telefono," +
                    "cor.Correo" +
                    " FROM clientes cl INNER JOIN telefono tel" +
                    " ON tel.idClientes=cl.idClientes" +
                    " JOIN Correo cor ON cor.idClientes=cl.idClientes" +
                    " WHERE cl.Nombre='" + txtBuscar.Text + "' OR cl.idClientes='" + txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaClientes.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)

                    MessageBox.Show("¡Registro No Existe!");
                else

                    MessageBox.Show("¡Cliente Encontrado!");

                txtBuscar.Text = "";
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
