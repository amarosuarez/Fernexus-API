using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class clsManejadoraPedidosDAL
    {
        /// <summary>
        /// Función que recibe una lista de productos completos y la fecha de pedido y crea el pedido
        /// </summary>
        /// <param name="listaProductosCompleto">Listado de Productos completos</param>
        /// <param name="fechaPedido">Fecha del pedido</param>
        /// <returns>Número de filas afectadas</returns>
        public int crearPedidoDAL(List<clsProductoCompleto> listaProductosCompleto, String fechaPedido)
        {
            int numFilasAfectadas = 0;

            return numFilasAfectadas;
        }
    }
}
