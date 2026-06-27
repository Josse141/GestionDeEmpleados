using System;
using System.Linq;
using System.Windows.Forms;
using GestionEmpleados_DatabaseFirst.Modelos;
using GestionEmpleados_DatabaseFirst.Objetos;

namespace GestionEmpleados_DatabaseFirst
{
    public partial class FrmEmpleados : Form
    {
        //Instancia del contexto generado por Entity Framework Database First.
        private readonly DB_GestionEmpleadosEntities _dbContext;

        //Guarda el identificador del registro seleccionado para editar o eliminar.
        private int _idEmpleadoSeleccionado;

        public FrmEmpleados()
        {
            InitializeComponent();
            _dbContext = new DB_GestionEmpleadosEntities();
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            //Se cargan los tipos disponibles para registrar al empleado.
            cmbTipoEmpleado.Items.Add("PLANTA");
            cmbTipoEmpleado.Items.Add("TEMPORAL");
            cmbTipoEmpleado.SelectedIndex = 0;

            CargarEmpleados();
            LimpiarCampos();
        }

        //Consulta los empleados de la base de datos y los muestra en el DataGridView.
        private void CargarEmpleados()
        {
            var empleados = _dbContext.Empleados
                .OrderBy(e => e.Apellido)
                .ThenBy(e => e.Nombre)
                .ToList();

            dgvEmpleados.DataSource = empleados;

            if (dgvEmpleados.Columns["IdEmpleado"] != null)
            {
                dgvEmpleados.Columns["IdEmpleado"].Visible = false;
            }
        }

        //Valida los datos que se ingresan antes de guardarlos en la base.
        private bool ValidarCampos(out decimal sueldo)
        {
            sueldo = 0;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show("Complete el nombre, apellido y cargo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtSueldo.Text, out sueldo) || sueldo <= 0)
            {
                MessageBox.Show("Ingrese un sueldo válido mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSueldo.Focus();
                return false;
            }

            if (cmbTipoEmpleado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de empleado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal sueldo;

            if (!ValidarCampos(out sueldo))
            {
                return;
            }

            //Se crea el objeto que corresponde a la tabla Empleados del modelo Database First.
            var nuevoEmpleado = new EmpleadoBD
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Sueldo = sueldo,
                Cargo = txtCargo.Text.Trim(),
                TipoEmpleado = cmbTipoEmpleado.Text
            };

            _dbContext.Empleados.Add(nuevoEmpleado);
            _dbContext.SaveChanges();

            MessageBox.Show("Empleado registrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarEmpleados();
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            decimal sueldo;

            if (_idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos(out sueldo))
            {
                return;
            }

            var empleado = _dbContext.Empleados.Find(_idEmpleadoSeleccionado);

            if (empleado != null)
            {
                empleado.Nombre = txtNombre.Text.Trim();
                empleado.Apellido = txtApellido.Text.Trim();
                empleado.Sueldo = sueldo;
                empleado.Cargo = txtCargo.Text.Trim();
                empleado.TipoEmpleado = cmbTipoEmpleado.Text;

                _dbContext.SaveChanges();
                MessageBox.Show("Empleado actualizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEmpleados();
                LimpiarCampos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar el empleado seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                var empleado = _dbContext.Empleados.Find(_idEmpleadoSeleccionado);

                if (empleado != null)
                {
                    _dbContext.Empleados.Remove(empleado);
                    _dbContext.SaveChanges();

                    MessageBox.Show("Empleado eliminado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarEmpleados();
                    LimpiarCampos();
                }
            }
        }

        private void btnCalcularSueldo_Click(object sender, EventArgs e)
        {
            decimal sueldo;
            decimal bono = 0;

            if (!decimal.TryParse(txtSueldo.Text, out sueldo) || sueldo <= 0)
            {
                MessageBox.Show("Ingrese primero un sueldo base válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtBono.Text) &&
                (!decimal.TryParse(txtBono.Text, out bono) || bono < 0))
            {
                MessageBox.Show("El bono debe ser un número válido mayor o igual a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Aquí se aplica herencia: se crea una clase derivada según el tipo seleccionado.
            Empleado empleado = CrearEmpleadoPorTipo(sueldo);
            decimal total;

            //Aquí se utiliza la sobrecarga del método CalcularSueldo.
            if (bono > 0)
            {
                total = empleado.CalcularSueldo(sueldo, bono);
            }
            else
            {
                total = empleado.CalcularSueldo(sueldo);
            }

            lblResultadoSueldo.Text = "Tipo: " + empleado.ObtenerTipoEmpleado() + " | Sueldo calculado: ₡" + total.ToString("N2");
        }

        //Crea el objeto de herencia que representa el empleado seleccionado en pantalla.
        private Empleado CrearEmpleadoPorTipo(decimal sueldo)
        {
            if (cmbTipoEmpleado.Text == "TEMPORAL")
            {
                return new EmpleadoTemporal(txtNombre.Text.Trim(), txtApellido.Text.Trim(), sueldo, txtCargo.Text.Trim());
            }

            return new EmpleadoPlanta(txtNombre.Text.Trim(), txtApellido.Text.Trim(), sueldo, txtCargo.Text.Trim());
        }

        //Carga en los controles los datos del empleado seleccionado en el DataGridView.
        private void dgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmpleados.CurrentRow == null || dgvEmpleados.CurrentRow.DataBoundItem == null)
            {
                return;
            }

            EmpleadoBD empleado = dgvEmpleados.CurrentRow.DataBoundItem as EmpleadoBD;

            if (empleado == null)
            {
                return;
            }

            _idEmpleadoSeleccionado = empleado.IdEmpleado;
            txtNombre.Text = empleado.Nombre;
            txtApellido.Text = empleado.Apellido;
            txtSueldo.Text = empleado.Sueldo.ToString("0.00");
            txtCargo.Text = empleado.Cargo;
            cmbTipoEmpleado.Text = empleado.TipoEmpleado;
            txtBono.Clear();
            lblResultadoSueldo.Text = "Seleccione un bono opcional para calcular el sueldo.";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        //Limpia los controles para permitir ingresar un empleado nuevo.
        private void LimpiarCampos()
        {
            _idEmpleadoSeleccionado = 0;
            txtNombre.Clear();
            txtApellido.Clear();
            txtSueldo.Clear();
            txtCargo.Clear();
            txtBono.Clear();
            cmbTipoEmpleado.SelectedIndex = cmbTipoEmpleado.Items.Count > 0 ? 0 : -1;
            lblResultadoSueldo.Text = "Seleccione un bono opcional para calcular el sueldo.";

            dgvEmpleados.ClearSelection();
            txtNombre.Focus();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _dbContext.Dispose();
            base.OnFormClosed(e);
        }
    }
}
