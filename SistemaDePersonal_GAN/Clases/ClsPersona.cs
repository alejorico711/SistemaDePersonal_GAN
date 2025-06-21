using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePersonal_GAN.Formularios
{
    public class ClsPersona
    {
        private string _dni;

        public string DNI
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _fechaIngrso;

        public string FechaIngreso
        {
            get { return _fechaIngrso; }
            set { _fechaIngrso = value; }
        }

        public ClsPersona()
        {
            
        }

        public ClsPersona(string d, string n, string a, string e, string f)
        {
            DNI = d;
            Nombre = n;
            Apellido = a;
            Email = e;
            FechaIngreso = f;
        }

        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
