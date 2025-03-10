﻿using ENT;

namespace DTO
{
    public class clsProductoCompletoModel
    {
        #region Propiedades
        public int idProducto { get; set; }
        public clsProveedor proveedor { get; set; }
        public string nombre { get; set; }
        public double precioUd { get; set; }
        public int cantidad { get; set; }
        public List<clsCategoria> categorias { get; set; }

        #endregion

        #region Constructores
        public clsProductoCompletoModel() { }

        public clsProductoCompletoModel(int idProducto,clsProveedor clsProveedor)
        {
            this.idProducto = idProducto;
            this.proveedor = clsProveedor;
        }

        public clsProductoCompletoModel(int idProducto, clsProveedor clsProveedor, string nombre, double precioUd, int cantidad, List<clsCategoria> categorias) : this(idProducto, clsProveedor)
        {
            this.nombre = nombre;
            this.precioUd = precioUd;
            this.cantidad = cantidad;
            this.categorias = categorias;
            this.proveedor = clsProveedor;
        }

        #endregion
        override
        public bool Equals(object? obj)
        {
            clsProductoCompletoModel prod = (clsProductoCompletoModel) obj;

            return prod.idProducto == this.idProducto;
        }
    }
}
