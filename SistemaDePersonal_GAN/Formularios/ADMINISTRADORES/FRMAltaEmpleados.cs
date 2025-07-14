using SistemaDePersonal_GAN.Formularios;
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

namespace SistemaDePersonal_GAN
{
    public partial class FRMAltaEmpleados : Form
    {
        List<ClsEmpleado> lstEmpleados = new List<ClsEmpleado>();
        List<ClsEmpleado> lstEmpleados2 = new List<ClsEmpleado>();
        List<ClsArea> lstAreas = new List<ClsArea>();
        public FRMAltaEmpleados()
        {
            InitializeComponent();
            cargaCombo();
            

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
            cmbArea.DataSource = lstAreas;
            txtSupervisor.ReadOnly = true;
            cmbEmpleado.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;
            txtSupervisor.Text = string.Empty;
        }

        private void cargaCombo()
        {
            lstEmpleados2.Clear();
            lstEmpleados.Clear();
            cmbEmpleado.DataSource = null;
            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read)) //crea lista de usuarios no dados de alta
            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = "";
                string[] vl = new string[0];

                sr.ReadLine();
                linea = sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    if (vl[2] == "0" || vl[2]=="3")
                    {
                        using (FileStream fs1 = new FileStream("empleados.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr1 = new StreamReader(fs1))
                        {
                            string linea1 = "";
                            string[] vl1 = new string[0];
                            while (linea1 != null)
                            {
                                vl1 = linea1.Split(';');
                                if (vl[0] == vl1[0])
                                {
                                    ClsEmpleado emp = new ClsEmpleado(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4], vl1[5]);
                                    if (emp.Area == "NULL")
                                    {
                                        lstEmpleados2.Add(emp);
                                    }
                                    else
                                    {
                                        lstEmpleados.Add(emp);
                                    }
                                    
                                }
                                linea1 = sr1.ReadLine();
                            }
                        }
                    }
                    linea = sr.ReadLine();
                }
            }
            cmbEmpleado.DataSource = lstEmpleados2;
        }

        private void cmbArea_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cmbArea.SelectedIndex != -1)
            {
                ClsArea aux = lstAreas[cmbArea.SelectedIndex];
                using (FileStream fs = new FileStream("supervisores.txt", FileMode.Open, FileAccess.Read)) 
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
            try
            {
                if (cmbEmpleado.SelectedIndex != -1 && cmbArea.SelectedIndex != -1)
                {
                    lstEmpleados2[cmbEmpleado.SelectedIndex].Area = lstAreas.ElementAt(cmbArea.SelectedIndex).ID;

                    ClsEmpleado emp = lstEmpleados2[cmbEmpleado.SelectedIndex];
                    emp.alta();

                    using (StreamWriter sw = new StreamWriter("empleados.txt", false))
                    {
                        sw.Write("DNI;Nombre;Apellido;Email;FechaIngreso;Area");
                    }
                    foreach (var empleado in lstEmpleados)
                    {
                        using (StreamWriter sw = new StreamWriter("empleados.txt", true))
                        {
                            sw.Write(Environment.NewLine + $"{empleado.DNI};{empleado.Nombre};{empleado.Apellido};{empleado.Email};{empleado.FechaIngreso};{empleado.Area}");
                        }
                    }
                    foreach (var empleado in lstEmpleados2)
                    {
                        using (StreamWriter sw = new StreamWriter("empleados.txt", true))
                        {
                            sw.Write(Environment.NewLine + $"{empleado.DNI};{empleado.Nombre};{empleado.Apellido};{empleado.Email};{empleado.FechaIngreso};{empleado.Area}");
                        }
                    }

                    cmbArea.SelectedIndex = -1;
                    cmbEmpleado.SelectedIndex = -1;
                    txtSupervisor.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Complete todos los campos para dar de alta un empleado");
                }
                cargaCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
