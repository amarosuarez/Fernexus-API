using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class clsPedidoCompletoModel
    {
        #region Propiedades 
        int idPedido { get; set; }
        List<clsProductoCompletoModel> productos { get; set; }
        double costeTotal { get; set; }
        string fechaPedido { get; set; }
        #endregion

        #region Constructores
        public clsPedidoCompletoModel() { }
        public clsPedidoCompletoModel(int idPedido)
        {
            this.idPedido = idPedido;
        }
        public clsPedidoCompletoModel(int idPedido, List<clsProductoCompletoModel> productos, double costeTotal, string fechaPedido) : this(idPedido)
        {
            this.productos = productos;
            this.costeTotal = costeTotal;
            this.fechaPedido = fechaPedido;
        }
        #endregion
    }
}
