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
    public partial class FRMBajaAreas : Form
    {
        List<ClsArea> lstareas = new List<ClsArea>();
        List<ClsSupervisor> lstsupervisores = new List<ClsSupervisor>();
        public FRMBajaAreas()
        {
            InitializeComponent();

            dgvAreas.ReadOnly = true;
            dgvAreas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAreas.MultiSelect = false;

            cargaAreas();
            cargaSupervisores();
        }

        private void cargaSupervisores()
        {
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
        }

        private void cargaAreas()
        {
            lstareas = File.ReadLines("areas.txt")
                .Skip(1)
                .Select(linea => creaArea(linea))
                .ToList();
            dgvAreas.DataSource = lstareas;
        }

        private ClsArea creaArea(string linea)
        {
            var datos = linea.Split(';');
            ClsArea area = new ClsArea(datos[0], datos[1], datos[2], datos[3]);
            return area;
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAreas.SelectedRows.Count == 1)
                {
                    ClsArea area = lstareas.ElementAt(dgvAreas.SelectedRows[0].Index);
                    foreach (var sup in lstsupervisores.Where(sup => sup.DNI == area.IDSupervisor))
                    {
                        sup.Area = "NULL";
                    }
                    area.baja();
                    cargaAreas();
                }
                else
                {
                    MessageBox.Show("Seleccione un area");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
