using SistemaDePersonal_GAN.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDePersonal_GAN.Formularios.SUPERVISORES
{
    public class ClsTarea
    {
		//IDTarea;DescripcionTarea;IDArea;DNIEmpleado;FechaAsignacion;Estado
		private string _idTarea;

		public string IDTarea
		{
			get { return _idTarea; }
			set { _idTarea = value; }
		}
		private string _descripcion;

		public string Descripcion
		{
			get { return _descripcion; }
			set { _descripcion = value; }
		}
		private string _idArea;

		public string IDArea
		{
			get { return _idArea; }
			set { _idArea = value; }
		}
		private string _dniEmpleado;

		public string DNIEmpleado
		{
			get { return _dniEmpleado; }
			set { _dniEmpleado = value; }
		}
		private DateTime _fechaAsignacion;

		public DateTime FechaAsignacion
		{
			get { return _fechaAsignacion; }
			set { _fechaAsignacion = value; }
		}
		private string _estado;

		public string Estado
		{
			get { return _estado; }
			set { _estado = value; }
		}
		public ClsTarea(string idtarea, string d, string idarea, string dniemp, DateTime fecha, string e)
        {
            IDTarea = idtarea;
			Descripcion = d;
			IDArea = idarea;
			DNIEmpleado = dniemp;
			FechaAsignacion = fecha;
			Estado = e;
        }
    }
}
