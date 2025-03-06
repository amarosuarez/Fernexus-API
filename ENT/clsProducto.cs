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

        public int Stock { get; set; }

        public int IdCategoria { get; set; }

        public clsCategoria Categoria { get; set; }
        
        public List<clsCategoria> Categorias { get; set; }
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

        public clsProducto(string nombre, double precio, int idCategoria, clsCategoria categoria) : this(nombre, precio, idCategoria)
        {
            this.Categoria = categoria;
        }

        public clsProducto(string nombre, double precio, int idCategoria, List<clsCategoria> categorias) : this(nombre, precio, idCategoria)
        {
            this.Categorias = categorias;
        }

        public clsProducto(string nombre, double precio, int idCategoria, int stock) : this(nombre, precio, idCategoria)
        {
            this.Stock = stock;
        }
        #endregion
    }
}
