using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePersonal_GAN.Formularios
{
    public class ClsArea
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
    }
}
