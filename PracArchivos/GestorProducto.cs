using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracArchivos
{
    public class GestorProducto
    {

        public string Ruta { get; set; }

        public GestorProducto(string ruta) 
        {
            Ruta = ruta;
        }

        public void RegistrarProducto(Producto producto)
        {
          FileStream fileStream = new FileStream(Ruta,FileMode.Append,FileAccess.Write); //0101011010101
          
            using (StreamWriter streamWriter = new StreamWriter(fileStream)) //ASCI,
            {
                streamWriter.WriteLine(producto.GenerarRegistro());
            }
            fileStream.Close();
        }

        public void EliminarProducto(int numero)
        {
            string output = string.Empty;

            FileStream fs = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read);

            using (StreamReader reader = new StreamReader(fs))
            {
                string linea = reader.ReadLine();


                while (linea != null)
                {
                    Producto producto = new Producto(linea);

                    if (producto.Numero!=numero)
                    {
                        output += linea + Environment.NewLine;
                    }

                    linea=reader.ReadLine();

                }
            }
            fs.Close();

            fs = new FileStream(Ruta, FileMode.Truncate, FileAccess.Write);

            using (StreamWriter writer=new StreamWriter(fs))
            {
                writer.Write(output);
            }
            fs.Close();

        }


        public List<Producto> ListarProductos()
        {
            List<Producto> productos = new List<Producto>();

            FileStream fileStream= new FileStream (Ruta,FileMode.OpenOrCreate,FileAccess.Read);

            using (StreamReader streamReader= new StreamReader(fileStream))
            {
                string linea=streamReader.ReadLine();

                while(linea!=null)

                    {
                      Producto producto = new Producto(linea);
                     
                     productos.Add(producto);
                     linea=streamReader.ReadLine(); 

                    }
            }
            fileStream.Close();

            return productos;
        }
        public void ModificarCliente(int numero, Producto unClienteModificado)
        {
            string output = string.Empty;

            FileStream fs = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read);

            using (StreamReader sr = new StreamReader(fs))
            {
                string linea = sr.ReadLine();

                while (linea != null)
                {
                    Producto unCliente = new Producto(linea);

                    if (unCliente.Numero == numero)
                    {
                        unCliente = unClienteModificado;
                    }

                    output += unCliente.GenerarRegistro() + Environment.NewLine;
                    linea = sr.ReadLine();
                }

            }
            fs.Close();

            fs = new FileStream(Ruta, FileMode.Truncate, FileAccess.Write);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(output);
            }
            fs.Close();

        }


    }
}
