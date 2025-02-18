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
        public int IdProovedor { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
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
