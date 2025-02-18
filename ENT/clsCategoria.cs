namespace ENT
{
    public class clsCategoria
    {
        #region Propiedades
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
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
