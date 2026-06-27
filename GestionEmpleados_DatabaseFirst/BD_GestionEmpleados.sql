/*
    Base de datos para el proyecto Gestión de Empleados.
    Ejecutar este archivo en SQL Server Management Studio antes de abrir el proyecto.
*/

USE master;
GO

IF DB_ID('DB_GestionEmpleados') IS NOT NULL
BEGIN
    ALTER DATABASE DB_GestionEmpleados SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DB_GestionEmpleados;
END
GO

CREATE DATABASE DB_GestionEmpleados;
GO

USE DB_GestionEmpleados;
GO

CREATE TABLE dbo.Empleados
(
    IdEmpleado INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Sueldo DECIMAL(10,2) NOT NULL,
    Cargo NVARCHAR(60) NOT NULL,
    TipoEmpleado NVARCHAR(20) NOT NULL,

    CONSTRAINT PK_Empleados PRIMARY KEY (IdEmpleado),
    CONSTRAINT CK_Empleados_Sueldo CHECK (Sueldo > 0),
    CONSTRAINT CK_Empleados_Tipo CHECK (TipoEmpleado IN ('PLANTA', 'TEMPORAL'))
);
GO

-- Datos iniciales para comprobar el listado en el DataGridView.
INSERT INTO dbo.Empleados (Nombre, Apellido, Sueldo, Cargo, TipoEmpleado)
VALUES
('Ana', 'Rodríguez', 650000.00, 'Asistente Administrativa', 'PLANTA'),
('Carlos', 'Mora', 720000.00, 'Analista de Sistemas', 'PLANTA'),
('Sofía', 'Vargas', 410000.00, 'Auxiliar Contable', 'TEMPORAL');
GO

SELECT * FROM dbo.Empleados;
GO
