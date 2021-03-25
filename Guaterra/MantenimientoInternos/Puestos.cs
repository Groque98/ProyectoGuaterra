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
    public partial class Puestos : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;
        public Puestos()
        {
            InitializeComponent();
            llenarTabla();
        }

        public void llenarTabla() {
            OdbcDataAdapter dat;
            DataSet ds;
            try {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT idPuesto AS Puesto,Nombre,Descripcion,Sueldo FROM puesto", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaPuestos.DataSource = ds.Tables[0];
            } catch (OdbcException e) {
                MessageBox.Show(e.Message);
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtSueldo.Enabled = true;
            txtDescripcion.Enabled = true;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
        }

        private void Puestos_Load(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            txtDescripcion.Enabled = false;
            txtId.Enabled = false;
            txtNombre.Enabled = false;
            txtSueldo.Enabled = false;

        }

      

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            txtDescripcion.Enabled = false;
            txtId.Enabled = false;
            txtId.Text = "";
            txtNombre.Enabled = false;
            txtSueldo.Enabled = false;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtSueldo.Text = "";
            llenarTabla();
        }

        private void TablaPuestos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void TablaPuestos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaPuestos.CurrentRow.Cells[0].Value.ToString() != "") {
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnCancelar.Enabled = true;
                btnGuardar.Enabled = false;
                txtNombre.Enabled = true;
                btnNuevo.Enabled = false;
                txtSueldo.Enabled = true;
                txtDescripcion.Enabled = true;
                txtId.Text = Convert.ToString(TablaPuestos.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(TablaPuestos.CurrentRow.Cells[1].Value);
                txtDescripcion.Text = Convert.ToString(TablaPuestos.CurrentRow.Cells[2].Value);
                txtSueldo.Text = Convert.ToString(TablaPuestos.CurrentRow.Cells[3].Value);
            }
            else {
                MessageBox.Show("Campo Vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text==""||txtDescripcion.Text==""||txtSueldo.Text=="")
            {
                MessageBox.Show("Hacen Campos Por Llenar","INFORMACION",MessageBoxButtons.OK,MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                
                try
                {
                    cmd = new OdbcCommand("INSERT INTO puesto(Nombre,Descripcion,Sueldo)"+
                        "VALUES('"+txtNombre.Text + "','"
                        + txtDescripcion.Text + "','"
                        + txtSueldo.Text + "')",nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if(ingresoCorrecto){
                    MessageBox.Show("Puesto Ingresado Correctamente");
                    txtNombre.Text = "";
                    txtSueldo.Text = "";
                    txtDescripcion.Text = "";
                    txtNombre.Enabled = false;
                    txtSueldo.Enabled = false;
                    txtDescripcion.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnModificar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                }

                llenarTabla();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text==""||txtSueldo.Text == ""||txtDescripcion.Text == "") {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else {
                cmd = new OdbcCommand("UPDATE puesto SET Nombre='"
                    + txtNombre.Text + "',Descripcion='"
                    + txtDescripcion.Text + "',Sueldo='"
                    + txtSueldo.Text + "' WHERE idPuesto='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Puesto Modificado Correctamente");
                    txtId.Text = "";
                    txtNombre.Text = "";
                    txtSueldo.Text = "";
                    txtDescripcion.Text = "";
                    txtNombre.Enabled = false;
                    txtSueldo.Enabled = false;
                    txtDescripcion.Enabled = false;
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
            try {
                cmd = new OdbcCommand("DELETE FROM puesto WHERE idPuesto='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex) {
                MessageBox.Show(ex.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto) {
                MessageBox.Show("Puesto Eliminado Correctamente");
                txtId.Text = "";
                txtNombre.Text = "";
                txtSueldo.Text = "";
                txtDescripcion.Text = "";
                txtNombre.Enabled = false;
                txtSueldo.Enabled = false;
                txtDescripcion.Enabled = false;
                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = false;
            }

            llenarTabla();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT idPuesto AS Puesto,Nombre,Descripcion,Sueldo FROM puesto WHERE Nombre = '" + txtBuscar.Text + "' OR idPuesto='" + txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaPuestos.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0) { 

                    MessageBox.Show("¡Registro No Existe!");
                llenarTabla();
            }
                else

                    MessageBox.Show("¡Puesto Encontrado!");
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }
    }
}
