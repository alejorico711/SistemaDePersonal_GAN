using SistemaDePersonal_GAN.Clases;
using SistemaDePersonal_GAN.Formularios.SUPERUSUARIOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Formularios.ADMINISTRADORES
{
    public partial class FRMSuperusuario : Form
    {
        private string clave;
        public FRMSuperusuario(string contraseña)
        {
            InitializeComponent();
            panelTop.MouseDown += new MouseEventHandler(panelTop_MouseDown);
            panelTop.MouseMove += new MouseEventHandler(panelTop_MouseMove);
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);
            clave = contraseña;
        }

        private Form formularioActual = null;

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if (formularioActual == formularioHijo)
            {

            }
            else if (formularioActual != formularioHijo)
            {
                if (formularioActual != null)
                {
                    formularioActual.Close();
                }
                formularioActual = formularioHijo;
                formularioHijo.TopLevel = false;
                formularioHijo.FormBorderStyle = FormBorderStyle.None;
                formularioHijo.Dock = DockStyle.Fill;
                panelHijos.Controls.Add(formularioHijo);
                panelHijos.Tag = formularioHijo;
                pictureBox2.Hide();
                formularioHijo.Show();
            }



            /*if (formularioActual != null)
            {
                formularioActual.Close();
            }
            formularioActual = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            panelHijos.Controls.Add(formularioHijo);
            panelHijos.Tag = formularioHijo;
            pictureBox2.Hide();
            formularioHijo.Show();*/
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void btnAltaEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMAltaEmpleados());
        }

        private void btnListaEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMListaEmpleados());
        }

        private void btnListaSupervisores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMListaSupervisores());
        }

        private void btnListaAreas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMListaAreas());
        }

        private void btnBajaEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBajaEmpledos());
        }

        private void btnAltaAreas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMAltaAreas());
        }

        private void btnBajaAreas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBajaAreas());
        }

        private void btnAltaSupervisores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMAltaSupervisores());
        }

        private void btnBajaSupervisores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBajaSupervisores());
        }

        private void btnNuevoSuperuser_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMNuevoSuperuser(clave));
        }

        private void btnListaAdministradores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMListaAdministradores());
        }

        private void btnAltaAdministradores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMAltaAdministradores());
        }

        private void btnBajaAdministradores_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMBajaAdministradores());
        }

        private void btnCambio_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMCambioSupervisor());
        }

        private void bntLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show();
        }
    }
}
