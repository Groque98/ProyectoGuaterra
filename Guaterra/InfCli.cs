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
    public partial class InfCli : Form
    {
        public InfCli()
        {
            InitializeComponent();
        }

        private void InitializeComponen()
        {
            this.SuspendLayout();
            // 
            // InfCli
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "InfCli";
            this.Load += new System.EventHandler(this.InfCli_Load);
            this.ResumeLayout(false);

        }

        private void InfCli_Load(object sender, EventArgs e)
        {

        }
    }
}
