#Gestión de Clientes — C# + SQL Server + ADO.NET

Aplicación de consola desarrollada en C# para la gestión de clientes, utilizando SQL Server como base de datos y ADO.NET 
como tecnología de acceso a datos.
Este proyecto forma parte de mi ruta de aprendizaje para especializarme en .NET / C#, 
pasando de aplicaciones de consola hacia el desarrollo de ASP.NET Core Web API.

Objetivo

Desarrollar una aplicación que permita gestionar clientes mediante operaciones CRUD:

Registrar clientes
Listar clientes
Buscar clientes por ID
Actualizar clientes
Eliminar clientes

Además, el proyecto busca aplicar conceptos fundamentales de desarrollo profesional en .NET:

Programación Orientada a Objetos
Interfaces
Inyección de Dependencias
Patrón Repository
Separación de responsabilidades
ADO.NET
SQL Server
Stored Procedures
Manejo de excepciones
Validaciones

Tecnologías utilizadas
C#
.NET
SQL Server
ADO.NET
Microsoft.Data.SqlClient
Microsoft.Extensions.DependencyInjection
Visual Studio
Git
GitHub

Arquitectura

El proyecto utiliza una estructura separada por responsabilidades:

GestionClientesSQLSERVER
│
├── Configuration
│   └── ServiceRegistration.cs
│
├── Data
│   └── Conexion.cs
│
├── Helpers
│   └── ConsoleHelper.cs
│
├── Models
│   └── Cliente.cs
│
├── Repositories
│   ├── IClienteRepository.cs
│   ├── ClienteRepositorySql.cs
│   └── ClienteRepositorySp.cs
│
├── Services
│   ├── IClienteService.cs
│   └── ClienteService.cs
│
└── Program.cs

Responsabilidades principales

Models

Contiene las entidades utilizadas por la aplicación.

Data

Administra la conexión con SQL Server.

Repositories

Contiene la lógica de acceso a datos.

Se implementaron dos alternativas:
IClienteRepository
       │
       ├── ClienteRepositorySql
       │       └── SQL directo
       │
       └── ClienteRepositorySp
               └── Stored Procedures

Esto permite cambiar la estrategia de acceso a datos sin modificar el Service ni el programa principal.

Services

Contiene las reglas de negocio y validaciones antes de acceder al repositorio.

Helpers

Contiene métodos reutilizables para la lectura y validación de datos ingresados desde consola.

Configuration

Centraliza el registro de dependencias mediante Dependency Injection.
Inyección de Dependencias

El proyecto utiliza Microsoft.Extensions.DependencyInjection.

La aplicación trabaja contra interfaces:

IClienteService
       ↓
ClienteService
       ↓
IClienteRepository
       ↓
ClienteRepositorySp

La implementación del repositorio puede cambiar desde la configuración:

services.AddScoped<IClienteRepository, ClienteRepositorySp>();

o:

services.AddScoped<IClienteRepository, ClienteRepositorySql>();

El resto de la aplicación permanece sin cambios.

Esto permite comprender en la práctica el principio de inversión de dependencias y la ventaja de trabajar con abstracciones.

Base de datos

Base de datos:

GestionClientes

Tabla principal:

Clientes

Campos principales:

Campo	Tipo	Descripción
id_cliente	INT	Identificador del cliente
nombre	VARCHAR(100)	Nombre del cliente
ciudad	VARCHAR(50)	Ciudad
fecha_registro	DATETIME	Fecha de registro
Email	VARCHAR(100)	Correo electrónico
Credito	DECIMAL(18,2)	Crédito asignado
Estado	CHAR(1)	A = Activo / I = Inactivo

La tabla utiliza IDENTITY para generar automáticamente el identificador y valores DEFAULT para algunos campos.

También se utiliza una restricción CHECK para garantizar que el estado solamente pueda ser:

A = Activo
I = Inactivo

Acceso a datos

El proyecto implementa dos formas de acceso a SQL Server.

1. SQL directo

ClienteRepositorySql

Utiliza consultas SQL parametrizadas mediante ADO.NET.

Ejemplo conceptual:

SELECT id_cliente, nombre, ciudad, email, credito, estado
FROM Clientes
WHERE id_cliente = @id_cliente
2. Stored Procedures

ClienteRepositorySp

Utiliza procedimientos almacenados para realizar las operaciones CRUD.

Entre ellos:

sp_RegistrarCliente
sp_ListarClientes
sp_ObtenerClientePorId
sp_ActualizarCliente
sp_EliminarCliente

El proyecto permite cambiar entre ambas implementaciones mediante Dependency Injection.

Seguridad y buenas prácticas

Durante el desarrollo se aplicaron algunas prácticas importantes:

Consultas parametrizadas.
Uso de SqlCommand.
Separación entre acceso a datos y lógica de negocio.
Uso de interfaces.
Inyección de Dependencias.
Validaciones antes de guardar información.
Manejo de valores NULL.
Uso de SCOPE_IDENTITY() para recuperar el ID generado.
Restricciones en SQL Server para proteger la integridad de los datos.

Funcionalidades

Al ejecutar la aplicación se muestra el menú:

--- GESTIÓN DE CLIENTES ---

1. Listar clientes
2. Buscar cliente
3. Registrar cliente
4. Actualizar cliente
5. Eliminar cliente
6. Salir
Registrar cliente

Permite ingresar:

Nombre
Ciudad
Email
Crédito
Estado

El ID es generado automáticamente por SQL Server.

Buscar cliente

Permite consultar un cliente utilizando su ID.

Actualizar cliente

Permite modificar los datos de un cliente existente.

Eliminar cliente

Permite eliminar un cliente mediante su ID.

Listar clientes

Obtiene todos los clientes registrados en la base de datos.

Configuración de conexión

La conexión se encuentra centralizada en:

Data/Conexion.cs

Ejemplo:

Server=INFORMATICA;
Database=GestionClientes;
Integrated Security=True;
TrustServerCertificate=True;

La cadena de conexión debe adaptarse al entorno donde se ejecute el proyecto.

Cómo ejecutar el proyecto
1. Clonar el repositorio
git clone https://github.com/jortiz2705/GestionClientesSQL.git
2. Crear la base de datos

Crear en SQL Server la base de datos:

GestionClientes
3. Crear la tabla y procedimientos

Ejecutar el script SQL incluido en el proyecto.

4. Configurar la conexión

Revisar:

Data/Conexion.cs

y colocar el nombre de la instancia de SQL Server correspondiente.

5. Ejecutar

Abrir la solución en Visual Studio y ejecutar el proyecto.

Conceptos aprendidos

Este proyecto permitió practicar y consolidar:

C#
Programación Orientada a Objetos
Clases y objetos
Encapsulamiento
Interfaces
Inyección de Dependencias
ServiceCollection
ServiceProvider
IServiceScope
AddScoped
Patrón Repository
Capa de servicios
ADO.NET
SqlConnection
SqlCommand
SqlDataReader
Parámetros SQL
ExecuteReader
ExecuteScalar
ExecuteNonQuery
SQL Server
Stored Procedures
CRUD
Validaciones
Manejo de excepciones

Evolución del proyecto

Este proyecto representa el segundo paso de mi aprendizaje práctico en .NET.

Proyecto 01

Gestión de Clientes — C#

Conceptos principales:

C#
POO
Clases
Encapsulamiento
List<T>
LINQ
Validaciones
Proyecto 02

Gestión de Clientes — C# + SQL Server

Conceptos principales:

C#
SQL Server
ADO.NET
Repository
Service
Interfaces
Dependency Injection
Stored Procedures
CRUD
Próximo proyecto

Proyecto 03 — ASP.NET Core Web API

El siguiente paso será transformar los conocimientos adquiridos en una aplicación Web API utilizando:

ASP.NET Core
REST API
HTTP
Controllers
DTOs
Dependency Injection
SQL Server

Autor

Jhon Ortiz

Ingeniero de Sistemas

En proceso de especialización práctica en:

C#
.NET
ASP.NET Core
SQL Server
Desarrollo de APIs
Inteligencia Artificial aplicada al desarrollo empresarial

GitHub:

jortiz2705
Proyecto desarrollado como parte de mi aprendizaje práctico de C# y .NET, con énfasis en arquitectura, 
acceso a datos y buenas prácticas de desarrollo.