using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePersonal_GAN.Formularios.ADMINISTRADORES
{
    public static class DelegadosGlobales
    {
        public delegate void BienvenidaDelegate(string nombre);
        public static BienvenidaDelegate Bienvenida;
    }
}
