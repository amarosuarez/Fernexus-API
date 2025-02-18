namespace DTO
{
    public class clsProductoCompletoModel
    {
        #region Propiedades
        public int idProducto { get; set; }
        public int idProovedor { get; set; }
        public string nombre { get; set; }
        public double precioUd { get; set; }
        public int cantidad { get; set; }
        public double precioTotal { get; set; }
        #endregion

        #region Constructores
        public clsProductoCompletoModel() { }

        public clsProductoCompletoModel(int idProducto, int idProovedor)
        {
            this.idProducto = idProducto;
            this.idProovedor = idProovedor;
        }

        public clsProductoCompletoModel(int idProducto, int idProovedor, string nombre, double precioUd, int cantidad, double precioTotal) : this(idProducto, idProovedor)
        {
            this.nombre = nombre;
            this.precioUd = precioUd;
            this.cantidad = cantidad;
            this.precioTotal = precioTotal;
        }

        #endregion
    }
}
