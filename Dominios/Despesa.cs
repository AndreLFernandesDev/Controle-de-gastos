namespace Dominios
{
    class Despesa
    {
        public string NomeDespesa { get; set; }
        public decimal ValorDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public string Categoria { get; set; }
        public string SituacaoDespesa { get; set; }
        public enum CategoriaDespesa
        {
            Lazer = 1, Transporte, Moradia, Saúde, Internet, Academia, Telefone, Supermercado, Beleza,
            Outros
        };
        public Despesa(string nomeDespesa, decimal valorDespesa, DateTime dataDespesa, string situacaoDespesa, string categoria)
        {
            this.NomeDespesa = nomeDespesa;
            this.ValorDespesa = valorDespesa;
            this.DataDespesa = dataDespesa;
            this.SituacaoDespesa = situacaoDespesa;
            this.Categoria = categoria;
        }
    }
}