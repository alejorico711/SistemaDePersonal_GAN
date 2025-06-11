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
                            MessageBox.Show("entra");
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
    }
}
