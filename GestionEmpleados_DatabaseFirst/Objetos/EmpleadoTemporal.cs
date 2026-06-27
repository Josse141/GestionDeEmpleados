namespace GestionEmpleados_DatabaseFirst.Objetos
{
    //Representa a un empleado contratado de forma temporal.
    public class EmpleadoTemporal : Empleado
    {
        public EmpleadoTemporal(string nombre, string apellido, decimal sueldo, string cargo)
            : base(nombre, apellido, sueldo, cargo)
        {
        }

        public override string ObtenerTipoEmpleado()
        {
            return "TEMPORAL";
        }
    }
}
