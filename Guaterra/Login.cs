using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;

namespace Guaterra.MantenimientosExternos.Servicios
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Conexion nueva = new Conexion();
            OdbcCommand cmd;

            string User = this.txtUsuario.Text;
            string Password = this.txtPassword.Text;

            if (User== "" ||Password =="") {
                MessageBox.Show("Ingrese Usuario y Contraseña","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.txtUsuario.Focus();
                return;
            }
            else {
                cmd = new OdbcCommand("SELECT idTipo_Usuario FROM usuario WHERE Nombre_Usuario LIKE '%"+ txtUsuario.Text
                   + "%' AND Contraseña_Usuario LIKE '%" +txtPassword.Text + "%'"
                   ,nueva.nuevaConexion());

                OdbcDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var f = new Form1();
                    f.Show();
                    txtUsuario.Clear();
                    txtPassword.Clear();
                    this.Hide();
                }
                else {
                    MessageBox.Show("Ingrese Datos Nuevamente");
                    txtUsuario.Clear();
                    txtPassword.Clear();
                }
            }
        }
    }
}
