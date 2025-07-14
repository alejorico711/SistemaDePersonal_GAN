using SistemaDePersonal_GAN.Clases;
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

namespace SistemaDePersonal_GAN.Formularios.SUPERUSUARIOS
{
    public partial class FRMNuevoSuperuser : Form
    {
        private string contraseña;
        public FRMNuevoSuperuser(string clave)
        {
            InitializeComponent();
            contraseña = clave;
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtDNI.Text, out int a) && txtDNI.TextLength == 8 && txtContraseña.TextLength == 8)
                {
                    bool existeDNI = false;
                    using (FileStream fs = new FileStream("empleados.txt", FileMode.Open, FileAccess.Read))
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
                        using (PasswordPrompt prompt = new PasswordPrompt())
                        {
                            if (prompt.ShowDialog() == DialogResult.OK)
                            {
                                string passwordIngresada = prompt.EnteredPassword;

                                if (passwordIngresada == contraseña) //validación
                                {
                                    // ✅ Contraseña correcta, crear superusuario
                                    using (StreamWriter sw = new StreamWriter("usuarios.txt", true))
                                    {
                                        sw.Write(Environment.NewLine + $"{txtDNI.Text};{txtContraseña.Text};4");
                                    }
                                    MessageBox.Show("Superusuario creado con exito");
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta. No se pudo crear el superusuario.");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Complete correctamente los campos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
