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

namespace SistemaDePersonal_GAN.Formularios
{
    public partial class FRMBajaEmpledos : Form
    {
        List<ClsPersona> lstEmpleados = new List<ClsPersona>();
        public FRMBajaEmpledos()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            actualizaData();
        }

        private void actualizaData()
        {
            lstEmpleados.Clear();
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
                    if (vl[2] == "3")
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
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lstEmpleados;
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                ClsPersona emp = lstEmpleados[dataGridView1.CurrentRow.Index];

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
                            sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "0");
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

                using (FileStream fs1 = new FileStream("accesos.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs1))
                using (FileStream fs2 = new FileStream("aux1.txt", FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs2))
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
                            
                        }
                        else
                        {
                            sw.Write(Environment.NewLine + vl[0] + ';' + vl[1]);
                        }
                        linea = sr.ReadLine();
                    }
                }
                File.Delete("accesos.txt");
                File.Move("aux1.txt", "accesos.txt");
                MessageBox.Show("Usuario dado de baja con exito");
                actualizaData();
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para dar de baja");
            }
        }
    }
}
