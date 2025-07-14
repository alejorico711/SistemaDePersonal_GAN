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
    public partial class FRMCambioSupervisor : Form
    {
        List<ClsArea> lstareas = new List<ClsArea>();
        List<ClsSupervisor> lstsupervisores = new List<ClsSupervisor>();
        List<ClsSupervisor> lstsupervisores2 = new List<ClsSupervisor>();
        public FRMCambioSupervisor()
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
            lstsupervisores2 = lstsupervisores.Where(sup => sup.Area == "NULL").ToList();
            cmbSupervisores.DataSource = lstsupervisores2;
            cmbSupervisores.SelectedIndex = -1;
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

        private void btnCambio_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAreas.SelectedRows.Count == 1 && cmbSupervisores.SelectedIndex != -1)
                {
                    string IDAnteriorSup = lstareas.ElementAt(dgvAreas.SelectedRows[0].Index).IDSupervisor;
                    lstareas.ElementAt(dgvAreas.SelectedRows[0].Index).IDSupervisor = lstsupervisores2.ElementAt(cmbSupervisores.SelectedIndex).DNI;
                    lstsupervisores2.ElementAt(cmbSupervisores.SelectedIndex).Area = lstareas.ElementAt(dgvAreas.SelectedRows[0].Index).ID;

                    using (StreamWriter sw = new StreamWriter("areas.txt", false))
                    {
                        sw.Write("ID;NOMBRE;DESCRIPCION;IDSUPERVISOR");
                    }
                    foreach (var area in lstareas)
                    {
                        using (StreamWriter sw = new StreamWriter("areas.txt", true))
                        {
                            sw.Write(Environment.NewLine + $"{area.ID};{area.Nombre};{area.Descripcion};{area.IDSupervisor}");
                        }
                    }

                    using (StreamWriter sw = new StreamWriter("supervisores.txt", false))
                    {
                        sw.Write("DNI;Nombre;Apellido;Email;FechaIngreso;Area");
                    }
                    foreach (var sup in lstsupervisores)
                    {
                        if (sup.DNI == IDAnteriorSup)
                        {
                            using (StreamWriter sw = new StreamWriter("supervisores.txt", true))
                            {
                                sw.Write(Environment.NewLine + $"{sup.DNI};{sup.Nombre};{sup.Apellido};{sup.Email};{sup.FechaIngreso};NULL");
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter("supervisores.txt", true))
                            {
                                sw.Write(Environment.NewLine + $"{sup.DNI};{sup.Nombre};{sup.Apellido};{sup.Email};{sup.FechaIngreso};{sup.Area}");
                            }
                        }
                    }
                    MessageBox.Show("Supervisor cambiado con exito");
                }
                else
                {
                    MessageBox.Show("Seleccione un area para cambiar su supervisor");
                }
                cargaAreas();
                cargaSupervisores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
