using System;
using System.Collections.Generic;
using System.Text;
using GestionClientesSQLSERVER.Models;

namespace GestionClientesSQLSERVER.Services
{
    public interface IClienteService
    {
        List<Cliente> ListarClientes();
        bool RegistrarCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool EliminarCliente(int idCliente);
        Cliente? ObtenerClientePorId(int idCliente);

        string ProbarDependencia();
    }
}
