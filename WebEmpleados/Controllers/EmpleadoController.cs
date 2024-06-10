using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEmpleados.Datos;
using WebEmpleados.Models;

namespace WebEmpleados.Controllers
{
    public class EmpleadoController : Controller
    {
        //Esto es un comentario aaaaaaaaaaaaa
        public ActionResult Index()
        {
            //Creamos la lista vacia
            List<E_Empleado> lista = new List<E_Empleado>();
            try
            {
                //Creamos un objeto de la capa de datos
                D_Empleado datos = new D_Empleado();
                //Obtener la lista de Empleados
                lista = datos.ObtenerTodos();
            }
            catch (SqlException ex)
            {
                TempData["error"] = $"Error en la base de datos {ex.Message}";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            //Pasamos el modelo(lista de empleados) a la vista
            return View("Consulta", lista);
        }

        public ActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public ActionResult Agregar(E_Empleado objEmpleado)
        {
            try
            {
                D_Empleado datos = new D_Empleado();
                datos.Agregar(objEmpleado);
                TempData["mensaje"] = $"El empleado {objEmpleado.Nombre} fue registrado";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult ObtenerParaEditar(int idEmpleado)
        {
            D_Empleado datos = new D_Empleado();
            E_Empleado empleado = datos.ObtenerPorId(idEmpleado);
            return View("VistaEditar", empleado);
        }

        public ActionResult Editar(E_Empleado empleado)
        {
            D_Empleado datos = new D_Empleado();
            datos.Editar(empleado);

            TempData["mensaje"] = $"El empleado {empleado.Nombre} se edito";
            return RedirectToAction("Index");
        }

    }
}