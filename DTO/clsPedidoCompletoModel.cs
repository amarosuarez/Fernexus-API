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
        public int IdPedido { get; set; }
        public List<clsProductoCompletoModel> Productos { get; set; }
        public double CosteTotal { get; set; }
        public DateTime FechaPedido { get; set; }
        #endregion

        #region Constructores
        public clsPedidoCompletoModel() { }
        public clsPedidoCompletoModel(int idPedido)
        {
            this.IdPedido = idPedido;
        }
        public clsPedidoCompletoModel(int idPedido, List<clsProductoCompletoModel> productos, double costeTotal, DateTime fechaPedido) : this(idPedido)
        {
            this.Productos = productos;
            this.CosteTotal = costeTotal;
            this.FechaPedido = fechaPedido;
        }
        #endregion
    }
}
