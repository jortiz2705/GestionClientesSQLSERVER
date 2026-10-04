using GestionClientesSQLSERVER.Data;
using GestionClientesSQLSERVER.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionClientesSQLSERVER.Repositories
{
    public class ClienteRepositorySp : IClienteRepository
    {
        private readonly Conexion _conexion;
        public ClienteRepositorySp()
        {
            _conexion = new Conexion();
        }
        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            string sql = "sp_ListarClientes"; // Llamada al procedimiento almacenado
            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado
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
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
            return clientes;
        }
        public int RegistrarCliente(Cliente cliente)
        {
            string sql = "sp_RegistrarCliente"; // Llamada al procedimiento almacenado
            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.Ciudad;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = (object?)cliente.Email ?? DBNull.Value;
                        var paramCredito = cmd.Parameters.Add("@credito", SqlDbType.Decimal);
                        paramCredito.Precision = 18;
                        paramCredito.Scale = 2;
                        paramCredito.Value = cliente.Credito;
                        cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = cliente.Estado;
                        // 2. AGREGAR EL PARÁMETRO DE SALIDA EN C#
                        SqlParameter paramId = new SqlParameter("@id_cliente", SqlDbType.Int);
                        paramId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramId);

                        int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecutamos el procedimiento

                        if (filasAfectadas > 0 && paramId.Value != DBNull.Value)
                        {
                            return Convert.ToInt32(paramId.Value); // Retorna el ID del cliente insertado
                        }
                        return 0; // Retorna 0 si no se insertó ningún cliente
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de Base de Datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
        }
        public bool ActualizarCliente(Cliente cliente)
        {
            string sql = "sp_ActualizarCliente"; // Llamada al procedimiento almacenado
            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado

                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = cliente.IdCliente;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = cliente.Nombre;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar, 50).Value = cliente.Ciudad;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = (object?)cliente.Email ?? DBNull.Value;
                        var paramCredito = cmd.Parameters.Add("@credito", SqlDbType.Decimal);
                        paramCredito.Precision = 18;
                        paramCredito.Scale = 2;
                        paramCredito.Value = cliente.Credito;
                        cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = cliente.Estado;
                        int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecutamos el procedimiento

                        return filasAfectadas > 0; // Retorna true si se actualizó algún cliente
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
        }
        public bool EliminarCliente(int idCliente)
        {
            string sql = "sp_EliminarCliente"; // Llamada al procedimiento almacenado
            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
                        int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecutamos el procedimiento
                        return filasAfectadas > 0; // Retorna true si se eliminó algún cliente
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al eliminar el cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
        }
        public Cliente? ObtenerClientePorId(int idCliente)
        {
            string sql = "sp_ObtenerClientePorId"; // Llamada al procedimiento almacenado
            try
            {
                using (SqlConnection cn = _conexion.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado
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
                throw new Exception("Error al obtener el cliente", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado", ex);
            }
            return null;
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
