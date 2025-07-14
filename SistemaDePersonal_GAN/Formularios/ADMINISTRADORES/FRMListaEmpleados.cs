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
    public partial class FRMListaEmpleados : Form
    {
        List<ClsEmpleado> lstEmpleados = new List<ClsEmpleado>();
        public FRMListaEmpleados()
        {
            InitializeComponent();

            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
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
                                    //ClsPersona persona = new ClsPersona(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                                    lstEmpleados.Add(emp);
                                }
                                linea1 = sr1.ReadLine();
                            }
                        }
                    }
                    linea = sr.ReadLine();
                }
            }
            dataGridView1.DataSource = lstEmpleados;
            dataGridView1.ReadOnly = true;
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            List<ClsEmpleado> coincidencias = lstEmpleados
                    .Where(emp => emp.DNI.ToLower().StartsWith(txtBusqueda.Text))
                    .ToList();
            dataGridView1.DataSource = coincidencias;
        }
    }
}
