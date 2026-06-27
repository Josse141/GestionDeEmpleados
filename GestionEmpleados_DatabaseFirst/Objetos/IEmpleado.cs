namespace GestionEmpleados_DatabaseFirst.Objetos
{
    //Define los métodos que deben cumplir los diferentes tipos de empleados.
    public interface IEmpleado
    {
        decimal CalcularSueldo(decimal sueldoBase);

        decimal CalcularSueldo(decimal sueldoBase, decimal bonoAdicional);

        string ObtenerTipoEmpleado();
    }
}
