using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace miPrimeaAplicacion
{
    internal class Conexion
    {
        public SqlConnection objConexion = new SqlConnection();
        public SqlCommand objComando = new SqlCommand();
        public SqlDataAdapter objDataAdapter = new SqlDataAdapter();
        DataSet objDs = new DataSet();

        public Conexion()
        {
            String cadenaCoenxion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True";
            objConexion.ConnectionString = cadenaCoenxion;
            objConexion.Open();
        }

        public DataSet obtenerDatos()
        {
            objDs.Clear();
            objComando.Connection = objConexion;

            objDataAdapter.SelectCommand = objComando;
            objComando.CommandText = "SELECT * FROM alumnos";
            objDataAdapter.Fill(objDs, "alumnos");

            return objDs;
        }

        public string administrarDatosAlumnos(string[] datos, string accion)
        {
            try
            {
                objComando.Connection = objConexion;

                if (accion == "nuevo")
                {
                    objComando.CommandText = "INSERT INTO alumnos (codigo, nombre, direccion, telefono, email) " +
                                             "VALUES ('" + datos[1] + "', '" + datos[2] + "', '" + datos[3] + "', '" + datos[4] + "', '" + datos[5] + "')";
                }
                else if (accion == "modificar")
                {
                    objComando.CommandText = "UPDATE alumnos SET codigo='" + datos[1] + "', nombre='" + datos[2] + "', " +
                                             "direccion='" + datos[3] + "', telefono='" + datos[4] + "', email='" + datos[5] + "' " +
                                             "WHERE idAlumno='" + datos[0] + "'";
                }
                else if (accion == "eliminar")
                {
                    objComando.CommandText = "DELETE FROM alumnos WHERE idAlumno='" + datos[0] + "'";
                }

                objComando.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}