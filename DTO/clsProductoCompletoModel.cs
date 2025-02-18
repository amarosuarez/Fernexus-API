namespace DTO
{
    public class clsProductoCompletoModel
    {
        #region Propiedades
        int idProducto { get; set; }
        int idProovedor { get; set; }
        string nombre { get; set; }
		double precioUd { get; set; }
        int cantidad { get; set; }
        double precioTotal { get; set; }
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
