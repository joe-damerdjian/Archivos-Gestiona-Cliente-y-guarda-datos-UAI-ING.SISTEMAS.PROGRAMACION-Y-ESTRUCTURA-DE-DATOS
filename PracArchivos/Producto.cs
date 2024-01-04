using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracArchivos
{
    public class Producto
    {
        public int Numero { get; set; }
        public decimal Precio { get; set; } //..055,3535,53 - float ->0.43
        public string Nombre { get; set; }  //cadena 

        
        


        public Producto()
        {
            //inicializa
            
        }

        public Producto(string linea) //<- cadena de caracteres.
        {
            string[] datos; //datos,

            datos = linea.Split(';'); //hola perro casa-> hola;perro;casa

            Numero = Convert.ToInt32(datos[0]);
            Precio = Convert.ToDecimal(datos[1]);
            Nombre = datos[2];
        }

        public string GenerarRegistro()
        {
            return $"{Numero};{Precio};{Nombre}";
        }
        public override string ToString()
        {
            return $"{Numero}{Precio}{Nombre}";
        }

    }
}
