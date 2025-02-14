namespace ENT
{
    public class clsCategoria
    {
        #region Propiedades
        int IdCategoria { get; set; }
        string Nombre { get; set; }
        #endregion

        #region Constructores
        public clsCategoria() { }

        public clsCategoria(int idCategoria)
        {
            IdCategoria = idCategoria;
        }

        public clsCategoria(int idCategoria, string nombre)
        {
            this.IdCategoria = idCategoria;
            this.Nombre = nombre;
        }
        #endregion
    }
}
