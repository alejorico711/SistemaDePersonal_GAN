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

namespace SistemaDePersonal_GAN.Formularios.EMPLEADOS
{
    public partial class FRMTareasEmpleado : Form
    {
        List<ClsTarea> lstTareas = new List<ClsTarea>();
        ClsEmpleado miempleado;
        public FRMTareasEmpleado(ClsEmpleado empleado)
        {
            InitializeComponent();
            miempleado = empleado;
            cargatareas();
        }

        private void cargatareas()
        {
            lstTareas = File.ReadLines("tareas.txt")
                .Skip(1)
                .Where(linea => linea.Split(';')[3] == miempleado.DNI)
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
    }
}
