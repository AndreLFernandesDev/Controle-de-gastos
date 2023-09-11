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
        Usuario novoUsuario2 = new("maycon", 1000, 500);

        //Adicionar despesas
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
        Despesa novaDespesa2 = new("aluguel", 50, dataDespesa, situacao, categoria);
        novoUsuario.Despesas.Add(novaDespesa2);

        //Adicionar receita
        Console.WriteLine("Digite o nome da receita:");
        string nomeReceita = ObterNomeReceita();
        Console.WriteLine("Digite o valor da receita:");
        decimal valorReceita = ObterValorReceita();
        Console.WriteLine("Digite a data do recebimento do valor:");
        DateTime dataReceita = ObterDataReceita();
        Console.WriteLine("Digite o número correspondente a categoria da receita:");
        Console.WriteLine("1- Presentes");
        Console.WriteLine("2- Prêmios");
        Console.WriteLine("3- Reembolso");
        Console.WriteLine("4- Rendimentos");
        Console.WriteLine("5- Salário");
        Console.WriteLine("6- Outros");
        Receita.CategoriaReceita categoriaReceita = ObterCategoriaReceita();
        Receita novaReceita = new(nomeReceita, valorReceita, dataReceita, categoriaReceita);
        novoUsuario.Receitas.Add(novaReceita);
        Receita novaReceita2 = new("presentes", 10, dataReceita, categoriaReceita);
        novoUsuario.Receitas.Add(novaReceita2);

        Console.WriteLine(novoUsuario.NomeUsuario);
        Console.WriteLine(novoUsuario.ValorSalario);
        Console.WriteLine(novoUsuario.MetaGastos);
        Console.WriteLine(novaDespesa.NomeDespesa);
        Console.WriteLine(novaDespesa.ValorDespesa);
        Console.WriteLine(novaDespesa.DataDespesa);
        Console.WriteLine(novaDespesa.Categoria);

        Console.WriteLine(novaReceita.NomeReceita);
        Console.WriteLine(novaReceita.ValorReceita);
        Console.WriteLine(novaReceita.DataReceita);
        Console.WriteLine(novaReceita.Categoria);

        Console.WriteLine(novoUsuario2.NomeUsuario);
        Console.WriteLine(novoUsuario2.ValorSalario);
        Console.WriteLine(novoUsuario2.MetaGastos);
        Console.WriteLine(novaDespesa2.NomeDespesa);
        Console.WriteLine(novaDespesa2.ValorDespesa);
        Console.WriteLine(novaDespesa2.DataDespesa);
        Console.WriteLine(novaDespesa2.Categoria);

        Console.WriteLine(novaReceita2.NomeReceita);
        Console.WriteLine(novaReceita2.ValorReceita);
        Console.WriteLine(novaReceita2.DataReceita);
        Console.WriteLine(novaReceita2.Categoria);
        Console.WriteLine("O valor total das despesas é {0}", TotalDespesas(novoUsuario));
        Console.WriteLine("O valor total das receitas é {0}", TotalReceitas(novoUsuario));
        Console.WriteLine("O valor restante é de: {0}", Restante(novoUsuario));
        Console.WriteLine("Situação da sua meta de gastos:" + MetaSituation(novoUsuario));
        Console.WriteLine("Situacão da despesa:" + situacao);
        Console.WriteLine("Categoria da despesa:" + ExibirCategoria(novoUsuario, nome));



    }
    public static string ObterNome()
    {
        string nome = "andre";
        return nome;
    }
    public static decimal ObterSalario()
    {
        //decimal salario = Convert.ToDecimal(Console.ReadLine());
        decimal salario = 2000;
        return salario;
    }
    public static decimal ObterMeta()
    {
        //decimal meta = Convert.ToDecimal(Console.ReadLine());
        decimal meta = 90;
        return meta;
    }
    public static string ObterNomeDespesa()
    {
        //string nome = Console.ReadLine();
        string nome = "internet";
        return nome;
    }
    public static decimal ObterValorDespesa()
    {
        //decimal valor = Convert.ToDecimal(Console.ReadLine());
        decimal valor = 50;
        return valor;
    }
    public static DateTime ObterDataDespesa()
    {
        //string dataDespesa = Console.ReadLine();
        string dataDespesa = "05/08/2023";
        CultureInfo provider = new("pt-BR");
        DateTime data = DateTime.Parse(dataDespesa, provider);
        return data;
    }
    public static Despesa.CategoriaDespesa ObterCategoria()
    {
        //int categoria = Convert.ToInt32(Console.ReadLine());
        int categoria = 7;
        return (Despesa.CategoriaDespesa)categoria;

    }
    public static string ObterNomeReceita()
    {
        //string nome = Console.ReadLine();
        string nome = "bonus";
        return nome;
    }
    public static decimal ObterValorReceita()
    {
        //decimal valor = Convert.ToInt32(Console.ReadLine());
        decimal valor = 100;
        return valor;
    }
    public static DateTime ObterDataReceita()
    {
        //string dataReceita = Console.ReadLine();
        string dataReceita = "25/07/2023";
        CultureInfo provider = new("pt-BR");
        DateTime data = DateTime.Parse(dataReceita, provider);
        return data;
    }
    public static Receita.CategoriaReceita ObterCategoriaReceita()
    {
        //int categoria = Convert.ToInt32(Console.ReadLine());
        int categoria = 2;
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
}