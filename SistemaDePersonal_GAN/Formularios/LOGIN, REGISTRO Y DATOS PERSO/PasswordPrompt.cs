using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Clases
{
    public partial class PasswordPrompt : Form
    {
        public string EnteredPassword { get; private set; }

        public PasswordPrompt()
        {

            // Crear controles (sin diseñador)
            Label lbl = new Label() { Text = "Ingrese su contraseña para confirmar la operacion:", Top = 20, Left = 20, Width = 200 };
            TextBox txt = new TextBox() { Top = 50, Left = 20, Width = 200, UseSystemPasswordChar = true };
            Button btnOk = new Button() { Text = "Aceptar", Top = 80, Left = 20, DialogResult = DialogResult.OK };
            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            this.Controls.Add(btnOk);
            this.AcceptButton = btnOk;

            btnOk.Click += (s, e) =>
            {
                EnteredPassword = txt.Text;
                this.Close();
            };

            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 270;
            this.Height = 160;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
        }
    }
}
