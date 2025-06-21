using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePersonal_GAN.Formularios
{
    public class ClsUsuario
    {
		private string _dni;

		public string DNI
		{
			get { return _dni; }
			set { _dni = value; }
		}

		private string _nivel;

		public string Nivel
		{
			get { return _nivel; }
			set { _nivel = value; }
		}
        public ClsUsuario()
        {
			
        }

        public ClsUsuario(string d, string n)
        {
            DNI= d;
			Nivel= n;
        }
    }
}
