using System.Diagnostics.Metrics;
using System.Globalization;
using Dominios;

class Financas
{
    private static void Main()
    {

        Console.WriteLine("Digite seu nome:");
        string nome = ObterNome();
        Console.WriteLine("Digite o valor do seu salário:");
        decimal salario = ObterSalario();
        Console.WriteLine("Digite sua meta de gastos:");
        decimal meta = ObterMeta();
        Usuario novoUsuario = new(nome, salario, meta);
        string escolha;
        do
        {
            Console.WriteLine("Digite o número correspondente ao que deseja executar:");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("1- Imprimir lista de despesas");
            Console.WriteLine("2- Imprimir lista de receitas");
            Console.WriteLine("3- Adicionar despesa");
            Console.WriteLine("4- Adicionar receita");
            Console.WriteLine("5- Divisão de gastos");
            Console.WriteLine("0- Sair");
            escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    Console.WriteLine("LISTA DE DESPESAS");
                    Console.WriteLine("-----------------");
                    for (int i = 0; i < novoUsuario.Despesas.Count; i++)
                    {
                        Despesa x = novoUsuario.Despesas[i];
                        int pos = i + 1;
                        Console.WriteLine("{0}-Nome: {1} | Valor:{2} | Data: {3} | Categoria: {4} | Situação: {5}",
                        pos, x.NomeDespesa, x.ValorDespesa, x.DataDespesa, x.Categoria, x.SituacaoDespesa);
                    }
                    break;
                case "2":
                    Console.WriteLine("LISTA DE RECEITAS");
                    Console.WriteLine("-----------------");
                    for (int i = 0; i < novoUsuario.Despesas.Count; i++)
                    {
                        Receita x = novoUsuario.Receitas[i];
                        Console.WriteLine("Nome: {0} | Valor:{1} | Data: {2} | Categoria: {3} | Situação: {4}",
                        x.NomeReceita, x.ValorReceita, x.DataReceita, x.Categoria, x.SituacaoReceita);

                    }
                    break;
                case "3":


                    //Adicionar despesa
                    Console.WriteLine("Digite o nome da despesa:");
                    string nomeDespesa = ObterNomeDespesa();
                    Console.WriteLine("Digite o valor da despesa:");
                    decimal valorDespesa = ObterValorDespesa();
                    Console.WriteLine("Digite a data de vencimento da despesa:");
                    DateTime dataDespesa = ObterDataDespesa();
                    Console.WriteLine("Digite o número referente a situaçao da despesa: ");
                    Console.WriteLine("1- Paga");
                    Console.WriteLine("2-Não paga");
                    string situacao = ObterSituacaoDespesa();
                    Console.WriteLine("Digite o número correspondente a categoria da despesa:");
                    Console.WriteLine("1- Lazer");
                    Console.WriteLine("2- Transporte");
                    Console.WriteLine("3- Moradia");
                    Console.WriteLine("4- Saúde");
                    Console.WriteLine("5- Internet");
                    Console.WriteLine("6- Academia");
                    Console.WriteLine("7- Telefone");
                    Console.WriteLine("8-Supermercado");
                    Console.WriteLine("9-Beleza");
                    Console.WriteLine("10- Outros");
                    Despesa.CategoriaDespesa categoria = ObterCategoria();
                    Despesa novaDespesa = new(nomeDespesa, valorDespesa, dataDespesa, situacao, categoria);
                    novoUsuario.Despesas.Add(novaDespesa);
                    break;
                case "4":

                    //Adicionar receita
                    Console.WriteLine("Digite o nome da receita:");
                    string nomeReceita = ObterNomeReceita();
                    Console.WriteLine("Digite o valor da receita:");
                    decimal valorReceita = ObterValorReceita();
                    Console.WriteLine("Digite a data do recebimento do valor:");
                    DateTime dataReceita = ObterDataReceita();
                    Console.WriteLine("Digite o número referente a situaçãp da receita:");
                    Console.WriteLine("1- Recebida");
                    Console.WriteLine("2- Não recebida");
                    string situacaoRec = ObterSituacaoReceita();
                    Console.WriteLine("Digite o número correspondente a categoria da receita:");
                    Console.WriteLine("1- Presentes");
                    Console.WriteLine("2- Prêmios");
                    Console.WriteLine("3- Reembolso");
                    Console.WriteLine("4- Rendimentos");
                    Console.WriteLine("5- Salário");
                    Console.WriteLine("6- Outros");
                    Receita.CategoriaReceita categoriaReceita = ObterCategoriaReceita();
                    Receita novaReceita = new(nomeReceita, valorReceita, dataReceita, categoriaReceita, situacaoRec);
                    novoUsuario.Receitas.Add(novaReceita);
                    break;

            }
        } while (escolha != "0");

    }
    public static string ObterNome()
    {
        string nome = Console.ReadLine();
        return nome;
    }
    public static decimal ObterSalario()
    {
        decimal salario = Convert.ToDecimal(Console.ReadLine());
        return salario;
    }
    public static decimal ObterMeta()
    {
        decimal meta = Convert.ToDecimal(Console.ReadLine());
        return meta;
    }
    public static string ObterNomeDespesa()
    {
        string nome = Console.ReadLine();
        return nome;
    }
    public static decimal ObterValorDespesa()
    {
        decimal valor = Convert.ToDecimal(Console.ReadLine());
        return valor;
    }
    public static DateTime ObterDataDespesa()
    {
        string dataDespesa = Console.ReadLine();
        CultureInfo provider = new("pt-BR");
        DateTime data = DateTime.Parse(dataDespesa, provider);
        return data;
    }
    public static Despesa.CategoriaDespesa ObterCategoria()
    {
        int categoria = Convert.ToInt32(Console.ReadLine());
        return (Despesa.CategoriaDespesa)categoria;

    }
    public static string ObterNomeReceita()
    {
        string nome = Console.ReadLine();
        return nome;
    }
    public static decimal ObterValorReceita()
    {
        decimal valor = Convert.ToInt32(Console.ReadLine());
        return valor;
    }
    public static DateTime ObterDataReceita()
    {
        string dataReceita = Console.ReadLine();
        CultureInfo provider = new("pt-BR");
        DateTime data = DateTime.Parse(dataReceita, provider);
        return data;
    }
    public static Receita.CategoriaReceita ObterCategoriaReceita()
    {
        int categoria = Convert.ToInt32(Console.ReadLine());
        return (Receita.CategoriaReceita)categoria;

    }
    public static decimal TotalDespesas(Usuario novoUsuario)
    {
        decimal soma = 0;
        decimal valor = 0;
        for (int i = 0; i < novoUsuario.Despesas.Count; i++)
        {
            Despesa novaDespesa = novoUsuario.Despesas[i];
            valor = novaDespesa.ValorDespesa;
            soma += valor;
        }
        return soma;
    }
    public static decimal TotalReceitas(Usuario novoUsuario)
    {
        decimal soma = 0;
        decimal valor = 0;
        for (int i = 0; i < novoUsuario.Receitas.Count; i++)
        {
            Receita novaReceita = novoUsuario.Receitas[i];
            valor = novaReceita.ValorReceita;
            soma += valor;
        }
        return soma;
    }
    public static decimal Restante(Usuario novoUsuario)
    {

        decimal salario = ObterSalario();
        decimal despesas = TotalDespesas(novoUsuario);
        decimal receitas = TotalReceitas(novoUsuario);
        decimal restante = salario + receitas - despesas;
        return restante;
    }
    public static string MetaSituation(Usuario novoUsuario)
    {
        decimal meta = ObterMeta();
        decimal despesas = TotalDespesas(novoUsuario);
        string x;
        if (despesas < meta)
        {
            x = "respeitada!";
        }
        else
        {
            x = "ultrapassada!";
        }
        return x;
    }
    public static string ExibirCategoria(Usuario novoUsuario, string nome)
    {

        for (int i = 0; i < novoUsuario.Despesas.Count; i++)
        {

            Despesa x = novoUsuario.Despesas[i];
            nome = Convert.ToString(x.Categoria);
        }
        return nome;

    }
    public static string ObterSituacaoDespesa()
    {
        int situacao = Convert.ToInt32(Console.ReadLine());
        string retorno;
        if (situacao == 1)
        {
            retorno = "Paga";
        }
        else { retorno = "Não paga"; }
        return retorno;
    }
    public static string ObterSituacaoReceita()
    {
        int situacaoReceita = Convert.ToInt32(Console.ReadLine());
        string retorno;
        if (situacaoReceita == 1)
        {
            retorno = "Recebida";
        }
        else { retorno = "Não recebida"; }
        return retorno;
    }
}