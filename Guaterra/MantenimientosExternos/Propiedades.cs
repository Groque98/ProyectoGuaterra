using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guaterra.MantenimientosExternos
{
    public partial class Propiedades : Form
    {
        Conexion nueva = new Conexion();
        OdbcCommand cmd;
        Boolean ingresoCorrecto = true;
       
        public Propiedades()
        {
            InitializeComponent();
            llenarTabla();
            llenarcombobox();
            llenarcombobox1();
        }


        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        
        public static byte[] convertircodigoaimagen(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }




        public void llenarcombobox()//TipoPropiedad
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Tipo FROM tipo_propiedad", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cboPropiedad.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }

            try {
                OdbcCommand sql = new OdbcCommand("SELECT Nombre FROM clientes",nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read()==true) {
                    cmbDueño.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e) {
                MessageBox.Show(e.ToString());
            }
        }
        public void llenarcombobox1()//Departamento
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Nombre FROM departamento", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cboDepart.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void llenarcombobox2()//Municipio
        {
            try
            {
                OdbcCommand sql = new OdbcCommand("SELECT Municipio FROM municipio", nueva.nuevaConexion());
                OdbcDataReader almacena = sql.ExecuteReader();
                while (almacena.Read() == true)
                {
                    cboMuni.Items.Add(almacena.GetValue(0));
                }
                almacena.Close();
            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void llenarTabla()
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT pr.idPropiedad AS Codigo,pr.MetrosCuadrados,pr.Descripcion,pr.Direccion,pr.Costo,pr.No_Niveles," +
                    " tip.Tipo," +
                    " c.Nombre AS Dueño," +
                    " mun.Municipio," +
                    " dept.Nombre AS Departamento," +
                    " CASE pr.disponibilidad "+
         "when '1' then 'Si' "+
         "when '0' then 'No' "+
         "end as Disponible "+
                    " FROM propiedad pr INNER JOIN tipo_propiedad tip" +
                    " ON tip.idTipo_Propiedad=pr.idTipo_Propiedad " +
                    " JOIN clientes c ON pr.idClientes=c.idClientes " +
                    " JOIN municipio mun ON mun.idMunicipio=pr.idMunicipio" +
                    " JOIN departamento dept ON dept.idDepartamento=mun.idDepartamento", nueva.nuevaConexion());

                dat.Fill(ds);
                TablaPropiedades.DataSource = ds.Tables[0];

                //Convert.ToBase64String(TablaPropiedades.CurrentRow.Cells[11]
            


            }
            catch (OdbcException e)
            {
                MessageBox.Show(e.Message);
            };

          
        }
        private void Propiedades_Load(object sender, EventArgs e)
        {
               
 btnCancelar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;
            txtidPropiedades.Enabled = false;
            cboPropiedad.Enabled = false;
            cmbDueño.Enabled = false;
            cboDepart.Enabled = false;
            cboMuni.Enabled = false;
            txtDescrip.Enabled = false;
            txtMetros.Enabled = false;
            txtNiveles.Enabled = false;
            txtDireccion.Enabled = false;
            txtCosto.Enabled = false;
            checkdisponible.Enabled = false;
            pbox.Enabled = false;
            btnimagen.Enabled = false;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cboPropiedad.Enabled = true;
            cboDepart.Enabled = true;
            cboMuni.Enabled = true;
            txtDescrip.Enabled = true;
            txtMetros.Enabled = true;
            txtNiveles.Enabled = true;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
            txtDireccion.Enabled = true;
            cmbDueño.Enabled = true;
            txtCosto.Enabled = true;
            checkdisponible.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //---------------------------------------------------------Tipo_Propiedad
            string Tipo = "";           
            bool validaciontipo = false;

            if (cboPropiedad.SelectedItem != null)
            {
                string nTipo = cboPropiedad.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idTipo_Propiedad FROM tipo_propiedad WHERE Tipo = '" + nTipo + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        Tipo = almacena.GetString(0);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                validaciontipo = true;
            }
            //--------------------------------------------------------------Municipio
            string municipio = "";
            bool validacionmuni = false;

            if (cboMuni.SelectedItem != null)
            {
                string nMuni = cboMuni.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idMunicipio FROM municipio WHERE Municipio ='"+ nMuni +"'",nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        municipio = almacena.GetString(0);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                validacionmuni = true;
            }
            //-------------------------------------------------------------Dueño
            string dueño = "";
            bool validadueño = false;

            if (cmbDueño.SelectedItem != null) {
                string nDueño = cmbDueño.SelectedItem.ToString();

                try {
                    OdbcCommand sql = new OdbcCommand("SELECT idClientes FROM clientes WHERE Nombre ='"+nDueño+"'",nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read()==true) {
                        dueño = almacena.GetString(0);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
            else {
                validadueño = true;
            }
            //------------------------- 
            if (txtMetros.Text == "" || txtNiveles.Text == "" || txtDescrip.Text == "" || cboPropiedad.Text == "" || cboDepart.Text == "" || cboMuni.Text == "" || cmbDueño.Text ==""||linkimagen.Text=="")
            {
                MessageBox.Show("Hacen Falta Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {
                bool x;
                if (checkdisponible.Checked == true)
                    x = true;
                else
                    x = false;

                //converitr imagen a binario para bd
                FileStream stream = new FileStream(linkimagen.Text, FileMode.Open, FileAccess.Read);
                //Se inicailiza un flujo de archivo con la imagen seleccionada desde el disco.
                BinaryReader br = new BinaryReader(stream);
                FileInfo fi = new FileInfo(linkimagen.Text);

                //Se inicializa un arreglo de Bytes del tamaño de la imagen
                byte[] binData = new byte[stream.Length];
                //Se almacena en el arreglo de bytes la informacion que se obtiene del flujo de archivos(foto)
                //Lee el bloque de bytes del flujo y escribe los datos en un búfer dado.
                stream.Read(binData, 0, Convert.ToInt32(stream.Length));

                ////Se muetra la imagen obtenida desde el flujo de datos
                pbox.Image = Image.FromStream(stream);

                cmd = new OdbcCommand("UPDATE propiedad SET MetrosCuadrados='"
                    + txtMetros.Text + "',Descripcion='"
                    + txtDescrip.Text + "',Direccion = '"
                    + txtDireccion.Text + "',Costo='"
                    + txtCosto.Text + "',No_Niveles='"
                    + txtNiveles.Text + "',idTipo_Propiedad='"
                    + Tipo + "',idMunicipio='"
                    + municipio + "',idClientes='"
                    + dueño + "',disponibilidad='"          
                    + x + "',imagen='" 
                    + Convert.ToBase64String(binData) + "' WHERE idPropiedad='"
                    + txtidPropiedades.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();



                if (ingresoCorrecto)
                {
                    MessageBox.Show("Propiedad Modificado Correctamente");
                    txtidPropiedades.Text = "";
                    txtMetros.Text = "";
                    txtNiveles.Text = "";
                    txtDescrip.Text = "";
                    cboPropiedad.Text = "";
                    cboDepart.Text = "";
                    cboMuni.Text = "";
                    txtDireccion.Text = "";
                    cmbDueño.Text = "";
                    txtCosto.Text = "";
                    pbox.Image = null;
                    checkdisponible.Checked = false;

                    txtMetros.Enabled = false;
                    txtNiveles.Enabled = false;
                    txtDescrip.Enabled = false;
                    cboPropiedad.Enabled = false;
                    cboDepart.Enabled = false;
                    cboMuni.Enabled = false;
                    txtDireccion.Enabled = false;
                    cmbDueño.Enabled = false;
                    txtCosto.Enabled = false;
                    checkdisponible.Enabled = false;

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
            //---------------------------------------------------------Tipo_Propiedad
            string Tipo = "";
            bool validaciontipo = false;

            if (cboPropiedad.SelectedItem != null)
            {
                string nTipo = cboPropiedad.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idTipo_Propiedad FROM tipo_propiedad WHERE Tipo = '" + nTipo + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        Tipo = almacena.GetString(0);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                validaciontipo = true;
            }
            
            //--------------------------------------------------------------Municipio
            string municipio = "";
            bool validacionmuni = false;
            

            if (cboMuni.SelectedItem != null)
            {
                string nMuni = cboMuni.SelectedItem.ToString();
                //Consulta El Codigo Del Puesto Obtenido Del Texto
                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idMunicipio FROM municipio WHERE Municipio = '" + nMuni + "'"
                        , nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        municipio = almacena.GetString(0);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
            else
            {
                validacionmuni = true;
            }
            //-------------------------------------------------------------Dueño
            string dueño = "";
            bool validadueño = false;

            if (cmbDueño.SelectedItem != null)
            {
                string nDueño = cmbDueño.SelectedItem.ToString();

                try
                {
                    OdbcCommand sql = new OdbcCommand("SELECT idClientes FROM clientes WHERE Nombre ='" + nDueño + "'", nueva.nuevaConexion());
                    OdbcDataReader almacena = sql.ExecuteReader();
                    while (almacena.Read() == true)
                    {
                        dueño = almacena.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                validadueño = true;
            }

            //================================ CAMPO DISPONIBILIDAD====================================

            int checkdisponibilidad = 0;

            if (checkdisponible.Checked == true)
            {
                checkdisponibilidad = 1;
            }
            else
            {
                checkdisponibilidad = 0;
            }


            //-------------------------INGRESAR DATOS A TABLA_PROPIEDADES---------------------------------------------------
            //==============================================================================================================

            if (txtMetros.Text == "" || txtNiveles.Text == "" || txtDescrip.Text == "" || cboPropiedad.Text == "" || cboDepart.Text == "" || cboMuni.Text == ""||cmbDueño.Text=="" || linkimagen.Text=="")
            {
                MessageBox.Show("Hacen Campos Por Llenar", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ingresoCorrecto = false;
            }
            else
            {

                try
                {
                    //======================================================================================================
                    //===============================CONVERSION DE IMAGEN A BYTES =========================================
                    //=====================================================================================================
                    FileStream stream = new FileStream(linkimagen.Text, FileMode.Open, FileAccess.Read);
                    //Se inicailiza un flujo de archivo con la imagen seleccionada desde el disco.
                    BinaryReader br = new BinaryReader(stream);
                    FileInfo fi = new FileInfo(linkimagen.Text);

                    //Se inicializa un arreglo de Bytes del tamaño de la imagen
                    byte[] binData = new byte[stream.Length];
                    //Se almacena en el arreglo de bytes la informacion que se obtiene del flujo de archivos(foto)
                    //Lee el bloque de bytes del flujo y escribe los datos en un búfer dado.
                    stream.Read(binData, 0, Convert.ToInt32(stream.Length));

                    ////Se muetra la imagen obtenida desde el flujo de datos
                    pbox.Image = Image.FromStream(stream);

                    cmd = new OdbcCommand("INSERT INTO propiedad(MetrosCuadrados,Descripcion,Direccion,Costo,No_Niveles,idTipo_Propiedad,idMunicipio,idClientes,disponibilidad,imagen)" +
                        "VALUES('" + txtMetros.Text + "','"
                         + txtDescrip.Text + "','"
                         + txtDireccion.Text + "','"
                         + txtCosto.Text + "','"
                         + txtNiveles.Text + "','"
                        + Tipo + "', '"
                        + municipio + "','"
                        + dueño + "','"
                        + checkdisponibilidad + "','"
                        + Convert.ToBase64String(binData) + "')", nueva.nuevaConexion()) ;
                    Console.WriteLine(binData);
                    cmd.ExecuteNonQuery();

           
                }
                catch (OdbcException E)
                {
                    MessageBox.Show(E.Message);
                    ingresoCorrecto = false;
                }

                if (ingresoCorrecto)
                {
                    MessageBox.Show("Propiedad Ingresado Correctamente");
                    txtidPropiedades.Text = "";
                    txtMetros.Text = "";
                    txtNiveles.Text = "";
                    txtDescrip.Text = "";
                    cboPropiedad.Text = "";
                    cboDepart.Text = "";
                    cboMuni.Text = "";
                    txtCosto.Text = "";
                    txtDireccion.Text = "";
                    cmbDueño.Text = "";
                    linkimagen.Text = "";
                    checkdisponible.Checked = false;


                    txtMetros.Enabled = false;
                    txtNiveles.Enabled = false;
                    txtDescrip.Enabled = false;
                    cboPropiedad.Enabled = false;
                    cboDepart.Enabled = false;
                    cboMuni.Enabled = false;
                    txtDireccion.Enabled = false;
                    cmbDueño.Enabled = false;
                    txtCosto.Enabled = false;
                    checkdisponible.Enabled = false;


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
                //Elimina propiedad
                cmd = new OdbcCommand("DELETE FROM propiedad WHERE idPropiedad='"
                    + txtidPropiedades.Text + "'", nueva.nuevaConexion());
                cmd.ExecuteNonQuery();
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
                ingresoCorrecto = false;
            }

            if (ingresoCorrecto)
            {
                MessageBox.Show("propiedad Eliminado Correctamente");
                txtidPropiedades.Text = "";
                txtMetros.Text = "";
                txtNiveles.Text = "";
                txtDescrip.Text = "";
                cboPropiedad.Text = "";
                cboDepart.Text = "";
                cboMuni.Text = "";
                cmbDueño.Text = "";
                txtDireccion.Text = "";
                txtCosto.Text = "";

                txtMetros.Enabled = false;
                txtNiveles.Enabled = false;
                txtDescrip.Enabled = false;
                cboPropiedad.Enabled = false;
                cboDepart.Enabled = false;
                cboMuni.Enabled = false;
                txtDireccion.Enabled = false;
                cmbDueño.Enabled = false;
                txtCosto.Enabled = false;

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

            txtidPropiedades.Enabled = false;
            txtMetros.Enabled = false;
            txtNiveles.Enabled = false;
            txtDescrip.Enabled = false;
            cboPropiedad.Enabled = false;
            cboDepart.Enabled = false;
            cboMuni.Enabled = false;
            txtDireccion.Enabled = false;
            cmbDueño.Enabled = false;
            txtCosto.Enabled = false;
            checkdisponible.Enabled = false;

            txtidPropiedades.Text = "";
            txtMetros.Text = "";
            txtNiveles.Text = "";
            txtCosto.Text = "";
            txtDescrip.Text = "";
            cboPropiedad.Text = "";
            cboDepart.Text = "";
            cboMuni.Text = "";
            txtDireccion.Text = "";
            cmbDueño.Text = "";
            checkdisponible.Checked = false;
            
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void TablaPropiedades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (TablaPropiedades.CurrentRow.Cells[0].Value.ToString() != "")
            {

                btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = true;
                txtMetros.Enabled = true;
                txtNiveles.Enabled = true;
                txtDescrip.Enabled = true;
                cboMuni.Enabled = true;
                cboDepart.Enabled = true;
                cboPropiedad.Enabled = true;
                txtDireccion.Enabled = true;
                txtCosto.Enabled = true;
                cmbDueño.Enabled = true;
                checkdisponible.Enabled = true;
                btnimagen.Enabled = true;

                txtidPropiedades.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[0].Value);
                cboPropiedad.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[6].Value);
                cboMuni.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[8].Value);
                cboDepart.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[9].Value);
                txtDireccion.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[3].Value);
                txtDescrip.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[2].Value);
                txtMetros.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[1].Value);
                txtNiveles.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[5].Value);
                txtCosto.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[4].Value);
                cmbDueño.Text = Convert.ToString(TablaPropiedades.CurrentRow.Cells[7].Value);
               
                //Convert.ToBase64String(TablaPropiedades.CurrentRow.Cells[11]
                //convertircodigoaimagen(linkimagen.ToString());

             
               
                
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;



                if (Convert.ToString(TablaPropiedades.CurrentRow.Cells[10].Value)=="Si")
                {
                    checkdisponible.Checked = true;
                }
                else
                {
                    checkdisponible.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Campo Vacio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboPropiedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMuni.Text = "";
        }

        private void cboMuni_Click(object sender, EventArgs e)
        {
            cboMuni.DataSource = null;
            cboMuni.Items.Clear();
            OdbcCommand sql = new OdbcCommand("SELECT Municipio from " +
                "Municipio m inner JOIN departamento d on m.idDepartamento=d.idDepartamento " +
                "WHERE d.Nombre='" + cboDepart.Text + "'", nueva.nuevaConexion());
            OdbcDataReader almacena = sql.ExecuteReader();
            while (almacena.Read() == true)
            {
                cboMuni.Items.Add(almacena.GetValue(0));
            }
            almacena.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OdbcDataAdapter dat;
            DataSet ds;
            try
            {
                ds = new DataSet();
                dat = new OdbcDataAdapter("SELECT pr.idPropiedad AS Codigo,pr.MetrosCuadrados,pr.Descripcion,pr.Direccion,pr.Costo,pr.No_Niveles," +
                    " tip.Tipo," +
                    " c.Nombre AS Dueño," +
                    " mun.Municipio," +
                    " dept.Nombre AS Departamento" +
                    " FROM propiedad pr INNER JOIN tipo_propiedad tip" +
                    " ON tip.idTipo_Propiedad=pr.idTipo_Propiedad " +
                    " JOIN clientes c ON pr.idClientes=c.idClientes " +
                    " JOIN municipio mun ON mun.idMunicipio=pr.idMunicipio" +
                    " JOIN departamento dept ON dept.idDepartamento=mun.idDepartamento"+
                    " WHERE c.Nombre='" + txtBuscar.Text + "' OR pr.idPropiedad='" + txtBuscar.Text + "'", nueva.nuevaConexion());
                dat.Fill(ds);
                TablaPropiedades.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)

                    MessageBox.Show("¡Registro No Existe!");
                else
                
                    MessageBox.Show("¡Propiedad Encontrada");
               
                txtBuscar.Text = "";
            }
            catch (OdbcException ex)
            {
                MessageBox.Show(ex.Message);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog examinar = new OpenFileDialog();
            examinar.Filter = "image files|*.jpg;*.png;*.gif;*.ico;.*;";
            DialogResult dres1 = examinar.ShowDialog();
            if (dres1 == DialogResult.Abort)
                return;
            if (dres1 == DialogResult.Cancel)
                return;
            linkimagen.Text = examinar.FileName;
            pbox.Image = Image.FromFile(examinar.FileName);
           pbox.SizeMode=PictureBoxSizeMode.StretchImage;

            FileStream stream = new FileStream(linkimagen.Text, FileMode.Open, FileAccess.Read);
            //Se inicailiza un flujo de archivo con la imagen seleccionada desde el disco.
            BinaryReader br = new BinaryReader(stream);
            FileInfo fi = new FileInfo(linkimagen.Text);

            //Se inicializa un arreglo de Bytes del tamaño de la imagen
            byte[] binData = new byte[stream.Length];
            //Se almacena en el arreglo de bytes la informacion que se obtiene del flujo de archivos(foto)
            //Lee el bloque de bytes del flujo y escribe los datos en un búfer dado.
            stream.Read(binData, 0, Convert.ToInt32(stream.Length));

            ////Se muetra la imagen obtenida desde el flujo de datos
            pbox.Image = Image.FromStream(stream); 
            Console.WriteLine(Convert.ToBase64String(binData));


        }

        private void TablaPropiedades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
