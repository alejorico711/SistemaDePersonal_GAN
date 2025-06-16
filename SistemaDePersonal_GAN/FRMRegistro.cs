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

namespace SistemaDePersonal_GAN
{
    public partial class FRMRegistro : Form
    {
        public FRMRegistro()
        {
            InitializeComponent();
            panelTop.MouseDown += new MouseEventHandler(panelTop_MouseDown);
            panelTop.MouseMove += new MouseEventHandler(panelTop_MouseMove);
            panelTop.MouseUp += new MouseEventHandler(panelTop_MouseUp);
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

        private void btnListo_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length > 0 && txtDNI.Text.Length > 0 && txtApellido.TextLength > 0 && txtEmail.TextLength > 0 && txtContraseña.Text.Length > 0)
            {
                if (txtContraseña.TextLength < 8)
                {
                    MessageBox.Show("Ingrese una contraseña con al menos 8 digitos");
                }else if (txtDNI.TextLength < 8)
                {
                    MessageBox.Show("Ingrese un DNI valido");
                }
                else
                {
                    bool existeDNI = false;
                    using (FileStream fs = new FileStream("datosPersonales.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string linea = "";
                        string[] vl = new string[0];
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            vl = linea.Split(';');
                            if (vl[0] == txtDNI.Text)
                            {
                                existeDNI = true;
                            }
                            linea = sr.ReadLine();
                        }
                    }
                    if (existeDNI)
                    {
                        MessageBox.Show("DNI ya registrado");
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("Complete todos los campos");
            }
        }

        private int panelCount = 0;
        private void btnAgregarFamiliar_Click(object sender, EventArgs e)
        {
            // Crear un nuevo panel
            Panel newPanel = new Panel
            {
                Size = new Size(236, 181),
                Location = new Point(0, panelCount * 181+5),

                BorderStyle = BorderStyle.FixedSingle
            };

            // Crear controles dentro del nuevo panel
            Label lblNombre = new Label { Text = "Nombre", Location = new Point(10, 13), AutoSize = true };
            TextBox txtNombre = new TextBox { Location = new Point(10, 28), Width = 100 };
            Label lblapellido = new Label { Text = "Apellido", Location = new Point(10, 49), AutoSize = true };
            TextBox txtApellido = new TextBox { Location = new Point(10, 64), Width = 100 };
            Label lblRelacion = new Label { Text = "Relacion", Location = new Point(10, 85), AutoSize = true };
            TextBox txtRelacion = new TextBox { Location = new Point(10, 100), Width = 100 };
            Label lblFN = new Label { Text = "Fecha de nacimiento", Location = new Point(10, 121), AutoSize = true };
            DateTimePicker dtpFecha = new DateTimePicker { Location = new Point(10, 136), Width = 200, Value = DateTime.Today };
            Button btnx = new Button { Text = "X", Location = new Point(190, 15), Size = new Size(24, 23) };

            // Agregar controles al nuevo panel
            newPanel.Controls.Add(lblNombre);
            newPanel.Controls.Add(txtNombre);
            newPanel.Controls.Add(lblapellido);
            newPanel.Controls.Add(txtApellido);
            newPanel.Controls.Add(lblRelacion);
            newPanel.Controls.Add(txtRelacion);
            newPanel.Controls.Add(lblFN);
            newPanel.Controls.Add(dtpFecha);
            newPanel.Controls.Add(btnx);
            btnx.Click += (s, ev) =>
            {
                panelFamiliares.Controls.Remove(newPanel);
            };

            // Agregar el nuevo panel al contenedor principal
            panelFamiliares.Controls.Add(newPanel);

            // Incrementar el contador de paneles
            panelCount++;
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
