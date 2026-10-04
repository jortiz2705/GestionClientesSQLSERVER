using System;
using System.Collections.Generic;
using System.Text;

namespace GestionClientesSQLSERVER.Helpers
{
    public class ConsoleHelper
    {
        public static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Por favor, ingrese un dato válido.");
                    Console.ResetColor();
                }
            }
        }
        public static string LeerEstado(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? entrada = Console.ReadLine()?.Trim().ToUpper();
                if (!string.IsNullOrWhiteSpace(entrada) && (entrada == "A" || entrada == "I"))
                {
                    return entrada;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Por favor, ingrese un estado válido (A/I).");
                    Console.ResetColor();
                }
            }
        }
        public static int LeerNumero(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out int resultado) && resultado > 0)
                {
                    return resultado;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Por favor, ingrese un dato válido.");
                    Console.ResetColor();
                }
            }
        }
        public static decimal LeerDecimal(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (decimal.TryParse(Console.ReadLine(), out decimal resultado) && resultado > 0)
                {
                    return resultado;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Por favor, ingrese un dato válido.");
                    Console.ResetColor();
                }
            }
        }
    }
}
