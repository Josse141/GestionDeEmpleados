namespace GestionEmpleados_DatabaseFirst.Objetos
{
    //Representa a un empleado contratado de planta.
    public class EmpleadoPlanta : Empleado
    {
        public EmpleadoPlanta(string nombre, string apellido, decimal sueldo, string cargo)
            : base(nombre, apellido, sueldo, cargo)
        {
        }

        public override string ObtenerTipoEmpleado()
        {
            return "PLANTA";
        }
    }
}
