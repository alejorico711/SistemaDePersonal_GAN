using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
