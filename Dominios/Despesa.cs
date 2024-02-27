namespace Dominios
{
    class Despesa
    {
        public int IdUsuario { get; set; }
        public int IdDespesa { get; set; }
        public string NomeDespesa { get; set; }
        public decimal ValorDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public string SituacaoDespesa { get; set; }
        public string CategoriaDespesa { get; set; }
        public Despesa(int idUsuario, int idDespesa, string nomeDespesa, decimal valorDespesa, DateTime dataDespesa, string situacaoDespesa, string categoria)
        {
            IdUsuario = idUsuario;
            IdDespesa = idDespesa;
            NomeDespesa = nomeDespesa;
            ValorDespesa = valorDespesa;
            DataDespesa = dataDespesa;
            SituacaoDespesa = situacaoDespesa;
            CategoriaDespesa = categoria;
        }
    }
}