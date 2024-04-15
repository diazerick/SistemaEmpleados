using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebEmpleados.Models;

namespace WebEmpleados.Datos
{
    public class D_Empleado
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<E_Empleado> ObtenerTodos()
        {
            //Creando una lista de empleados vacia
            List<E_Empleado> lista = new List<E_Empleado>();

            //Creamos la conexion 
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                //Abrimos la conexion
                conexion.Open();
                string query = "SELECT idEmpleado,nombre,sueldo,fechaNacimiento,tiempoCompleto FROM Empleados";
                //Creando objeto de la clase SqlCommand
                SqlCommand comando = new SqlCommand(query, conexion);
                //Ejecutar el query, creamos un objeto de la clase SqlDataReader para almacenar los resultados
                SqlDataReader reader = comando.ExecuteReader();
                //Recorremos el conjunto de resultados
                while (reader.Read())
                {
                    //Crear un objeto empleado
                    E_Empleado empleado = new E_Empleado();
                    //Asignarle un valor a sus propiedades, convertir al tipo de dato necesario
                    empleado.IdEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                    empleado.Nombre = reader["nombre"].ToString();//Convertimos a string
                    empleado.Sueldo = Convert.ToDecimal(reader["sueldo"]);
                    empleado.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                    empleado.TiempoCompleto = Convert.ToBoolean(reader["tiempoCompleto"]);
                    //Agregamos el empleado a la lista
                    lista.Add(empleado);
                }
                //Cerramos la conexion
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
            return lista;
        }

        public void Agregar(E_Empleado empleado)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                //Creando query con parametros
                string query = "INSERT INTO Empleados(nombre,sueldo,fechaNacimiento,tiempoCompleto) " +
                                        "VALUES(@nombre,@sueldo,@fechaNacimiento,@tiempoCompleto)";
                //Crear el objeto de la clase SqlCommand
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignando los valores a los parametros del query
                comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                comando.Parameters.AddWithValue("@fechaNacimiento", empleado.FechaNacimiento);
                comando.Parameters.AddWithValue("@tiempoCompleto", empleado.TiempoCompleto);
                //Ejecutar el query
                comando.ExecuteNonQuery();
                //Cerrar la conexion
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public E_Empleado ObtenerPorId(int idEmpleado)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            //Creando objeto E_Empleado
            E_Empleado empleado = new E_Empleado();
            try
            {
                conexion.Open();
                string query = "SELECT idEmpleado,nombre,sueldo,fechaNacimiento,tiempoCompleto " +
                                "FROM Empleados WHERE idEmpleado = @idEmpleado";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    //Comienza a leer.
                    //Asignarle un valor a sus propiedades, convertir al tipo de dato necesario
                    empleado.IdEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                    empleado.Nombre = reader["nombre"].ToString();//Convertimos a string
                    empleado.Sueldo = Convert.ToDecimal(reader["sueldo"]);
                    empleado.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                    empleado.TiempoCompleto = Convert.ToBoolean(reader["tiempoCompleto"]);
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }

            return empleado;
        }

        public void Editar(E_Empleado empleado)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();

                string query = "UPDATE Empleados SET nombre=@nombre, sueldo=@sueldo, " +
                                "fechaNacimiento=@fechaNacimiento, tiempoCompleto=@tiempoCompleto WHERE idEmpleado = @idEmpleado";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                comando.Parameters.AddWithValue("@fechaNacimiento", empleado.FechaNacimiento);
                comando.Parameters.AddWithValue("@tiempoCompleto", empleado.TiempoCompleto);
                comando.Parameters.AddWithValue("@idEmpleado", empleado.IdEmpleado);

                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }
        }
    }
}