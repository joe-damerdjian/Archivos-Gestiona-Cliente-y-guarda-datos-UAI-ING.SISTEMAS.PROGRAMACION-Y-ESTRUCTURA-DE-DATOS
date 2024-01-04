using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracArchivos
{
    public partial class Form1 : Form
    {

        GestorProducto gestorProducto;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    gestorProducto = new GestorProducto(openFileDialog.FileName);
                    lblNombreArchivo.Text = openFileDialog.FileName;
                    MostrarLista();
                    btnCargarArchivo.Enabled = false;
                    btnRegistrar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el archivo.");
            }

        
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComprobarNombre(txtNombre.Text) && ComprobarNumeros(txtNumero.Text) && ComprobarNumeros(txtPrecio.Text))
                {
                    Producto producto = new Producto();
                    producto.Numero = Convert.ToInt32(txtNumero.Text);
                    producto.Precio = Convert.ToDecimal(txtPrecio.Text);
                    producto.Nombre = txtNombre.Text;

                    gestorProducto.RegistrarProducto(producto);

                    MostrarLista();
                }

                else
                {
                    MessageBox.Show("Hay un error en el formato de entrada de datos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al registrar producto!");
            }
        }


        public static bool ComprobarNumeros(string linea)
        {
            return Regex.IsMatch(linea, @"^[0-9]+$");
        }
        public static bool ComprobarNombre(string linea)
        {
            return Regex.IsMatch(linea, @"^[a-zA-Z ]+$");
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count>0)
            {
                Producto producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
                gestorProducto.EliminarProducto(producto.Numero);
                MostrarLista();

            }
        }

        public void MostrarLista()
        {
            dgvProductos.DataSource=gestorProducto.ListarProductos();
        }

        private void problemasEnRegistrarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Recuerda que tu producto debe cumplir con el formato correcto de entrada, de lo contrario no podremos tomar su producto.", "Validar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guiaCompletaValidacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para recurrir a la guia completa de validacion puede visitar nuestro sitio web 'www.ProductsSkinCare/help-guia.com'.", "Gracias por su atencion!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void rojoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
        }

        private void verdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
        }

        private void azulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {

                if (ComprobarNombre(txtNombre.Text) && ComprobarNumeros(txtNumero.Text) && ComprobarNumeros(txtPrecio.Text))
                {


                    Producto unClienteModificado = new Producto();
                    unClienteModificado.Numero = Convert.ToInt32(txtNumero.Text);
                    unClienteModificado.Precio = Convert.ToDecimal(txtPrecio.Text);
                    unClienteModificado.Nombre = txtNombre.Text;
                    Producto unCliente = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
                    gestorProducto.ModificarCliente(unCliente.Numero, unClienteModificado);
                    MostrarLista();
                }
                else
                {
                    MessageBox.Show("Error en la entrada de datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay elementos en la lista o no ha seleccionado ninguno.", "Chequear", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }

}
