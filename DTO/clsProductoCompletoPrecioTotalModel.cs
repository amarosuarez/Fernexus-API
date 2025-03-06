using ENT;

namespace DTO
{
    public class clsProductoCompletoPrecioTotalModel
    {
        #region Propiedades
        public int idProducto { get; set; }
        public clsProveedor proveedor { get; set; }
        public string nombre { get; set; }
        public double precioUd { get; set; }
        public int cantidad { get; set; }
        public double precioTotal { get; set; }
        public List<clsCategoria> categorias { get; set; }

        #endregion

        #region Constructores
        public clsProductoCompletoPrecioTotalModel() { }

        public clsProductoCompletoPrecioTotalModel(int idProducto,clsProveedor clsProveedor)
        {
            this.idProducto = idProducto;
            this.proveedor = clsProveedor;
        }

        public clsProductoCompletoPrecioTotalModel(int idProducto, clsProveedor clsProveedor, string nombre, double precioUd, int cantidad, double precioTotal, List<clsCategoria> categorias) : this(idProducto, clsProveedor)
        {
            this.nombre = nombre;
            this.precioUd = precioUd;
            this.cantidad = cantidad;
            this.precioTotal = precioTotal;
            this.categorias = categorias;
            this.proveedor = clsProveedor;
        }

        #endregion
        override
        public bool Equals(object? obj)
        {
            clsProductoCompletoPrecioTotalModel prod = (clsProductoCompletoPrecioTotalModel) obj;

            return prod.idProducto == this.idProducto;
        }
    }
}
