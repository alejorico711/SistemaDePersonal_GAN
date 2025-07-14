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

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public partial class FRMDatosPersonales : Form
    {
        ClsPersona persona;
        public FRMDatosPersonales(ClsPersona usuario)
        {
            InitializeComponent();
            persona = usuario;
            cargadatos();
        }

        private void cargadatos()
        {
            lblDNI.Text=persona.DNI;
            lblNombre.Text=persona.Nombre;
            lblApellido.Text=persona.Apellido;
            lblMail.Text = persona.Email;
            lblFechaIngreso.Text=persona.FechaIngreso;


            if(persona is ClsEmpleado aux)
            {
                using (FileStream fs = new FileStream("areas.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string linea = "";
                    string[] vl = new string[0];
                    linea = sr.ReadLine();
                    while (linea != null)
                    {
                        vl = linea.Split(';');
                        if (vl[0] == aux.Area)
                        {
                            lblArea.Text = vl[1];
                            lblCargo.Text = "Empleado";
                            break;
                        }

                        linea = sr.ReadLine();
                    }
                }
            }
            else if(persona is ClsSupervisor aux1)
            {
                using (FileStream fs = new FileStream("areas.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string linea = "";
                    string[] vl = new string[0];
                    linea = sr.ReadLine();
                    while (linea != null)
                    {
                        vl = linea.Split(';');
                        if (vl[0] == aux1.Area)
                        {
                            lblArea.Text = vl[1];
                            lblCargo.Text = "Supervisor";
                            break;
                        }

                        linea = sr.ReadLine();
                    }
                }
            }
            else if (persona is ClsAdministrador)
            {
                lblArea.Hide();
                label6.Hide();
                lblCargo.Text = "Administrador";
            }     
        }
    }
}
