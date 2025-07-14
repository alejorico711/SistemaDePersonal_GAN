using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public class ClsSupervisor : ClsPersona
    {
        //DNI;Nombre;Apellido;Email;FechaIngreso
        private string _area;

        public string Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public ClsSupervisor() : base()
        {

        }

        public ClsSupervisor(string d, string n, string a, string e, string f, string area) : base(d, n, a, e, f)
        {
            Area = area;
        }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }

        public override void alta()
        {
            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
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

                    if (vl[0] == DNI)
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "2");
                    }
                    else
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2]);
                    }
                    linea = sr.ReadLine();
                }
            }
            
            File.Delete("usuarios.txt");
            File.Move("aux1.txt", "usuarios.txt");
            MessageBox.Show("Usuario dado de alta con exito");

            using (StreamWriter sw = new StreamWriter("supervisores.txt", true))
            {
                sw.Write(Environment.NewLine +  $"{DNI};{Nombre};{Apellido};{Email};{FechaIngreso};NULL");
            }

            
        }

        public override void baja()
        {
            using (FileStream fs = new FileStream("usuarios.txt", FileMode.Open, FileAccess.Read))
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

                    if (vl[0] == DNI)
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "0");
                    }
                    else
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2]);
                    }
                    linea = sr.ReadLine();
                }
            }

            File.Delete("usuarios.txt");
            File.Move("aux1.txt", "usuarios.txt");
            using (FileStream fs = new FileStream("supervisores.txt", FileMode.Open, FileAccess.Read))
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

                    if (vl[0] == DNI)
                    {
                        using (StreamWriter sw1 = new StreamWriter("empleados.txt", true))
                        {
                            sw1.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4] + ';' + vl[5]);
                        }
                    }
                    else
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4] + ';' + vl[5]);
                    }
                    linea = sr.ReadLine();
                }
            }

            File.Delete("supervisores.txt");
            File.Move("aux1.txt", "supervisores.txt");
            MessageBox.Show("Usuario dado de baja con exito");
        }
    }
}
