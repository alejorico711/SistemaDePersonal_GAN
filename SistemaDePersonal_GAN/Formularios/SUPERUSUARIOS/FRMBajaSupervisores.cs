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
    public partial class FRMBajaSupervisores : Form
    {
        List<ClsSupervisor> lstsupervisores = new List<ClsSupervisor>();
        public FRMBajaSupervisores()
        {
            InitializeComponent();

            dgvSupervisores.ReadOnly = true;
            dgvSupervisores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSupervisores.MultiSelect = false;

            cargasupervisores();
        }

        private void cargasupervisores()
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
            dgvSupervisores.DataSource = lstsupervisores;
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgvSupervisores.SelectedRows.Count == 1)
            {
                try
                {
                    ClsSupervisor sup = lstsupervisores[dgvSupervisores.SelectedRows[0].Index];
                    bool tieneArea = false;
                    using (FileStream fs = new FileStream("supervisores.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string linea = "";
                        string[] vl = new string[0];
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            vl = linea.Split(';');
                            if (sup.DNI == vl[0] && vl[5] != "NULL")
                            {
                                tieneArea = true;
                            }
                            linea = sr.ReadLine();
                        }
                    }

                    if (tieneArea)
                    {
                        MessageBox.Show("El supervisor tiene un area a cargo, elimine primero el area o cambie el supervisor del area");
                    }
                    else
                    {
                        sup.baja();
                        cargasupervisores();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un supervisor");
            }
        }
    }
}
