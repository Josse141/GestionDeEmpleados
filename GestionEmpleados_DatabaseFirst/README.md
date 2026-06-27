# Gestión de Empleados - Windows Forms + Entity Framework Database First

## Requisitos

- Visual Studio 2019 o 2022 con la carga de trabajo **Desarrollo de escritorio de .NET**.
- .NET Framework 4.7.2.
- SQL Server Express LocalDB o SQL Server LocalDB.
- SQL Server Management Studio (SSMS), recomendado para ejecutar el script.

## Orden de uso

Para conexión con DB:

Server Name: (localdb)\MSSQLLocalDB
Authentication: Windows Authentication
Database Name: <default>
Encrypt: Optional

1. Abra `BD_GestionEmpleados.sql` con SQL Server Management Studio.
2. Ejecute todo el script. Este crea la base `DB_GestionEmpleados`, la tabla `Empleados` y tres registros de prueba.
3. Abra `GestionEmpleados_DatabaseFirst.sln` en Visual Studio.
4. Restaure los paquetes NuGet si Visual Studio lo solicita.
5. Compile y ejecute el proyecto.

## Database First

La base se crea primero a partir del archivo SQL. El proyecto contiene el modelo `Modelos/ModeloEmpleados.edmx`, el contexto `DB_GestionEmpleadosEntities` y la entidad `EmpleadoBD`, generados siguiendo la estructura de Entity Framework Database First.

La cadena de conexión usa LocalDB:

```text
(localdb)\MSSQLLocalDB
```

Si se usa otra instancia de SQL Server, se debe actualizar `App.config` en la sección `connectionStrings`.

## Funciones del formulario

- Listar empleados en un `DataGridView`.
- Agregar empleados.
- Editar empleados seleccionados.
- Eliminar empleados seleccionados.
- Registrar tipo de empleado: `PLANTA` o `TEMPORAL`.
- Calcular sueldo base o sueldo con bono adicional.

## Clases solicitadas

- `IEmpleado`: define el cálculo de sueldo y el tipo de empleado.
- `Empleado`: clase abstracta base.
- `EmpleadoPlanta`: hereda de `Empleado`.
- `EmpleadoTemporal`: hereda de `Empleado`.

El botón **Calcular sueldo** demuestra la creación de objetos derivados y las dos versiones sobrecargadas de `CalcularSueldo`.
