using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //Esta libreria es para usar bases de datos
using System.Data.SqlClient; //Esta libreria permite  usar  SQL Server
namespace miPrimeaAplicacion
{
    internal class conexion1
    {
        //definir los miembro, atrivutos y metodos de la clase
        public SqlConnection objConexion = new SqlConnection();//conectarme ala BD
        public SqlCommand objComando = new SqlCommand();//ejecutar consultas (insert, update, delete, select) sql en la BD
        public SqlDataAdapter objAdaptador = new SqlDataAdapter();// un puente entre la BD y la aplicacion
        DataSet objDs = new DataSet(); // representa una copia de la aquitectura de la BD

        public conexion1() // contrutor e inicializacion de los miembros de la clase 
        {
            string cadenaConexion = "Data Source=DESKTOP-0J2K7A9;Initial Catalog=BD_Alumnos;Integrated Security=True";
            objConexion.ConnectionString = cadenaConexion;
            objConexion.Open(); // abrir la conexion a la BD
        }

        public DataSet obtenerDatos()
        {
            objDs.Clear(); // limpiar el dataset
            objComando.Connection = objConexion; // asignar la conexion para ejecutar la consulta a la BD

            objAdaptador.SelectCommand = objComando;
            objComando.CommandText = "select * from Alumnos"; 
            objAdaptador.Fill(objDs, "Alumnos"); // tomamos los datos de la BD y llenamos el ds

            return objDs; 

        }
    }

}
