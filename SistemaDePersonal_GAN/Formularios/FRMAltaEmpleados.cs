using SistemaDePersonal_GAN.Formularios;
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
    public partial class FRMAltaEmpleados : Form
    {
        List<ClsPersona> lstEmpleados = new List<ClsPersona>();
        List<ClsArea> lstAreas = new List<ClsArea>();
        public FRMAltaEmpleados()
        {
            InitializeComponent();

            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read)) //crea lista de usuarios no dados de alta
            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = "";
                string[] vl = new string[0];
                
                sr.ReadLine();
                linea=sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    if (vl[2] == "0")
                    {
                        using (FileStream fs1 = new FileStream("datosPersonales.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr1 = new StreamReader(fs1))
                        {
                            string linea1 = "";
                            string[] vl1 = new string[0];
                            while (linea1 != null)
                            {
                                vl1 = linea1.Split(';');
                                if (vl[0] == vl1[0])
                                {
                                    ClsPersona persona = new ClsPersona(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                                    lstEmpleados.Add(persona);
                                }
                                linea1 = sr1.ReadLine();
                            }
                        }
                    }
                    linea = sr.ReadLine();
                }
            }

            using (FileStream fs = new FileStream("areas.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = "";
                string[] vl = new string[0];

                sr.ReadLine();
                linea = sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    ClsArea area = new ClsArea(vl[0], vl[1], vl[2], vl[3]);
                    lstAreas.Add(area);
                    linea = sr.ReadLine();
                }
            }
            
            cmbEmpleado.DataSource = lstEmpleados;
            cmbArea.DataSource = lstAreas;
            txtSupervisor.ReadOnly = true;
            cmbEmpleado.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;
            txtSupervisor.Text = string.Empty;

        }

        private void cmbArea_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbArea.SelectedIndex != -1)
            {
                ClsArea aux = lstAreas[cmbArea.SelectedIndex];
                using (FileStream fs = new FileStream("datosPersonales.txt", FileMode.Open, FileAccess.Read)) 
                using (StreamReader sr = new StreamReader(fs))
                {
                    string linea = "";
                    string[] vl = new string[0];

                    sr.ReadLine();
                    linea = sr.ReadLine();
                    while (linea != null)
                    {
                        vl = linea.Split(';');
                        if (aux.IDSupervisor == vl[0])
                        {
                            txtSupervisor.Text = vl[1] + " " + vl[2];
                        }
                        linea = sr.ReadLine();
                    }
                }
            }
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            if (cmbEmpleado.SelectedIndex!=-1 && cmbArea.SelectedIndex!=-1)
            {
                ClsPersona emp = lstEmpleados[cmbEmpleado.SelectedIndex];
                ClsArea are = lstAreas[cmbArea.SelectedIndex];

                using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                using (FileStream fs1 = new FileStream("aux1.txt", FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs1))
                {
                    string linea = "";
                    string[] vl = new string[0];
                    linea = sr.ReadLine();
                    sw.Write(linea);
                    linea = sr.ReadLine();
                    while (linea != null)
                    {
                        vl = linea.Split(';');

                        if (vl[0] == emp.DNI)
                        {
                            sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "3");
                        }
                        else
                        {
                            sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2]);
                        }
                        linea = sr.ReadLine();
                    }
                }
                File.Delete("usuarios.txt");
                File.Move("aux1.txt", "usuarios.txt");

                using (FileStream fs1 = new FileStream("accesos.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs1))
                {
                    sw.Write(Environment.NewLine + emp.DNI + ';' + are.ID);
                }

                MessageBox.Show("Usuario dado de alta con exito");
                cmbArea.SelectedIndex = -1;
                cmbEmpleado.SelectedIndex = -1;
                txtSupervisor.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Complete todos los campos para dar de alta un empleado");
            }
            
        }

        private void cmbEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true; //ya que los combobox no tienen propiedad readonly, bloqueo manualmente
            }
        }

        private void cmbArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true; //ya que los combobox no tienen propiedad readonly, bloqueo manualmente
            }
        }
    }
}
