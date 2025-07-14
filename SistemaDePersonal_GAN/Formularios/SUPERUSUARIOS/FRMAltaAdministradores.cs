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
    public partial class FRMAltaAdministradores : Form
    {
        List<ClsAdministrador> lstadminis = new List<ClsAdministrador>();
        public FRMAltaAdministradores()
        {
            InitializeComponent();

            dgvAdmins.ReadOnly = true;
            dgvAdmins.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAdmins.MultiSelect = false;

            cargadata();
        }

        private void cargadata()
        {
            lstadminis.Clear();
            dgvAdmins.DataSource = null;
            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read)) //crea lista de usuarios no dados de alta
            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = "";
                string[] vl = new string[0];

                sr.ReadLine();
                linea = sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    if (vl[2] == "0")
                    {
                        using (FileStream fs1 = new FileStream("empleados.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr1 = new StreamReader(fs1))
                        {
                            string linea1 = "";
                            string[] vl1 = new string[0];
                            while (linea1 != null)
                            {
                                vl1 = linea1.Split(';');
                                if (vl[0] == vl1[0])
                                {
                                    ClsAdministrador admin = new ClsAdministrador(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                                    //ClsPersona persona = new ClsPersona(vl1[0], vl1[1], vl1[2], vl1[3], vl1[4]);
                                    lstadminis.Add(admin);
                                }
                                linea1 = sr1.ReadLine();
                            }
                        }
                    }
                    linea = sr.ReadLine();
                }
            }
            dgvAdmins.DataSource = lstadminis;
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAdmins.SelectedRows.Count == 1)
                {
                    ClsAdministrador admin = lstadminis[dgvAdmins.SelectedRows[0].Index];
                    admin.alta();
                    using (FileStream fs = new FileStream("empleados.txt", FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    using (FileStream fs1 = new FileStream("aux1.txt", FileMode.OpenOrCreate, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(fs1))
                    {
                        string linea = "";
                        string[] vl = new string[0];
                        linea = sr.ReadLine();
                        sw.Write(linea);
                        linea = sr.ReadLine();
                        while (linea != null)
                        {
                            vl = linea.Split(';');

                            if (vl[0] == admin.DNI)
                            {
                                //no lo escribe
                            }
                            else
                            {
                                sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4] + ';' + vl[5]);
                            }
                            linea = sr.ReadLine();
                        }
                    }
                    File.Delete("empleados.txt");
                    File.Move("aux1.txt", "empleados.txt");


                }
                else
                {
                    MessageBox.Show("Complete todos los campos para dar de alta un empleado");
                }
                cargadata();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
