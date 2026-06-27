# Resumen del desarrollo

El proyecto se desarrolló como una aplicación de escritorio en C# Windows Forms. Para manejar la información se utilizó SQL Server con Entity Framework bajo el enfoque Database First. Primero se creó la base de datos mediante el archivo `BD_GestionEmpleados.sql`; posteriormente se construyó el modelo `ModeloEmpleados.edmx` para representar la tabla `Empleados` dentro de la aplicación.

La tabla principal contiene un identificador autoincremental y los datos requeridos: nombre, apellido, sueldo y cargo. Se agregó el campo `TipoEmpleado` para guardar si el registro representa a un empleado de planta o temporal. Esto permite utilizar el mismo dato tanto en el CRUD como en la parte de herencia.

En el formulario principal se implementó el CRUD completo. El DataGridView lista los empleados con una consulta LINQ. Para guardar un registro se utiliza `Add()` y `SaveChanges()`. Para editar se localiza el objeto con `Find()`, se modifican sus propiedades y se ejecuta `SaveChanges()`. Para eliminar se utiliza `Remove()` seguido de `SaveChanges()`.

También se implementó la interfaz `IEmpleado`, la clase abstracta `Empleado` y las clases derivadas `EmpleadoPlanta` y `EmpleadoTemporal`. La clase base posee dos métodos con el mismo nombre, `CalcularSueldo`: uno recibe únicamente el sueldo base y el otro recibe el sueldo base junto con un bono adicional. El formulario permite probar ambos métodos mediante el botón de cálculo.

El modelo de base de datos se mantiene separado de las clases de herencia. `EmpleadoBD` representa el registro generado por Database First, mientras que las clases `Empleado`, `EmpleadoPlanta` y `EmpleadoTemporal` se utilizan para demostrar los conceptos de programación orientada a objetos solicitados en el enunciado.
