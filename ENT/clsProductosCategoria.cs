using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsProductosCategoria
    {
        #region Propiedades
        int IdCategoria { get; set; }
        int IdProducto { get; set; }
        #endregion

        #region Constructores
        public clsProductosCategoria() { }
        public clsProductosCategoria(int IdCategoria, int IdProducto)
        {
            this.IdCategoria = IdCategoria;
            this.IdProducto = IdProducto;
        }
        #endregion
    }
}
