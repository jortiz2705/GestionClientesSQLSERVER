using GestionClientesSQLSERVER.Data;
using GestionClientesSQLSERVER.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestionClientesSQLSERVER.Repositories
{
    internal class ClienteRepositorySp : IClienteRepository
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
        public bool RegistrarCliente(Cliente cliente)
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
                        cmd.Parameters.Add("@email", SqlDbType.VarChar, 100).Value = cliente.Email;
                        var paramCredito = cmd.Parameters.Add("@credito", SqlDbType.Decimal);
                        paramCredito.Precision = 18;
                        paramCredito.Scale = 2;
                        paramCredito.Value = cliente.Credito;
                        cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = cliente.Estado;
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0; // Retorna true si se insertó al menos una fila
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
                        cmd.Parameters.AddWithValue("@id_cliente", cliente.IdCliente);
                        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@ciudad", cliente.Ciudad);
                        cmd.Parameters.AddWithValue("@email", (object?)cliente.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@credito", cliente.Credito);
                        cmd.Parameters.AddWithValue("@estado", cliente.Estado);
                        cmd.ExecuteNonQuery();
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
            return true;
        }
        public bool EliminarCliente(int idCliente)
        {
            return true;
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
                        cmd.Parameters.AddWithValue("@id_cliente", idCliente);
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
                throw new Exception("Error de Base de Datos", ex);
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
