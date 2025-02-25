using ENT;

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
        public double precioTotal { get; set; }
        public clsCategoria categoria { get; set; }

        #endregion

        #region Constructores
        public clsProductoCompletoModel() { }

        public clsProductoCompletoModel(int idProducto,clsProveedor clsProveedor)
        {
            this.idProducto = idProducto;
            this.proveedor = clsProveedor;
        }

        public clsProductoCompletoModel(int idProducto, clsProveedor clsProveedor, string nombre, double precioUd, int cantidad, double precioTotal,clsCategoria categoria) : this(idProducto, clsProveedor)
        {
            this.nombre = nombre;
            this.precioUd = precioUd;
            this.cantidad = cantidad;
            this.precioTotal = precioTotal;
            this.categoria = categoria;
            this.proveedor = clsProveedor;
        }

        #endregion
    }
}
