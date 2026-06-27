# Corrección aplicada a las advertencias de Visual Studio

Se corrigieron dos advertencias que aparecían al abrir la solución:

1. **Settings.settings**
   - El archivo existía dentro de `Properties`, pero no estaba incluido como elemento del proyecto.
   - Se agregó la referencia correcta en el archivo `.csproj` usando el generador `SettingsSingleFileGenerator`.

2. **ModeloEmpleados.edmx**
   - El proyecto ya utiliza los archivos T4 `ModeloEmpleados.Context.tt` y `ModeloEmpleados.tt` para contener el contexto y la entidad generada.
   - También tenía configurado el generador antiguo `EntityModelCodeGenerator`, que esperaba un archivo adicional llamado `ModeloEmpleados.Designer.cs`.
   - Se eliminó esa configuración duplicada para evitar la advertencia.

Después de abrir esta versión, se recomienda ejecutar:

1. `Build > Clean Solution`
2. `Build > Rebuild Solution`

No es necesario usar **Run Custom Tool** sobre el archivo `.edmx` para compilar esta versión.
