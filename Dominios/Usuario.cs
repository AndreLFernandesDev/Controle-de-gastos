namespace Dominios
{
    class Usuario
    {
        public int UsuarioId;
        public string NomeUsuario { get; set; }
        public decimal ValorSalario { get; set; }
        public decimal MetaGastos { get; set; }
        public List<Despesa> Despesas { get; set; } = new List<Despesa>();
        public List<Receita> Receitas { get; set; } = new List<Receita>();
        public Usuario(int usuarioId, string nomeUsuario, decimal valorSalario, decimal metaGastos)
        {
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            ValorSalario = valorSalario;
            MetaGastos = metaGastos;
        }
    }
}