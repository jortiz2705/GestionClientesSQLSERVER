using GestionClientesSQLSERVER.Configuration;
using GestionClientesSQLSERVER.Models;
using GestionClientesSQLSERVER.Repositories;
using GestionClientesSQLSERVER.Services;
using Microsoft.Extensions.DependencyInjection;
using GestionClientesSQLSERVER.Helpers;

var services = new ServiceCollection();

services.RegistrarDependencias();

ServiceProvider provider = services.BuildServiceProvider();

using IServiceScope scope = provider.CreateScope();

IClienteService service = scope.ServiceProvider.GetRequiredService<IClienteService>();

int opcion;
do
{
    Console.WriteLine("\n--- GESTIÓN DE CLIENTES ---");
    Console.WriteLine("1. Listar clientes");
    Console.WriteLine("2. Buscar cliente");
    Console.WriteLine("3. Registrar cliente");
    Console.WriteLine("4. Actualizar cliente");
    Console.WriteLine("5. Eliminar cliente");
    Console.WriteLine("6. Salir");
    Console.Write("Opción: ");
    int.TryParse(Console.ReadLine(), out opcion);

    switch (opcion)
    {
        case 1:
            var clientes = service.ListarClientes();
            foreach (var lista in clientes)
            {
                Console.WriteLine($"ID: {lista.IdCliente}");
                Console.WriteLine($"Nombre: {lista.Nombre}");
                Console.WriteLine($"Ciudad: {lista.Ciudad}");
                Console.WriteLine($"Email: {lista.Email}");
                Console.WriteLine($"Crédito: S/. {lista.Credito}");
                Console.WriteLine($"Estado: {lista.Estado}");
                Console.WriteLine(new string('-', 40));
            }
            break;
        case 2:
            Console.Write("Ingrese el ID del cliente a buscar: ");
            int idCliente = ConsoleHelper.LeerNumero("ID del cliente: ");
            var cliente = service.ObtenerClientePorId(idCliente);
            if (cliente != null)
            {
                Console.WriteLine($"ID: {cliente.IdCliente}");
                Console.WriteLine($"Nombre: {cliente.Nombre}");
                Console.WriteLine($"Ciudad: {cliente.Ciudad}");
                Console.WriteLine($"Email: {cliente.Email}");
                Console.WriteLine($"Crédito: S/. {cliente.Credito}");
                Console.WriteLine($"Estado: {cliente.Estado}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
            break;
        case 3:
            Console.WriteLine("Ingrese los datos del nuevo cliente:");
            string nombre = ConsoleHelper.LeerTexto("Nombre: ");
            string ciudad = ConsoleHelper.LeerTexto("Ciudad: ");
            string email = ConsoleHelper.LeerTexto("Email: ");
            decimal credito = ConsoleHelper.LeerDecimal("Crédito: ");
            string estado = ConsoleHelper.LeerEstado("Estado (A/I): ");
            var nuevoCliente = new Cliente(0, nombre, ciudad, email, credito, estado);
            int idRegistro = service.RegistrarCliente(nuevoCliente);
            if (idRegistro > 0)
            {
                Console.WriteLine($"Cliente registrado correctamente. ID: {idRegistro}");
            }
            else
            {
                Console.WriteLine("Error al registrar el cliente.");
            }
            break;
        case 4:
            Console.Write("Ingrese el ID del cliente a buscar: ");
            int idClienteBuscar = ConsoleHelper.LeerNumero("ID del cliente: ");
            var clienteEncontrado = service.ObtenerClientePorId(idClienteBuscar);
            if (clienteEncontrado != null)
            {
                Console.WriteLine("Ingrese los datos del nuevo cliente:");
                string nombreAtualizado = ConsoleHelper.LeerTexto("Nombre: ");
                string ciudadActualizado = ConsoleHelper.LeerTexto("Ciudad: ");
                string emailActualizado = ConsoleHelper.LeerTexto("Email: ");
                decimal creditoActualizado = ConsoleHelper.LeerDecimal("Crédito: ");
                string estadoActualizado = ConsoleHelper.LeerEstado("Estado (A/I): ");
                var clienteActualizado = new Cliente(idClienteBuscar, nombreAtualizado, ciudadActualizado, emailActualizado, creditoActualizado, estadoActualizado);
                bool exito = service.ActualizarCliente(clienteActualizado);
                if (exito)
                {
                    Console.WriteLine("Cliente actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el cliente.");
                }
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
            break;
        case 5:
            Console.Write("Ingrese el ID del cliente a eliminar: ");
            int idClienteEliminar = int.Parse(Console.ReadLine()!);
            bool exitoEliminar = service.EliminarCliente(idClienteEliminar);
            if (exitoEliminar)
            {
                Console.WriteLine("Cliente eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Error al eliminar el cliente.");
            }
            break;
        case 6:
            Console.WriteLine("Saliendo del programa...");
            break;
    }
        
} while (opcion != 6);


/*
Console.WriteLine("=== ELIMINAR CLIENTE ===");
var idClienteAEliminar = 1; // ID del cliente que deseas eliminar

try
{
    var repository = new ClienteRepository();
    bool exito = repository.EliminarCliente(idClienteAEliminar);
    if (exito)
    {
        Console.WriteLine("Cliente eliminado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al eliminar el cliente.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrió un error al eliminar el cliente: {ex.Message}");
}
*/


/*
Console.WriteLine("=== ACTUALIZAR DATOS DE CLIENTES ===");
var clienteActualizado = new Cliente(
    1, // ID del cliente que deseas actualizar
    "Aron Briceño", // Nuevo nombre
    "Chimbote", // Nueva ciudad
    "aron.briceño@example.com", // Nuevo email
    2500m, // Nuevo crédito
    "A" // Nuevo estado
);
try
{
    var repository = new ClienteRepository();
    bool exito = repository.ActualizarCliente(clienteActualizado);
    if (exito)
    {
        Console.WriteLine("Cliente actualizado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al actualizar el cliente.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrió un error al actualizar el cliente: {ex.Message}");
}
*/


/*
Console.WriteLine("=== REGISTRO DE NUEVO CLIENTE ===");
var nuevoCliente = new Cliente(
0, // El ID se generará automáticamente en la base de datos
"Eva Villar","Chimbote","eva.villar@example.com",2500m,"A");

try
{
    IClienteRepository repository = new ClienteRepository();

    IClienteService service = new ClienteService(repository);

    bool exito = service.RegistrarCliente(nuevoCliente);

    if (exito)
    {
        Console.WriteLine("Cliente registrado correctamente.");
    }
    else
    {
        Console.WriteLine("Error al registrar el cliente.");
    }

}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrió un error al registrar el cliente: {ex.Message}");
}
*/



/*
//LISTAR CLIENTES
Console.WriteLine("=== BIENVENIDO AL SISTEMA DE GESTIÓN DE CLIENTES ===");
Console.WriteLine("Cargando clientes desde la base de datos...\n");
try
{
    // 1. Instanciamos la clase donde creaste el método ListarClientes
  //  IClienteRepository repositorio = new ClienteRepositorySql();

  //  IClienteService service = new ClienteService(repositorio);

    // 2. Llamamos al método y guardamos el resultado en una lista local
    List<Cliente> listaDeClientes = service.ListarClientes();

    // 3. Validamos si la lista tiene registros
    if (!listaDeClientes.Any())
    {
        Console.WriteLine("No se encontraron clientes registrados.");
    }
    else
    {
        // 4. Recorremos la lista con un bucle foreach para imprimir cada cliente
        foreach (Cliente cli in listaDeClientes)
        {
            Console.WriteLine($"ID: {cli.IdCliente}");
            Console.WriteLine($"Nombre: {cli.Nombre}");
            Console.WriteLine($"Ciudad: {cli.Ciudad}");
            Console.WriteLine($"Email: {cli.Email}");
            Console.WriteLine($"Crédito: S/. {cli.Credito}");
            Console.WriteLine($"Estado: {cli.Estado}");
            Console.WriteLine(new string('-', 40)); // Línea separadora
        }

        Console.WriteLine($"\nSe listaron {listaDeClientes.Count} clientes correctamente.");
    }
}
catch (Exception ex)
{
    // Si algo falla en la conexión o en la consulta, saltará aquí
    Console.WriteLine($"Ocurrió un error general: {ex.Message}");
}

Console.WriteLine("\nPresiona cualquier tecla para salir...");
Console.ReadKey();
*/