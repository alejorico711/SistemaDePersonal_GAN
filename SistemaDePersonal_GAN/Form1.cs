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
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string linea = "";
            string[] vl = new string[0];

            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                linea = sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    if (vl[0] == txtUsuario.Text)
                    {
                        if (vl[1] == txtPassword.Text)
                        {
                            FRMPrincipal fRMPrincipal = new FRMPrincipal();
                            fRMPrincipal.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta");
                            txtPassword.Text = string.Empty;
                            
                        }
                    }
                    linea = sr.ReadLine();
                }
            }
        }
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private bool dragging = false;
        private Point dragCursorPoint;
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
                e.Handled = true; // Cancela la tecla si no es un número o control
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            FRMRegistro f = new FRMRegistro();
            f.Show();
            this.Hide();
        }
    }
}
