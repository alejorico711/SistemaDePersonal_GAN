using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panelTop.MouseDown += new MouseEventHandler(panelTop_MouseDown);
            panelTop.MouseMove += new MouseEventHandler(panelTop_MouseMove);
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);        //3 eventos para poder arrastrar la ventana, ya que eliminamos los bordes de los formularios
            txtPassword.UseSystemPasswordChar = true;                           //propiedad para que se vean los puntitos en vez de la contraseña
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text)==false || string.IsNullOrEmpty(txtUsuario.Text)==false)  //valido que el dni y la contraseña noe sten vacios
            {
                if (txtUsuario.Text.Length < 8 && txtPassword.Text.Length < 8)      //valido que el dni y la contraseña no sean menores a 8 caracteres
                {
                    MessageBox.Show("Ingrese un nombre de usuario y una contraseña valida");    //advertencia para ingresar un usuario valido
                    txtPassword.Text = string.Empty;
                    txtUsuario.Text = string.Empty;    //vacio los controles
                }
                else
                {
                    string linea = "";
                    string[] vl = new string[0];
                    bool noExiste = true;              //booleano para identificar si el usuario no existe

                    using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            vl = linea.Split(';');
                            if (vl[0] == txtUsuario.Text)
                            {
                                noExiste = false;
                                if (vl[1] == txtPassword.Text)
                                {
                                    FRMPrincipal fRMPrincipal = new FRMPrincipal();
                                    fRMPrincipal.Show();
                                    this.Hide();
                                }
                                else          //condicional para validar que la contraseña sea correcta
                                {
                                    MessageBox.Show("Contraseña incorrecta");
                                    txtPassword.Text = string.Empty;
                                }
                            }
                            linea = sr.ReadLine();
                        }
                        if (noExiste)
                        {
                            MessageBox.Show("Usuario no encontrado");   //vacio los controles y aviso que el usuario no fue encontrado
                            txtPassword.Text = string.Empty;
                            txtUsuario.Text = string.Empty;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Complete todos los campos");
            }
        }
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); //cierra la aplicacion cuando se hace click sobre el picturebox
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //minimiza la aplicacion cuando se hace click sobre el picturebox
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

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; //cancela la tecla si no es un número o control, es decir, evita que se pongan letras en el textbox DNI
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            FRMRegistro f = new FRMRegistro();        //abre el formulario de registro
            f.Show();
            this.Hide();
        }
    }
}
