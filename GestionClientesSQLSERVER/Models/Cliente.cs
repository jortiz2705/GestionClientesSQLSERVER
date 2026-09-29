using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientesSQLSERVER.Models
{
    public class Cliente
    {
        public int IdCliente { get; private set; }
        public string Nombre { get; private set; }
        public string Ciudad { get; private set; }
        public string? Email { get; private set; }
        public decimal Credito { get; private set; }
        public string Estado { get; private set; }
        public Cliente(int idCliente, string nombre, string ciudad, string? email, decimal credito, string estado)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Ciudad = ciudad;
            Email = email;
            Credito = credito;
            Estado = estado;
        }
    }
}
