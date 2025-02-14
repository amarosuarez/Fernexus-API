using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENT
{
    public class clsProovedor
    {
        #region Propiedades
        int IdProovedor { get; set; }
        string Nombre { get; set; }
        string Correo { get; set; }
        string Telefono { get; set; }
        string Direccion { get; set; }
        string Pais { get; set; }
        #endregion

        #region Constructores
        public clsProovedor() { }

        public clsProovedor(int IdProovedor)
        {
            this.IdProovedor = IdProovedor;
        }

        public clsProovedor(string Nombre, string Correo, string Telefono, string Direccion, string Pais)
        {
            this.Nombre = Nombre;
            this.Correo = Correo;
            this.Telefono = Telefono;
            this.Direccion = Direccion;
            this.Pais = Pais;
        }
        #endregion
    }
}
