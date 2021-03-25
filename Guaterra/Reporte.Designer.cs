namespace Guaterra.Reportes
{
    partial class Reporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboReportes = new System.Windows.Forms.ComboBox();
            this.TablaReporte = new System.Windows.Forms.DataGridView();
            this.cmdReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TablaReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de Informe";
            // 
            // cboReportes
            // 
            this.cboReportes.FormattingEnabled = true;
            this.cboReportes.Location = new System.Drawing.Point(157, 38);
            this.cboReportes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboReportes.Name = "cboReportes";
            this.cboReportes.Size = new System.Drawing.Size(140, 24);
            this.cboReportes.TabIndex = 1;
            this.cboReportes.SelectedIndexChanged += new System.EventHandler(this.cboReportes_SelectedIndexChanged);
            // 
            // TablaReporte
            // 
            this.TablaReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaReporte.Location = new System.Drawing.Point(15, 91);
            this.TablaReporte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TablaReporte.Name = "TablaReporte";
            this.TablaReporte.RowTemplate.Height = 24;
            this.TablaReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TablaReporte.Size = new System.Drawing.Size(884, 330);
            this.TablaReporte.TabIndex = 2;
            this.TablaReporte.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablaReporte_CellContentClick);
            this.TablaReporte.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablaReporte_CellValueChanged);
            // 
            // cmdReporte
            // 
            this.cmdReporte.Location = new System.Drawing.Point(347, 36);
            this.cmdReporte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdReporte.Name = "cmdReporte";
            this.cmdReporte.Size = new System.Drawing.Size(152, 32);
            this.cmdReporte.TabIndex = 3;
            this.cmdReporte.Text = "Generar Reporte";
            this.cmdReporte.UseVisualStyleBackColor = true;
            this.cmdReporte.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 453);
            this.Controls.Add(this.cmdReporte);
            this.Controls.Add(this.TablaReporte);
            this.Controls.Add(this.cboReportes);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Reporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Reporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboReportes;
        private System.Windows.Forms.DataGridView TablaReporte;
        private System.Windows.Forms.Button cmdReporte;
    }
}