namespace Dominios
{
    class Usuario
    {
        public string NomeUsuario { get; set; }
        public decimal ValorSalario { get; set; }
        public decimal MetaGastos { get; set; }
        public List<Despesa> Despesas { get; set; } = new List<Despesa>();
        public List<Receita> Receitas { get; set; } = new List<Receita>();
        public Usuario(string nomeUsuario, decimal valorSalario, decimal metaGastos)
        {
            this.NomeUsuario = nomeUsuario;
            this.ValorSalario = valorSalario;
            this.MetaGastos = metaGastos;
        }
    }
}