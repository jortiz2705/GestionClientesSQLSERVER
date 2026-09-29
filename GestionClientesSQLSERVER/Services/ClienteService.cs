using GestionClientesSQLSERVER.Models;
using GestionClientesSQLSERVER.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientesSQLSERVER.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public string ProbarDependencia()
        {
            return _clienteRepository.GetType().Name;
        }
        public List<Cliente> ListarClientes()
        {
            return _clienteRepository.ListarClientes(); 
        }
        public  bool RegistrarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new ArgumentException("El nombre del cliente es un campo obligatorio.");
            }
            if (cliente.Credito < 0)
            {
                throw new ArgumentException("El crédito asignado no puede ser un valor negativo.");
            }
            if (!string.IsNullOrWhiteSpace(cliente.Email) && !cliente.Email.Contains("@"))
            {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }
            return _clienteRepository.RegistrarCliente(cliente);
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new ArgumentException("El nombre del cliente es un campo obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(cliente.Ciudad))
            {
                throw new ArgumentException("La Ciudad del cliente es un campo obligatorio.");
            }
            return _clienteRepository.ActualizarCliente(cliente);
        }

        public bool EliminarCliente(int idCliente)
        {
            if(idCliente <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser un valor positivo.");
            }

            return _clienteRepository.EliminarCliente(idCliente);
        }

        public Cliente? ObtenerClientePorId(int idCliente)
        {
            if(idCliente <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser un valor positivo.");
            }
            return _clienteRepository.ObtenerClientePorId(idCliente);
        }

        
    }
}
