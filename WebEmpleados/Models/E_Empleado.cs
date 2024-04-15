using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace WebEmpleados.Models
{
    public class E_Empleado
    {
        //Propiedades simples
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public decimal Sueldo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool TiempoCompleto { get; set; }

        //Propiedades de solo lectura
        public string TiempoCompletoDescripcion
        {
            get
            {
                if (TiempoCompleto == true)
                    return "Si";
                else
                    return "No";
            }
        }

        public int Edad
        {
            get
            {
                //Obtenemos la fecha actual
                DateTime fechaActual = DateTime.Now;

                return fechaActual.Year - FechaNacimiento.Year;
            }

        }
    }
}