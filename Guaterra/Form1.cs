using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guaterra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void arrendamientossToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void compradoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientosExternos.Servicios.Compradores compradores = new MantenimientosExternos.Servicios.Compradores();
            compradores.MdiParent = this;
            compradores.Show();
        }

        private void empleadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.Empleados emps = new MantenimientoInternos.Empleados();
            emps.MdiParent = this;
            emps.Show();
        }

        private void puestosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.Puestos emps = new MantenimientoInternos.Puestos();
            emps.MdiParent = this;
            emps.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientosExternos.Clientes emps = new MantenimientosExternos.Clientes();
            emps.MdiParent = this;
            emps.Show();
        }

        private void propiedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientosExternos.Propiedades emps = new MantenimientosExternos.Propiedades();
            emps.MdiParent = this;
            emps.Show();
        }

        private void ArrendadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientosExternos.Servicios.Arrendadores alquileres = new MantenimientosExternos.Servicios.Arrendadores();
            alquileres.MdiParent = this;
            alquileres.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.Usuarios Usua = new MantenimientoInternos.Usuarios();
            Usua.MdiParent = this;
            Usua.Show();
        }

        private void informesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void enganchesEInteresesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.CuotasEngancheIntereses intereses = new MantenimientoInternos.CuotasEngancheIntereses();
            intereses.MdiParent = this;
            intereses.Show();

           
        }

        private void tipoDePropiedadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.tipopropiedades tp = new MantenimientoInternos.tipopropiedades();
            tp.MdiParent = this;
            tp.Show();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MantenimientoInternos.Comisiones comisiones = new MantenimientoInternos.Comisiones();
            comisiones.MdiParent = this;
            comisiones.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void informesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reportes.Reporte repor = new Reportes.Reporte();
            repor.MdiParent = this;
            repor.Show();
        }
    }
}
