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
            
            if (estado != "A" && estado != "I" && string.IsNullOrWhiteSpace(estado))
            {
                throw new ArgumentException("El estado debe ser 'A' (Activo) o 'I' (Inactivo).");
            }
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }
            if (string.IsNullOrWhiteSpace(ciudad))
            {
                throw new ArgumentException("La ciudad no puede estar vacía.");
            }
            if (credito < 0)
            {
                throw new ArgumentException("El crédito no puede ser negativo.");
            }
            IdCliente = idCliente;
            Nombre = nombre;
            Ciudad = ciudad;
            Email = email;
            Credito = credito;
            Estado = estado;
        }
    }
}
