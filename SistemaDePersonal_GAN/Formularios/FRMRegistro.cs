using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN
{
    public partial class FRMRegistro : Form
    {
        public FRMRegistro()
        {
            InitializeComponent();
            panelTop.MouseDown += new MouseEventHandler(panelTop_MouseDown);
            panelTop.MouseMove += new MouseEventHandler(panelTop_MouseMove);  //3 eventos para poder arrastrar la ventana, ya que eliminamos los bordes de los formularios
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();   //cierra la aplicacion cuando se hace click sobre el picturebox
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;    //minimiza la aplicacion cuando se hace click sobre el picturebox
        }

        private bool dragging = false;
        private Point dragCursorPoint;   //variables y eventos que hacen posible el arrastre de la ventana
        private Point dragFormPoint;

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private int ContadorPaneles = 0;
        private void btnAgregarFamiliar_Click(object sender, EventArgs e)
        {
            //crear un nuevo panel
            Panel newPanel = new Panel
            {
                Size = new Size(236, 181),
                Location = new Point(0, ContadorPaneles * 186),  //sobre "x", siempre esta en "0" y en "y" va a estar dependiendo cuantos paneles haya
                BorderStyle = BorderStyle.FixedSingle
            };

            //crear controles dentro del nuevo panel
            Label lblNombre = new Label { Text = "Nombre", Location = new Point(10, 13), AutoSize = true };
            TextBox txtNombreFamiliar = new TextBox { Location = new Point(10, 28), Width = 100 };
            txtNombreFamiliar.Name = "txtNombreFamiliar";  //le pongo nombre al textbox propiamente dicho, ya que la linea anterior es el nombre de mi variable

            Label lblapellido = new Label { Text = "Apellido", Location = new Point(10, 49), AutoSize = true };
            TextBox txtApellidoFamiliar = new TextBox { Location = new Point(10, 64), Width = 100 };
            txtApellidoFamiliar.Name = "txtApellidoFamiliar";

            Label lblRelacion = new Label { Text = "Relacion", Location = new Point(10, 85), AutoSize = true };
            ComboBox cmbRelacionFamiliar = new ComboBox { Location = new Point(10, 100), Width = 100 };
            cmbRelacionFamiliar.Name = "cmbRelacionFamiliar";

            cmbRelacionFamiliar.Items.Add("Padre");  //agrego opciones al combobox
            cmbRelacionFamiliar.Items.Add("Madre");
            cmbRelacionFamiliar.Items.Add("Hijo");
            cmbRelacionFamiliar.Items.Add("Hermano");
            cmbRelacionFamiliar.Items.Add("Conyuge");
            cmbRelacionFamiliar.Items.Add("Otro");

            Label lblFN = new Label { Text = "Fecha de nacimiento", Location = new Point(10, 121), AutoSize = true };
            DateTimePicker dtpFechaFamiliar = new DateTimePicker { Location = new Point(10, 136), Width = 200, Value = DateTime.Today };
            dtpFechaFamiliar.Name = "dtpFechaFamiliar";

            Button btnx = new Button { Text = "X", Location = new Point(190, 15), Size = new Size(24, 23) };
            //para cada panel agrego un boton que me elimina el panel en caso de que se haya agregado de mas

            //agregar controles al nuevo panel
            newPanel.Controls.Add(lblNombre);
            newPanel.Controls.Add(txtNombreFamiliar);
            newPanel.Controls.Add(lblapellido);
            newPanel.Controls.Add(txtApellidoFamiliar);
            newPanel.Controls.Add(lblRelacion);
            newPanel.Controls.Add(cmbRelacionFamiliar);
            newPanel.Controls.Add(lblFN);
            newPanel.Controls.Add(dtpFechaFamiliar);
            newPanel.Controls.Add(btnx);

            btnx.Click += (s, ev) =>
            {
                panelFamiliares.Controls.Remove(newPanel); //me elimina el panel al que pertenecer al boton del panel panel contenedor
            }; //asigno el evento al boton que creo, ya que al ser creado por codigo no lo puedo asignar en otro momento

            //agregar el nuevo panel al contenedor principal
            panelFamiliares.Controls.Add(newPanel);

            //incrementar el contador de paneles
            ContadorPaneles++;
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //cancela la tecla si no es un número o control, es decir, evita que se pongan letras en el textbox DNI
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //cancela la tecla si no es una letra o control, es decir, evita que se pongan numerois en el textbox nombre
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //cancela la tecla si no es una letra o control, es decir, evita que se pongan numerois en el textbox apellido
            }
        }
        private void btnListo_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length > 0 && txtDNI.Text.Length > 0 && txtApellido.TextLength > 0 && txtEmail.TextLength > 0 && txtContraseña.Text.Length > 0)
            {//valido que todos los datos personales esten completos
                if (txtContraseña.TextLength < 8)//valido que la contraseña sea mayor de 8 digitos
                {
                    MessageBox.Show("Ingrese una contraseña con al menos 8 digitos"); //advierto que la contraseña debe tenr minimo 8 digitos
                }else if (txtDNI.TextLength < 8)//valido que el DNI sea mayor de 8 digitos
                {
                    MessageBox.Show("Ingrese un DNI valido");//advierto que el DNI debe tenr minimo 8 digitos
                }
                else //si todo esta bien empieza la registracion
                {
                    bool existeDNI = false;
                    using (FileStream fs = new FileStream("datosPersonales.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string linea = "";
                        string[] vl = new string[0];
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            vl = linea.Split(';');
                            if (vl[0] == txtDNI.Text)  //compruebo si el DNI no esta ya registrado
                            {
                                existeDNI = true;
                            }
                            linea = sr.ReadLine();
                        }
                    }
                    if (existeDNI) //si existe el dni, advierte
                    {
                        MessageBox.Show("DNI ya registrado");
                    }
                    else
                    {
                        bool incompleto = false;
                        foreach (Control control in panelFamiliares.Controls) //para cada control dentro del panel contendor pregunto si es un panel
                        {
                            if (control is Panel panel) //si es un panel, le pido que busque los mismos controles que cree arriba
                            {
                                TextBox txtNombreFamiliar = panel.Controls.Find("txtNombreFamiliar", true).FirstOrDefault() as TextBox;
                                TextBox txtApellidoFamiliar = panel.Controls.Find("txtApellidoFamiliar", true).FirstOrDefault() as TextBox;
                                ComboBox cmbRelacionFamiliar = panel.Controls.Find("cmbRelacionFamiliar", true).FirstOrDefault() as ComboBox;
                                DateTimePicker dtpFechaFamiliar = panel.Controls.Find("dtpFechaFamiliar", true).FirstOrDefault() as DateTimePicker;

                                if (txtNombreFamiliar.Text == string.Empty || cmbRelacionFamiliar.SelectedIndex==-1 || txtApellidoFamiliar.Text == string.Empty || txtNombreFamiliar.Text == string.Empty)
                                {
                                    incompleto = true;
                                }
                            }
                                
                        }
                        if (incompleto) //si esta incompleto advierte
                        {
                            MessageBox.Show("Complete todos los campos para cada familiar");
                        }
                        else //entra si todos los familiares estan completos y comienza la registracion
                        {
                            int aux = 1;
                            foreach (Control control in panelFamiliares.Controls)
                            {
                                if (control is Panel panel)
                                {
                                    TextBox txtNombreFamiliar = panel.Controls.Find("txtNombreFamiliar", true).FirstOrDefault() as TextBox;
                                    TextBox txtApellidoFamiliar = panel.Controls.Find("txtApellidoFamiliar", true).FirstOrDefault() as TextBox;
                                    ComboBox cmbRelacionFamiliar = panel.Controls.Find("cmbRelacionFamiliar", true).FirstOrDefault() as ComboBox;
                                    DateTimePicker dtpFechaFamiliar = panel.Controls.Find("dtpFechaFamiliar", true).FirstOrDefault() as DateTimePicker;

                                    using (FileStream fs = new FileStream("gruposFamiliares.txt", FileMode.Append, FileAccess.Write))
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {//me escribe el archivo de grupos fgamiliares
                                        sw.Write(Environment.NewLine + txtDNI.Text + ';' + aux.ToString() + ';' + txtNombreFamiliar.Text + ';' + txtApellidoFamiliar.Text + ';' + cmbRelacionFamiliar.Text + ';' + dtpFechaFamiliar.Value.ToString("dd/MM/yyyy"));
                                    }
                                    aux++;
                                }
                            }

                            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Append, FileAccess.Write))
                            using (StreamWriter sw = new StreamWriter(fs))
                            {//me escribe el archivo de usuarios
                                //DNI; Contraseña; Nivel(1 - administrador, 2 - supervisor, 3 - empleado)
                                sw.Write(Environment.NewLine + txtDNI.Text + ';' + txtContraseña.Text + ';' + "0");
                            }

                            using (FileStream fs = new FileStream("datosPersonales.txt", FileMode.Append, FileAccess.Write))
                            using (StreamWriter sw = new StreamWriter(fs))
                            {//me escribe el archivo de sus datos personales
                                //DNI;Nombre;Apellido;Email;FechaIngreso
                                sw.Write(Environment.NewLine + txtDNI.Text + ';' + txtNombre.Text + ';' +txtApellido.Text + ';' + txtEmail.Text + ';' + DateTime.Today.ToString("dd/MM/yyyy"));
                            }

                            MessageBox.Show("Usuario registrado con exito"); //aviso que el usuario fue registrado con exito y vuelvo al formulario del login
                            this.Close();
                            Form1 f = new Form1();
                            f.Show();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Complete todos los campos");
            }
        }

        
    }
}
