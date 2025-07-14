using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public class ClsAdministrador : ClsPersona
    {

        public ClsAdministrador() : base()
        {

        }

        public ClsAdministrador(string d, string n, string a, string e, string f) : base(d, n, a, e, f)
        {

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
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "1");
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

            using (StreamWriter sw = new StreamWriter("administradores.txt", true))
            {
                sw.Write(Environment.NewLine + $"{DNI};{Nombre};{Apellido};{Email};{FechaIngreso}");
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
            MessageBox.Show("Usuario dado de baja con exito");

            using (FileStream fs = new FileStream("administradores.txt", FileMode.Open, FileAccess.Read))
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
                        using (StreamWriter sw1 = new StreamWriter("empleados.txt",true))
                        {
                            sw1.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4] + ';' + "NULL");
                        }
                    }
                    else
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4]);
                    }
                    linea = sr.ReadLine();
                }
            }

            File.Delete("administradores.txt");
            File.Move("aux1.txt", "administradores.txt");
        }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
