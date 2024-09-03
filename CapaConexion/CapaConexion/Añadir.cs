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

namespace CapaConexion
{
    public partial class Añadir : Form
    {
        public Añadir()
        {
            InitializeComponent();
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
            
            var resultado = 0;
            if (validarCampoNull(nuevoCliente) == false)
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
            }
            
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor" + resultado);
            }
        }

        public Boolean validarCampoNull(Object objeto)
        {

            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
