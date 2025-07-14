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

namespace SistemaDePersonal_GAN.Formularios.ADMINISTRADORES
{
    public partial class FRMListaEmpleadosDeArea : Form
    {
        List<ClsEmpleado> lstEmpleados = new List<ClsEmpleado>();
        public FRMListaEmpleadosDeArea(ClsSupervisor supervisor)
        {
            InitializeComponent();
            using (FileStream fs1 = new FileStream("empleados.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr1 = new StreamReader(fs1))
            {
                string linea1 = "";
                string[] vl1 = new string[0];
                linea1 = sr1.ReadLine();
                while (linea1 != null)
                {
                    vl1 = linea1.Split(';');
                    if (vl1[5] == supervisor.Area)
                    {
                        ClsEmpleado emp = new ClsEmpleado(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4], vl1[5]);
                        //ClsPersona persona = new ClsPersona(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                        lstEmpleados.Add(emp);
                    }
                    linea1 = sr1.ReadLine();
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