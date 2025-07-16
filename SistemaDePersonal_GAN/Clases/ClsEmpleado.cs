using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Formularios
{
    public class ClsEmpleado : ClsPersona
    {//DNI;Nombre;Apellido;Email;FechaIngreso
        private string _area;

		public string Area
		{
			get { return _area; }
			set { _area = value; }
		}

        public ClsEmpleado() : base()
        {
            
        }

        public ClsEmpleado(string d, string n, string a, string e, string f, string area) : base(d,n, a, e, f)
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
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + "3");
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

                    if (vl[0] == DNI)
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2] + ';' + vl[3] + ';' + vl[4] + ';' + "NULL");
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
            MessageBox.Show("Usuario dado de baja con exito");
        }
    }
}
