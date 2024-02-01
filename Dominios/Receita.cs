namespace Dominios
{
    class Receita
    {
        public int IdReceita { get; set; }
        public string NomeReceita { get; set; }
        public decimal ValorReceita { get; set; }
        public DateTime DataReceita { get; set; }
        public string Categoria { get; set; }
        public string SituacaoReceita { get; set; }
        public enum CategoriaReceita { Presentes = 1, Prêmios, Reembolso, Rendimentos, Salário, Outros };
        public Receita(int idReceita, string nomeReceita, decimal valorReceita, DateTime dataReceita, string categoriaReceita, string situacaoReceita)
        {
            this.IdReceita = idReceita;
            this.NomeReceita = nomeReceita;
            this.ValorReceita = valorReceita;
            this.DataReceita = dataReceita;
            this.SituacaoReceita = situacaoReceita;
            this.Categoria = categoriaReceita;

        }


    }
}