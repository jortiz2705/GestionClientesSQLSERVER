using GestionClientesSQLSERVER.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientesSQLSERVER.Repositories
{
    public interface IClienteRepository
    {
        List<Cliente> ListarClientes();
        bool RegistrarCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool EliminarCliente(int idCliente);
        Cliente? ObtenerClientePorId(int idCliente);
    }
}
