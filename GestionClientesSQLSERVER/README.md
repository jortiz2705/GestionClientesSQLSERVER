# Gestión de Clientes — C# + SQL Server + ADO.NET

**Aplicación de consola** desarrollada en C# para la gestión de clientes, utilizando **SQL Server** como base de datos y **ADO.NET** como tecnología de acceso a datos.

Este proyecto forma parte de mi ruta de aprendizaje para especializarme en **.NET / C#**, pasando de aplicaciones de consola hacia el desarrollo de **ASP.NET Core Web API**.

---

## **Objetivo**

Desarrollar una aplicación que permita gestionar clientes mediante operaciones **CRUD**:
* Registrar clientes
* Listar clientes
* Buscar clientes por ID
* Actualizar clientes
* Eliminar clientes

Además, el proyecto busca aplicar conceptos fundamentales de **desarrollo profesional en .NET**:
* Programación Orientada a Objetos
* Interfaces e Inyección de Dependencias
* Patrón Repository y Separación de responsabilidades
* ADO.NET, SQL Server y Stored Procedures
* Manejo de excepciones y Validaciones

---

## **Tecnologías utilizadas**

* **Lenguaje:** C# / .NET
* **Base de Datos:** SQL Server
* **Acceso a Datos:** ADO.NET / `Microsoft.Data.SqlClient`
* **Herramientas:** Microsoft.Extensions.DependencyInjection, Visual Studio, Git, GitHub

---

## **Arquitectura del Proyecto**

El proyecto utiliza una estructura limpia separada por capas y responsabilidades:

```text
GestionClientesSQLSERVER
│
├── 📁 Configuration
│   └── ServiceRegistration.cs
│
├── 📁 Data
│   └── Conexion.cs
│
├── 📁 Helpers
│   └── ConsoleHelper.cs
│
├── 📁 Models
│   └── Cliente.cs
│
├── 📁 Repositories
│   ├── IClienteRepository.cs
│   ├── ClienteRepositorySql.cs
│   └── ClienteRepositorySp.cs
│
├── 📁 Services
│   ├── IClienteService.cs
│   └── ClienteService.cs
│
└── 📄 Program.cs
```

---

## **Responsabilidades principales**

### **Models**
Contiene las entidades utilizadas por la aplicación.

### **Data**
Administra la conexión directa con SQL Server.

### **Repositories**
Contiene la lógica de acceso a datos. Se implementaron dos alternativas bajo una misma interfaz, lo que permite cambiar la estrategia de acceso a datos sin modificar la capa de servicios ni el programa principal:
```text
      IClienteRepository
              │
              ├──► ClienteRepositorySql (SQL directo)
              └──► ClienteRepositorySp  (Stored Procedures)
```

### **Services**
Contiene las reglas de negocio y validaciones antes de mandar la información al repositorio.

### **Helpers**
Contiene métodos reutilizables para la lectura y validación segura de datos ingresados desde la consola.

### **Configuration**
Centraliza el registro de dependencias mediante **Dependency Injection**.

---

## **Inyección de Dependencias**

El proyecto utiliza `Microsoft.Extensions.DependencyInjection`. La aplicación trabaja estrictamente contra interfaces, permitiendo un acoplamiento débil:

```text
IClienteService ──► ClienteService ──► IClienteRepository ──► ClienteRepositorySp / Sql
```

La implementación del repositorio se puede intercambiar desde la configuración cambiando solo una línea de código:

```csharp
// Opción A: Usando Procedimientos Almacenados
services.AddScoped<IClienteRepository, ClienteRepositorySp>();

// Opción B: Usando SQL directo
// services.AddScoped<IClienteRepository, ClienteRepositorySql>();
```
El resto de la aplicación permanece sin cambios, cumpliendo con el **Principio de Inversión de Dependencias**.

---

## **Base de datos**

* **Base de datos:** `GestionClientes`
* **Tabla principal:** `Clientes`

### **Estructura de la Tabla**

| Campo | Tipo | Descripción |
| :--- | :--- | :--- |
| **id_cliente** | `INT` | Identificador único (IDENTITY) |
| **nombre** | `VARCHAR(100)` | Nombre completo del cliente |
| **ciudad** | `VARCHAR(50)` | Ciudad de residencia |
| **fecha_registro** | `DATETIME` | Fecha de registro automático (DEFAULT) |
| **Email** | `VARCHAR(100)` | Correo electrónico |
| **Credito** | `DECIMAL(18,2)`| Crédito financiero asignado |
| **Estado** | `CHAR(1)` | Estado: **A** = Activo / **I** = Inactivo (Restricción CHECK) |

---

## **Acceso a datos**

El proyecto implementa dos formas independientes para interactuar con SQL Server:

### **SQL Directo (`ClienteRepositorySql`)**
Utiliza consultas SQL fuertemente parametrizadas mediante ADO.NET para evitar inyecciones de código:
```sql
SELECT id_cliente, nombre, ciudad, email, credito, estado 
FROM Clientes 
WHERE id_cliente = @id_cliente
```

### **Stored Procedures (`ClienteRepositorySp`)**
Utiliza los siguientes procedimientos almacenados en la base de datos para delegar la ejecución al servidor:
* `sp_RegistrarCliente`
* `sp_ListarClientes`
* `sp_ObtenerClientePorId`
* `sp_ActualizarCliente`
* `sp_EliminarCliente`

---

## **Seguridad y buenas prácticas**

* **Consultas parametrizadas:** Uso estricto de `SqlCommand` con parámetros para evitar vulnerabilidades.
* **Separación de capas:** Desacoplamiento total entre el acceso a datos y la lógica de negocio.
* **Integridad:** Uso de restricciones `CHECK` y `DEFAULT` directamente en SQL Server.
* **Eficiencia:** Recuperación de IDs autogenerados mediante `SCOPE_IDENTITY()`.

---

## **Funcionalidades (Interfaz de Consola)**

Al iniciar la aplicación, el usuario interactúa con el siguiente menú dinámico:

```text
--- GESTIÓN DE CLIENTES ---

1. Listar clientes
2. Buscar cliente
3. Registrar cliente
4. Actualizar cliente
5. Eliminar cliente
6. Salir
```

* **Registrar cliente:** Pide Nombre, Ciudad, Email, Crédito y Estado. El ID lo genera la base de datos.
* **Buscar cliente:** Consulta la información detallada filtrando por su ID.
* **Actualizar cliente:** Permite modificar campos específicos de un registro existente.
* **Eliminar cliente:** Remueve al cliente de la base de datos mediante su ID.
* **Listar clientes:** Muestra de forma ordenada todos los registros de la tabla.

---

## **Configuración de conexión**

La cadena de conexión se encuentra centralizada en **`Data/Conexion.cs`**:

```csharp
Server=INFORMATICA; Database=GestionClientes; Integrated Security=True; TrustServerCertificate=True;
```
> **Nota:** Recuerda adaptar este String a los parámetros de tu instancia local de SQL Server antes de ejecutar el proyecto.

---

## **Cómo ejecutar el proyecto**

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com
   ```
2. **Crear la Base de Datos:** Crea una base de datos vacía llamada `GestionClientes` en tu servidor de SQL Server.
3. **Ejecutar Scripts:** Ejecuta el script SQL adjunto en el proyecto para crear la tabla `Clientes` y los Stored Procedures necesarios.
4. **Configurar Conexión:** Abre `Data/Conexion.cs` y actualiza la propiedad `Server` con el nombre de tu servidor local.
5. **Compilar y Correr:** Abre la solución (`.slnx` o `.sln`) en **Visual Studio** y presiona `F5`.

---

## 📈 **Evolución de mi Ruta .NET**

* **Proyecto 01 — Gestión de Clientes (Consola):** C#, POO básica, Listas en memoria (`List<T>`), LINQ y validaciones básicas.
* **Proyecto 02 — Gestión de Clientes + SQL (Este proyecto):** Arquitectura por capas, SQL Server, ADO.NET, Interfaces, Dependency Injection y Stored Procedures.
* **Próximo proyecto — ASP.NET Core Web API:** El siguiente paso será transformar este backend en una API REST moderna utilizando HTTP Controllers, DTOs, y Entity Framework Core.

---

## **Autor**

**Jhon Ortiz** — *Ingeniero de Sistemas*
* **GitHub:** [@jortiz2705](https://github.com)

*En proceso de especialización práctica en C#, .NET, ASP.NET Core, SQL Server, Arquitectura de Software e IA aplicada al desarrollo empresarial.*
