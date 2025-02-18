using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsPedidosProducto
    {
        #region Propiedades
        public int IdPedidio { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        #endregion

        #region Contructores
        public clsPedidosProducto() { }

        public clsPedidosProducto(int IdPedidio, int IdProducto)
        {
            this.IdPedidio = IdPedidio;
            this.IdProducto = IdProducto;
        }

        public clsPedidosProducto(int IdPedidio, int IdProducto, int Cantidad)
        {
            this.IdPedidio = IdPedidio;
            this.IdProducto = IdProducto;
            this.Cantidad = Cantidad;
        }
        #endregion
    }
}
