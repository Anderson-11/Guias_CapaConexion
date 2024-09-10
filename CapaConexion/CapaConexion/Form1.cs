using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {

            var Customers = customerRepository.ObtenerTodos();
            dataGrid.DataSource = Customers;
            
        }

        private void FiltroBox_TextChanged(object sender, EventArgs e)
        {
            var ObtenerTodo = customerRepository.ObtenerTodos();

            var filtro = ObtenerTodo.FindAll(X => X.CompanyName.StartsWith(FiltroBox.Text));
            dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            //DatosLayer.DataBase.ConnetionTimeout = 30;
            //string cadenaConexion = DatosLayer.DataBase.ConnectionString;

            //var conectarDB = DatosLayer.DataBase.GetSqlConnection();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            
            if (cliente != null)
            {
                txtBuscar.Text = cliente.CompanyName;
                if(MessageBox.Show($"Quieres editar a: '{cliente.CompanyName}'", "Editar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                    Añadir persona = new Añadir(cliente.CustomerID);
                    persona.ShowDialog();
                }
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Añadir persona = new Añadir(id: "");
            persona.ShowDialog();
        }
    }
}
