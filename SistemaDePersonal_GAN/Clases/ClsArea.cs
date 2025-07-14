using SistemaDePersonal_GAN.Clases;
using SistemaDePersonal_GAN.Formularios.SUPERVISORES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDePersonal_GAN.Formularios
{
    public class ClsArea : IABM
    {//IDArea;NombreArea;Descripcion;IDSupervisor

		private string _id;

		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		private string _descripcion;

		public string Descripcion
		{
			get { return _descripcion; }
			set { _descripcion = value; }
		}

		private string _idSpuervisor;

		public string IDSupervisor
		{
			get { return _idSpuervisor; }
			set { _idSpuervisor = value; }
		}

        public ClsArea()
        {
            
        }

		public ClsArea(string id,string n, string d, string idSup)
        {
            ID = id;
			Nombre = n;
			Descripcion = d;
			IDSupervisor = idSup;
        }

        public override string ToString()
        {
			return Nombre;
        }
        public void baja()
        {
            using (FileStream fs = new FileStream("areas.txt", FileMode.Open, FileAccess.Read))
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

                    if (vl[0] == ID)
                    {
                        //no lo escribe
                    }
                    else
                    {
                        sw.Write(Environment.NewLine + vl[0] + ';' + vl[1] + ';' + vl[2]);
                    }
                    linea = sr.ReadLine();
                }
            }
            File.Delete("areas.txt");
            File.Move("aux1.txt", "areas.txt");
            MessageBox.Show("Area dada de baja con exito");
        }

        public void alta()
        {
            using (StreamWriter sw = new StreamWriter("areas.txt", true))
            {
                sw.Write(Environment.NewLine + $"{ID};{Nombre};{Descripcion};{IDSupervisor}");
            }
            
        }
    }
}
