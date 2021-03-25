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
    public partial class tipopropiedades : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;

        public tipopropiedades()
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
                dat = new OdbcDataAdapter("SELECT idTipo_Propiedad AS Codigo,tipo as Nombre FROM tipo_propiedad", nueva.nuevaConexion());
                dat.Fill(ds);
                tablapropiedades.DataSource = ds.Tables[0];
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            };
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            btnNuevo.Enabled = false;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT idTipo_Propiedad AS codigo,Tipo FROM tipo_propiedad WHERE Tipo = '" + txtBuscar.Text + "' OR idtipo_propiedad='" + txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                tablapropiedades.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0) { 

                    MessageBox.Show("¡Registro No Existe!");
                llenarTabla();
            }
                else

                    MessageBox.Show("¡Tipo de Propiedad Encontrado!");
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        private void tipopropiedades_Load(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            txtId.Enabled = false;
            txtNombre.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Hacen falta Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }

            else
            {

                try
                {
                    cmd = new OdbcCommand("INSERT INTO tipo_propiedad(Tipo) " +
                        "VALUES('" + txtNombre.Text + "')", nueva.nuevaConexion());
                    cmd.ExecuteNonQuery();
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Tipo de Propiedad Ingresada Correctamente");
                    txtNombre.Text = "";

                    txtNombre.Enabled = false;

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
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Hacen Falta Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                cmd = new OdbcCommand("UPDATE tipo_propiedad SET Tipo='"
                    + txtNombre.Text + "' WHERE idTipo_propiedad='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Puesto Modificado Correctamente");
                    txtId.Text = "";
                    txtNombre.Text = "";

                    txtNombre.Enabled = false;

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
                cmd = new OdbcCommand("  DELETE FROM Tipo_propiedad WHERE idTipo_Propiedad='"
                    + txtId.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto)
            {
                MessageBox.Show("Puesto Eliminado Correctamente");
                txtId.Text = "";
                txtNombre.Text = "";

                txtNombre.Enabled = false;

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

            txtId.Enabled = false;
            txtId.Text = "";
            txtNombre.Enabled = false;

            txtNombre.Text = "";
            llenarTabla();
        }

        private void tablapropiedades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tablapropiedades.CurrentRow.Cells[0].Value.ToString() != "")
            {
                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnCancelar.Enabled = true;
                btnGuardar.Enabled = false;
                txtNombre.Enabled = true;
                btnNuevo.Enabled = false;

                txtId.Text = Convert.ToString(tablapropiedades.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(tablapropiedades.CurrentRow.Cells[1].Value);
            }
        }
    }
}
