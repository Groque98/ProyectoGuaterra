using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
//using System.Windows.Forms.CheckBox
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

using CrystalDecisions.Windows.Forms;

namespace Guaterra.Reportes
{
    public partial class Reporte : Form
    {
        CrystalReportViewer objRpt;
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        public Reporte()
        {
            InitializeComponent();
            //llenarTabla();
            llenarcombobox();
        }
        public void llenarcombobox()
        {
            try
            {
                    cboReportes.Items.Add("Empleado");
                    cboReportes.Items.Add("Cliente");
                    cboReportes.Items.Add("Propiedades");
                    cboReportes.Items.Add("Registro Pagos");
                    cboReportes.Items.Add("Registro Servicio");
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            ds = new DataSet();
            string message = string.Empty;

            if (cboReportes.SelectedIndex == 0)
            {
                new InfEmp().Show();
            }
            else
                if (cboReportes.SelectedIndex == 1)
            {
                new InfCli().Show();
            }
            else
                if (cboReportes.SelectedIndex == 2)
            {
                new InfProp().Show();
            }
            else
                if (cboReportes.SelectedIndex ==3)
            {
                new InfRPagos().Show();
            }
            else
                if(cboReportes.SelectedIndex == 4)
            {
                new InfRServicios().Show();
            }
        }
        //-----------------------------------------------------------Formulario
        private void Reporte_Load(object sender, EventArgs e)
        {
            /*DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "Seleccionar";
            chk.Name = "chk";
            TablaReporte.Columns.Add(chk);*/
        }
        /*private void colorFila(DataGridView datagrid)
        {
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                if (Convert.ToBoolean(datagrid.Rows[row.Index].Cells["chk"].Value) == true)
                {
                    row.DefaultCellStyle.BackColor = Color.SeaGreen;
                    row.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
                }
            }
        }*/
        //---------------------------------------TablaReporte
        private void TablaReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           /* string message = string.Empty;
            foreach (DataGridViewRow fila in TablaReporte.Rows)
            {
                if (Convert.ToBoolean(fila.Cells["chk"].Value))
                {
                    message += fila.Cells["chk"].Value.ToString();
                }
            }
            MessageBox.Show("Fila Seleccionada" + message);   */
            //this.crystalReportViewer1.RefreshReport();

            /*string message = string.Empty;
            foreach (DataGridViewRow row in TablaReporte.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["chk"].Value);
                if (isSelected)
                {
                    /*message += Environment.NewLine;
                    message += row.Cells["chk"].Value.ToString();
                }
            }
            MessageBox.Show("Fila Seleccionada" + message);*/
            //colorFila(TablaReporte);
            // this.crystalReportViewer1.RefreshReport();

        }
        private void InfEmp_Load(object sender, EventArgs e)
        {
        }
        private void TablaReporte_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {       
        }
        //-------------------------------------------------------
        private void cboReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                if (cboReportes.SelectedIndex == 0)
                {
                    
                    dat = new OdbcDataAdapter("SELECT e.idEmpleado AS Codigo,e.Nombre, e.Apellido,e.DPI,e.Direccion," +
                         "te.Telefono," +
                        "c.Correo, " +
                        "p.Nombre AS Puesto" +
                        " FROM empleado e INNER JOIN telefono_e te" +
                        " ON te.idEmpleado=e.idEmpleado" +
                        " JOIN correo_t c ON c.idEmpleado=e.idEmpleado" +
                        " JOIN puesto p ON e.idPuesto=p.idPuesto", nueva.nuevaConexion());
                    dat.Fill(ds);
                    TablaReporte.DataSource = ds.Tables[0];
                }
                else 
                    if(cboReportes.SelectedIndex == 1)
                {
                    dat = new OdbcDataAdapter("SELECT cl.idClientes AS Codigo,cl.Nombre,cl.Apellido,cl.Direccion,cl.DPI," +
                    "tel.Telefono," +
                    "cor.Correo" +
                    " FROM clientes cl INNER JOIN telefono tel" +
                    " ON tel.idClientes=cl.idClientes" +
                    " JOIN Correo cor ON cor.idClientes=cl.idClientes", nueva.nuevaConexion());
                    dat.Fill(ds);
                    TablaReporte.DataSource = ds.Tables[0];
                }
                else 
                    if(cboReportes.SelectedIndex == 2)
                {
                    dat = new OdbcDataAdapter("SELECT pr.idPropiedad AS Codigo,pr.MetrosCuadrados,pr.Descripcion,pr.Direccion,pr.Costo,pr.No_Niveles," +
                    " tip.Tipo," +
                    " c.Nombre AS Dueño," +
                    " mun.Municipio," +
                    " dept.Nombre AS Departamento" +
                    " FROM propiedad pr INNER JOIN tipo_propiedad tip" +
                    " ON tip.idTipo_Propiedad=pr.idTipo_Propiedad " +
                    " JOIN clientes c ON pr.idClientes=c.idClientes " +
                    " JOIN municipio mun ON mun.idMunicipio=pr.idMunicipio" +
                    " JOIN departamento dept ON dept.idDepartamento=mun.idDepartamento", nueva.nuevaConexion());
                    dat.Fill(ds);
                    TablaReporte.DataSource = ds.Tables[0];
                }
                else
                    if (cboReportes.SelectedIndex == 3)//Registro Pagos
                {
                    dat = new OdbcDataAdapter("SELECT rp.idregistropagos AS codigo,rp.monto,rp.razon,rp.estado,rp.fechapagar,"+
                        " rs.idregistroservicio AS codigoRegistro," +
                        " fp.Pago AS TipoPropiedad" +
                        " FROM registropagos rp INNER JOIN registroservicio rs" +
                        " ON rs.idregistroservicio=rp.idregistroservicio " +
                        " JOIN forma_pago fp ON fp.idforma_pago=rp.idformapago", nueva.nuevaConexion());
                    dat.Fill(ds);
                    TablaReporte.DataSource = ds.Tables[0];
                }
                else 
                    if (cboReportes.SelectedIndex == 4)//Registro Servicios
                {
                    dat = new OdbcDataAdapter("SELECT rs.idregistroservicio AS Codigo,rs.tiempocontrato,rs.fecha,rs.total," +
                        " pr.idpropiedad AS CodigoPropiedad," +
                        " cl.idClientes AS CodigoCliente," +
                        " em.idempleado AS CodigoEmpleado," +
                        " ts.idtiposervicio AS CodTipoServicio" +
                        " FROM registroservicio rs INNER JOIN propiedad pr" +
                        " ON pr.idpropiedad=rs.idpropiedad" +
                        " JOIN clientes cl ON rs.idClientes=cl.idClientes" +
                        " JOIN empleado em ON em.idempleado=rs.idempleados" +
                        " JOIN tiposervicio ts ON ts.idtiposervicio=rs.idtiposervicio", nueva.nuevaConexion());
                    dat.Fill(ds);
                    TablaReporte.DataSource = ds.Tables[0];

                }  
            }
            catch (OdbcException r)
            {
                MessageBox.Show(r.Message);
            };
        }  
    }
}
