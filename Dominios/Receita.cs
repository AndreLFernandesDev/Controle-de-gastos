namespace Dominios
{
    class Receita
    {
        public int IdUsuario { get; set; }
        public int IdReceita { get; set; }
        public string NomeReceita { get; set; }
        public decimal ValorReceita { get; set; }
        public DateTime DataReceita { get; set; }
        public string CategoriaReceita { get; set; }
        public string SituacaoReceita { get; set; }
        public Receita(int idUsuario, int idReceita, string nomeReceita, decimal valorReceita, DateTime dataReceita, string situacaoReceita, string categoriaReceita)
        {
            IdUsuario = idUsuario;
            IdReceita = idReceita;
            NomeReceita = nomeReceita;
            ValorReceita = valorReceita;
            DataReceita = dataReceita;
            SituacaoReceita = situacaoReceita;
            CategoriaReceita = categoriaReceita;

        }


    }
}