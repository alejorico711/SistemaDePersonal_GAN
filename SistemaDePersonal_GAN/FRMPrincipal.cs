using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN
{
    public partial class FRMPrincipal : Form
    {
        public FRMPrincipal()
        {
            InitializeComponent();
            panelTop.MouseDown += new MouseEventHandler(panelTop_MouseDown);
            panelTop.MouseMove += new MouseEventHandler(panelTop_MouseMove);
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);
        }

        private Form currentForm = null;

        private void AbrirFormularioHijo(Form childForm)
        {
            if (currentForm != null)
            {
                currentForm.Close();
            }
            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelHijos.Controls.Add(childForm);
            panelHijos.Tag = childForm;
            pictureBox2.Hide();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FRMAsignacion());
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
    }
}
