using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsProducto
    {
        #region Propiedades
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int IdCategoria { get; set; }
        #endregion

        #region Constructores
        public clsProducto() { }

        public clsProducto(int idProducto)
        {
            IdProducto = idProducto;
        }

        public clsProducto(string nombre, double precio, int idCategoria)
        {
            this.Nombre = nombre;
            this.Precio = precio;
            this.IdCategoria = idCategoria;
        }
        #endregion
    }
}
