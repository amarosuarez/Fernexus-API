using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsPedido
    {
        #region Propiedades
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public double Coste { get; set; }
        #endregion

        #region Constructores
        public clsPedido() { }

        public clsPedido(int IdPedido)
        {
            this.IdPedido = IdPedido;
        }

        public clsPedido(int IdPedido, DateTime FechaPedido, double Coste)
        {
            this.IdPedido = IdPedido;
            this.FechaPedido = FechaPedido;
            this.Coste = Coste;
        }
        #endregion
    }
}
