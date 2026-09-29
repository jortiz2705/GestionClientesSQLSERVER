using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace GestionClientesSQLSERVER.Data
{
    public class Conexion
    {
        private readonly string _cadenaConexion =
            "Server=INFORMATICA;Database=GestionClientes;Integrated Security=True;TrustServerCertificate=true;";
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
