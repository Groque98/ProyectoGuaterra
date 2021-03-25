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
    public partial class InfEmp : Form
    {
        
        public InfEmp()
        {
            InitializeComponent();
            
        }

        private void InitializeComponen()
        {
            this.SuspendLayout();
            // 
            // InfEmp
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "InfEmp";
            this.Load += new System.EventHandler(this.InfEmp_Load);
            this.ResumeLayout(false);
        }

        private void InfEmp_Load(object sender, EventArgs e)
        {
            this.crystalReportViewer1.ReportSource = ReporEmp1;
            /*ReporEmp1.VerifyDatabase();
            crystalReportViewer1.ReportSource = ReporEmp1;
            crystalReportViewer1.Refresh();
            ReporEmp1.Refresh();
            this.crystalReportViewer1.ReportSource = new InfEmp();*/

        }      
    }
}
