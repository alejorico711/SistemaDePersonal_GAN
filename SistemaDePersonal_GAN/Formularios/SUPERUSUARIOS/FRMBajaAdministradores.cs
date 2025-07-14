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
    public partial class FRMBajaAdministradores : Form
    {
        List<ClsAdministrador> lstadmins = new List<ClsAdministrador>();
        public FRMBajaAdministradores()
        {
            InitializeComponent();

            dgvAdmins.ReadOnly = true;
            dgvAdmins.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdmins.MultiSelect = false;

            cargaadmins();
            dgvAdmins.DataSource = lstadmins;
        }

        private void cargaadmins()
        {
            using (FileStream fs1 = new FileStream("administradores.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr1 = new StreamReader(fs1))
            {
                string linea1 = "";
                string[] vl1 = new string[0];
                sr1.ReadLine();
                linea1 = sr1.ReadLine();
                while (linea1 != null)
                {
                    vl1 = linea1.Split(';');

                    ClsAdministrador admin = new ClsAdministrador(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                    lstadmins.Add(admin);

                    linea1 = sr1.ReadLine();
                }
            }

        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                ClsAdministrador admin = lstadmins[dgvAdmins.SelectedRows[0].Index];
                admin.baja();
                cargaadmins();
                dgvAdmins.DataSource = lstadmins;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
