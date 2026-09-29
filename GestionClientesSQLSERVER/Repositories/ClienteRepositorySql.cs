using GestionClientesSQLSERVER.Data;
using GestionClientesSQLSERVER.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.WebSockets; // Obligatorio para SQL Server

namespace GestionClientesSQLSERVER.Repositories
{
    public class ClienteRepositorySql : IClienteRepository
    {
        private readonly Conexion _conexion;
        public ClienteRepositorySql()
        {
            _conexion = new Conexion();
        }
        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            string sql = "SELECT id_cliente, nombre, ciudad, email, credito, estado FROM Clientes";

            try
            {   // El bloque 'using' asegura que la conexión se cierre correctamente incluso si ocurre un error
                // Usamos la inyección o método de la clase Conexion
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open(); //abrir la conexión

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                clientes.Add(MapearCliente(reader));
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {   // Captura errores específicos de SQL (ej. error de sintaxis, tabla no existe, caída de servidor)
                throw new Exception("Error de Base de Datos", ex);
            }
            catch (Exception ex)
            {   // Captura cualquier otro tipo de error inesperado (ej. error al mapear el cliente)
                throw new Exception("Error inesperado", ex);
            }
            return clientes;
        }

        public bool RegistrarCliente(Cliente cliente)
        {
            string sql = @"
                INSERT INTO Clientes (nombre, ciudad, email, credito, estado) 
                VALUES (@nombre, @ciudad, @email, @credito, @estado)";

            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.Ciudad;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = cliente.Email;
                        var paramCredito = cmd.Parameters.Add("@credito", SqlDbType.Decimal);
                        paramCredito.Precision = 18;
                        paramCredito.Scale = 2;
                        paramCredito.Value = cliente.Credito;
                        cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = cliente.Estado;
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos al registrar cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al registrar cliente", ex);
            }
        }
        public bool ActualizarCliente(Cliente cliente)
        {
            string sql = @"
                UPDATE Clientes 
                SET nombre = @nombre, ciudad = @ciudad, email = @email, credito = @credito, estado = @estado 
                WHERE id_cliente = @id_cliente";
            try 
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = cliente.IdCliente;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.Ciudad;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = cliente.Email;
                        var paramCredito = cmd.Parameters.Add("@credito", SqlDbType.Decimal);
                        paramCredito.Precision = 18;
                        paramCredito.Scale = 2;
                        paramCredito.Value = cliente.Credito;
                        cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = cliente.Estado;
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos al actualizar cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al actualizar cliente", ex);
            }
        }
        public bool EliminarCliente(int idCliente)
        {
            string sql = "DELETE FROM Clientes WHERE id_cliente = @id_cliente";

            try 
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos al eliminar cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al eliminar cliente", ex);
            }
        }
        public Cliente? ObtenerClientePorId(int idCliente)
        {
            string sql = "SELECT id_cliente, nombre, ciudad, email, credito, estado FROM Clientes WHERE id_cliente = @id_cliente";
            try 
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearCliente(reader);
                            }
                        }
                    }
                }
                
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos al obtener cliente por ID", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener cliente por ID", ex);
            }
            return null; // Retorna null si no se encuentra el cliente
        } 
        private Cliente MapearCliente(SqlDataReader reader)
        {
            int idxIdCliente = reader.GetOrdinal("id_cliente");  
            int idxNombre = reader.GetOrdinal("nombre");
            int idxCiudad = reader.GetOrdinal("ciudad");
            int idxEmail = reader.GetOrdinal("email");
            int idxCredito = reader.GetOrdinal("credito");
            int idxEstado = reader.GetOrdinal("estado");

            return new Cliente(
                reader.GetInt32(idxIdCliente),
                reader.GetString(idxNombre),
                reader.GetString(idxCiudad),
                reader.IsDBNull(idxEmail) ? null : reader.GetString(idxEmail),
                reader.GetDecimal(idxCredito),
                reader.GetString(idxEstado)
            );
        }
    }
}