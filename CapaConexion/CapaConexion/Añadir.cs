using DatosLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaConexion
{
    public partial class Añadir : Form
    {
        CustomerRepository cliente = new CustomerRepository();
        string id_ = "";
        public Añadir(string id)
        {
            InitializeComponent();
            id_ = id; 

            if (id_ != "")
            {
                this.Text = "Modificar Cliente";
                addPerson.Text = "Edicion de Cliente";
                btnInsertarDatos.Hide();
                CargarDatos();
            }
            else
            {
               
                btnModificar.Hide();
            }
        }
        public void CargarDatos()
        {
            var perso = cliente.ObtenerPorID(id_);

            tboxCustomerID.Text = perso.CustomerID;
            tboxCompanyName.Text = perso.CompanyName;
            tboxContactName.Text = perso.ContactName;
            tboxContactTitle.Text = perso.ContactTitle;
            tboxAddress.Text = perso.Address;
            tboxCity.Text = perso.City;
        }
        private void btnInsertarDatos_Click(object sender, EventArgs e)
        {
            var nuevoCliente = new customers {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };
            ValidarDatos();
            var resultado = 0;
            if (retornar == true)
            {
                var repository = new CustomerRepository();
                resultado = repository.InsertarCliente(nuevoCliente);
                MessageBox.Show("Cliente Ingresado", "Añadir Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tboxCustomerID.Text = "";
                tboxCompanyName.Text = "";
                tboxContactName.Text = "";
                tboxContactTitle.Text = "";
                tboxAddress.Text = "";
                tboxCity.Text = "";
                retornar = false;
            }
            
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor" + resultado);
            }
        }

        private customers ObtenerNuevoCliente()
        {
            var nuevoCliente = new customers
            {

                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };
            return nuevoCliente; // Retorna el nuevo objeto Person
        }

        bool retornar = false;

        private void ValidarDatos()
        {
            if (tboxCustomerID.Text != "" && tboxCompanyName.Text != "" && tboxContactTitle.Text != "" && tboxCity.Text != "" &&
                tboxAddress.Text != "" && tboxContactName.Text != "")
            {
                retornar = true;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ValidarDatos();
            if (retornar == true)
            {
                // Obtiene los datos actualizados del cliente desde los campos de texto
                var update = ObtenerNuevoCliente();
                // Llama al método para actualizar el personal y obtiene el resultado
                int actulizar = cliente.ActualizarCliente(update, id_);

                // Verifica si la actualización fue exitosa
                if (actulizar > 0)
                {
                    // Muestra un mensaje de éxito y cierra el formulario
                    MessageBox.Show($"Se ha actualizado de forma EXITOSA", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    // Muestra un mensaje de error si la actualización falló
                    MessageBox.Show($"ERROR", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
