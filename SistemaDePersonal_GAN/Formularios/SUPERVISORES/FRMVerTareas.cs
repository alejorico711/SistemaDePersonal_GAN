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
using static System.Windows.Forms.MonthCalendar;

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public partial class FRMVerTareas : Form
    {
        List<ClsTarea> lstTareas = new List<ClsTarea>();
        ClsSupervisor misupervisor;
        public FRMVerTareas(ClsSupervisor supervisor)
        {
            InitializeComponent();

            dgvTareas.ReadOnly = true;
            dgvTareas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTareas.MultiSelect = false;

            misupervisor = supervisor;

            cargatareas();
        }

        private void cargatareas()
        {
            lstTareas = File.ReadLines("tareas.txt")
                .Skip(1)
                .Where(linea => linea.Split(';')[2] == misupervisor.Area)
                .Select(linea => creaTarea(linea))
                .ToList();
            dgvTareas.DataSource = lstTareas;
        }

        private ClsTarea creaTarea(string linea)
        {
            var datos = linea.Split(';');
            ClsTarea tarea = new ClsTarea(datos[0], datos[1], datos[2], datos[3], Convert.ToDateTime(datos[4]), datos[5]);
            return tarea;
        }

        private void btnFinalizarTarea_Click(object sender, EventArgs e)
        {
            if (dgvTareas.SelectedRows[0].Index != -1)
            {
                if (dgvTareas.SelectedRows[0].Cells[5].Value.ToString() != "FINALIZADA")
                {
                    lstTareas[dgvTareas.SelectedRows[0].Index].Estado = "FINALIZADA";
                    dgvTareas.DataSource = null;
                    dgvTareas.DataSource = lstTareas;

                    using (StreamWriter sw = new StreamWriter("tareas.txt", false))
                    {
                        sw.Write("IDTarea;DescripcionTarea;IDArea;DNIEmpleado;FechaAsignacion;Estado");
                    }
                    foreach (var tarea in lstTareas)
                    {
                        using (StreamWriter sw = new StreamWriter("tareas.txt", true))
                        {
                            sw.Write(Environment.NewLine + $"{tarea.IDTarea};{tarea.Descripcion};{tarea.IDArea};{tarea.DNIEmpleado};{tarea.FechaAsignacion.ToString("dd-MM-yyyy")};{tarea.Estado}");
                        }
                    }
                    cargatareas();
                    MessageBox.Show("Tarea finalizada con exito");
                }
                else
                {
                    MessageBox.Show("La tarea ya esta finalizada");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una tarea para finalizar");
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            List<ClsTarea> coincidencias = lstTareas
                    .Where(tarea => tarea.DNIEmpleado.ToLower().StartsWith(txtBusqueda.Text))
                    .ToList();
            dgvTareas.DataSource = coincidencias;
        }
    }
}
