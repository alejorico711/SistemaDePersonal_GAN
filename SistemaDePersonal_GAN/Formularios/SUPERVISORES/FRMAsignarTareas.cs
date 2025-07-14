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

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public partial class FRMAsignarTareas : Form
    {
        List<ClsEmpleado> lstEmpleados = new List<ClsEmpleado>();
        ClsSupervisor misupervisor;
        public FRMAsignarTareas(ClsSupervisor supervisor)
        {
            InitializeComponent();

            dgvEmpleados.ReadOnly= true;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.MultiSelect = false;

            misupervisor = supervisor;

            cargaempleados();
        }

        private void cargaempleados()
        {
            lstEmpleados = File.ReadLines("empleados.txt")
                .Skip(1)
                .Where(linea => linea.Split(';')[5] == misupervisor.Area)
                .Select(linea => creaEmpleado(linea))
                .ToList();
            dgvEmpleados.DataSource = lstEmpleados;
        }

        private ClsEmpleado creaEmpleado(string linea)
        {
            var datos = linea.Split(';');
            ClsEmpleado emp = new ClsEmpleado(datos[0], datos[1], datos[2], datos[3], datos[4], datos[5]);
            return emp;
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEmpleados.SelectedRows[0].Index != -1)
                {
                    if (txtDescripcion.TextLength > 5)
                    {
                        string idAUX = "";
                        using (FileStream fs = new FileStream("IDsTareas.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string linea = "";
                            linea = sr.ReadLine();
                            idAUX = linea;
                        }
                        using (FileStream fs = new FileStream("IDsTareas.txt", FileMode.Truncate, FileAccess.Write))
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine((Convert.ToInt32(idAUX) + 1).ToString());
                        }

                        ClsEmpleado emp = lstEmpleados.ElementAt(dgvEmpleados.SelectedRows[0].Index);

                        using (FileStream fs = new FileStream("tareas.txt", FileMode.Append, FileAccess.Write))
                        using (StreamWriter sw = new StreamWriter(fs))
                        {//IDTarea;DescripcionTarea;IDArea;DNIEmpleado;FechaAsignacion;Estado
                            sw.Write(Environment.NewLine + idAUX + ';' + txtDescripcion.Text + ';' + emp.Area + ';' + emp.DNI + ';' + DateTime.Today.ToString("dd-MM-yyyy") + ';' + "ASIGNADA");
                        }

                        MessageBox.Show("Tarea asignada con exito");
                        txtDescripcion.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una descripcion para la tarea");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un empleado para continuar con la operacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
