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

namespace SistemaDePersonal_GAN.Formularios
{
    public partial class FRMListaAreas : Form
    {
        public FRMListaAreas()
        {
            InitializeComponent();
            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = "";
                string[] vl = new string[0];

                sr.ReadLine();
                linea = sr.ReadLine();
                while (linea != null)
                {
                    vl = linea.Split(';');
                    if (vl[2] == "2")
                    {
                        string linea2 = "";
                        string[] vl2 = new string[0];
                        string nombre = "";
                        using (FileStream fs2 = new FileStream("supervisores.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr2 = new StreamReader(fs2))
                        {
                            sr2.ReadLine();
                            linea2 = sr2.ReadLine();
                            while (linea2 != null)
                            {
                                vl2 = linea2.Split(';');

                                if (vl[0] == vl2[0])
                                {
                                    nombre = vl2[1] + " " + vl2[2];
                                }
                                
                                linea2 = sr2.ReadLine();
                            }
                        }
                        using (FileStream fs1 = new FileStream("areas.txt", FileMode.Open, FileAccess.Read))
                        using (StreamReader sr1 = new StreamReader(fs1))
                        {
                            string linea1 = "";
                            string[] vl1 = new string[0];
                            sr1.ReadLine();
                            linea1 = sr1.ReadLine();
                            while (linea1 != null)
                            {
                                vl1 = linea1.Split(';');
                                if (vl[0] == vl1[3])
                                {
                                    dataGridView1.Rows.Add(vl1[0], vl1[1], vl1[2], nombre);
                                }
                                linea1 = sr1.ReadLine();
                            }
                        }
                    }
                    linea = sr.ReadLine();
                }
            }
        }
    }
}
