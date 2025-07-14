using SistemaDePersonal_GAN.Formularios.SUPERVISORES;
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
    public partial class FRMAltaAreas : Form
    {
        List<ClsSupervisor> lstsupervisores = new List<ClsSupervisor>();
        public FRMAltaAreas()
        {
            InitializeComponent();
            cargaSupervisores();
            
        }

        private void cargaSupervisores()
        {
            lstsupervisores.Clear();
            cmbSupervisores.DataSource = null;
            using (FileStream fs1 = new FileStream("supervisores.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr1 = new StreamReader(fs1))
            {
                string linea1 = "";
                string[] vl1 = new string[0];
                sr1.ReadLine();
                linea1 = sr1.ReadLine();
                while (linea1 != null)
                {
                    vl1 = linea1.Split(';');

                    ClsSupervisor sup = new ClsSupervisor(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4], vl1[5]);
                    lstsupervisores.Add(sup);

                    linea1 = sr1.ReadLine();
                }
            }
            cmbSupervisores.DataSource = lstsupervisores.Where(sup => sup.Area == "NULL").ToList();
            cmbSupervisores.SelectedIndex = -1;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcion.Text.Length > 0 && txtNombreArea.TextLength > 0 && cmbSupervisores.SelectedIndex != -1)
                {
                    string idAUX = "";
                    using (FileStream fs = new FileStream("IDsAreas.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string linea = "";
                        linea = sr.ReadLine();
                        idAUX = linea;
                    }
                    using (FileStream fs = new FileStream("IDsAreas.txt", FileMode.Truncate, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine((Convert.ToInt32(idAUX) + 1).ToString());
                    }

                    ClsArea area = new ClsArea("A" + idAUX, txtNombreArea.Text, txtDescripcion.Text, lstsupervisores.ElementAt(cmbSupervisores.SelectedIndex).DNI);
                    area.alta();
                    lstsupervisores.ElementAt(cmbSupervisores.SelectedIndex).Area = "A" + idAUX;


                    using (StreamWriter sw = new StreamWriter("supervisores.txt", false))
                    {
                        sw.Write("DNI;Nombre;Apellido;Email;FechaIngreso;Area");
                    }

                    foreach (ClsSupervisor sup in lstsupervisores)
                    {
                        using (StreamWriter sw = new StreamWriter("supervisores.txt", true))
                        {
                            sw.Write(Environment.NewLine + $"{sup.DNI};{sup.Nombre};{sup.Apellido};{sup.Email};{sup.FechaIngreso};{sup.Area}");
                        }
                    }

                    MessageBox.Show("Area creada con exito");
                    cargaSupervisores();
                    txtDescripcion.Text = string.Empty;
                    txtNombreArea.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Complete todos los campos");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
