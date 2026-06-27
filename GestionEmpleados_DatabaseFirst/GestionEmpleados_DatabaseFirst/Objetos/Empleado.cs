namespace GestionEmpleados_DatabaseFirst.Objetos
{
    //Clase base abstracta que contiene los datos comunes de un empleado.
    public abstract class Empleado : IEmpleado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public decimal Sueldo { get; set; }
        public string Cargo { get; set; }

        protected Empleado(string nombre, string apellido, decimal sueldo, string cargo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Sueldo = sueldo;
            Cargo = cargo;
        }

        //Primera versión del método sobrecargado: solo utiliza el sueldo base.
        public virtual decimal CalcularSueldo(decimal sueldoBase)
        {
            return sueldoBase;
        }

        //Segunda versión del método sobrecargado: agrega un bono al sueldo base.
        public virtual decimal CalcularSueldo(decimal sueldoBase, decimal bonoAdicional)
        {
            return sueldoBase + bonoAdicional;
        }

        //Cada clase derivada debe indicar el tipo de empleado que representa.
        public abstract string ObtenerTipoEmpleado();
    }
}
