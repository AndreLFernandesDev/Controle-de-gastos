using System.Collections;
using System.Globalization;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
            Console.WriteLine("5- Divisão dos valores (%)");
            Console.WriteLine("6- Remover despesa");
            Console.WriteLine("7- Remover receita");
            Console.WriteLine("0- Sair");
            escolha = Console.ReadLine();
            switch (escolha)
            {
                case "1":
                    Console.WriteLine("LISTA DE DESPESAS");
                    Console.WriteLine("-----------------");
                    if (novoUsuario.Despesas.Count == 0)
                    {
                        Console.WriteLine("vazia");
                    }
                    for (int i = 0; i < novoUsuario.Despesas.Count; i++)
                    {
                        Despesa x = novoUsuario.Despesas[i];
                        int pos = i + 1;
                        Console.WriteLine("{0}-Nome: {1} | Valor:R$ {2} | Data: {3} | Categoria: {4} | Situação: {5}",
                        pos, x.NomeDespesa, x.ValorDespesa, x.DataDespesa, x.Categoria, x.SituacaoDespesa);
                    }
                    Console.WriteLine("");
                    break;
                case "2":
                    Console.WriteLine("LISTA DE RECEITAS");
                    Console.WriteLine("-----------------");
                    if (novoUsuario.Receitas.Count == 0)
                    {
                        Console.WriteLine("vazia");
                    }
                    for (int i = 0; i < novoUsuario.Receitas.Count; i++)
                    {
                        Receita x = novoUsuario.Receitas[i];
                        int pos = i + 1;
                        Console.WriteLine("{0}-Nome: {1} | Valor:R$ {2} | Data: {3} | Categoria: {4} | Situação: {5}",
                        pos, x.NomeReceita, x.ValorReceita, x.DataReceita, x.Categoria, x.SituacaoReceita);

                    }
                    Console.WriteLine("");
                    break;
                case "3":


                    //Adicionar despesa
                    Console.WriteLine("Digite o nome da despesa:");
                    string nomeDespesa = ObterNomeDespesa();
                    Console.WriteLine("Digite o valor da despesa:");
                    decimal valorDespesa = ObterValorDespesa();
                    Console.WriteLine("Digite a data de vencimento da despesa: dd/mm/aaaa");
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
                    Console.WriteLine("");
                    break;
                case "4":

                    //Adicionar receita
                    Console.WriteLine("Digite o nome da receita:");
                    string nomeReceita = ObterNomeReceita();
                    Console.WriteLine("Digite o valor da receita:");
                    decimal valorReceita = ObterValorReceita();
                    Console.WriteLine("Digite a data do recebimento do valor: dd/mm/aaaa");
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
                    Console.WriteLine("");
                    break;

                case "5":
                    DivisaoDespesas(novoUsuario);
                    Console.WriteLine("O valor total de gastos é  R$ {0}", TotalDespesas(novoUsuario));
                    Console.WriteLine("O valor total de receitas é R$ {0}", TotalReceitas(novoUsuario));
                    Console.WriteLine("O saldo restante após a soma de todas as receitas e subtraindo as despesas é de: R${0}", Restante(novoUsuario, salario));
                    Console.WriteLine("Sua meta de gastos foi: {0}", MetaSituation(novoUsuario, meta));
                    Console.WriteLine("");
                    break;

                case "6":
                    Console.WriteLine("Digite o número da despesa que deseja remover:");
                    RemoverDespesa(novoUsuario);
                    Console.WriteLine("");
                    break;

                case "7":
                    Console.WriteLine("Digite o número da receita0 que deseja remover:");
                    RemoverReceita(novoUsuario);
                    Console.WriteLine("");
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
    public static decimal Restante(Usuario novoUsuario, decimal salario)
    {
        decimal despesas = TotalDespesas(novoUsuario);
        decimal receitas = TotalReceitas(novoUsuario);
        decimal restante = salario + receitas - despesas;
        return restante;
    }
    public static string MetaSituation(Usuario novoUsuario, decimal meta)
    {
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
    public static void RemoverDespesa(Usuario novoUsuario)
    {
        int pos = Convert.ToInt32(Console.ReadLine());
        novoUsuario.Despesas.RemoveAt(pos - 1);
        Console.WriteLine("Despesa removida!");

    }
    public static void RemoverReceita(Usuario novoUsuario)
    {
        int pos = Convert.ToInt32(Console.ReadLine());
        novoUsuario.Receitas.RemoveAt(pos - 1);
        Console.WriteLine("Receita removida!");

    }
    public static void DivisaoDespesas(Usuario novoUsuario)
    {
        decimal somaLazer = 0;
        decimal somaAcademia = 0;
        decimal somaBeleza = 0;
        decimal somaInternet = 0;
        decimal somaMoradia = 0;
        decimal somaSaude = 0;
        decimal somaSupermercado = 0;
        decimal somaTransporte = 0;
        decimal somaOutros = 0;
        decimal somaTelefone = 0;

        for (int i = 0; i < novoUsuario.Despesas.Count; i++)
        {
            if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Lazer)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaLazer += valor;
            }

            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Academia)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaAcademia += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Beleza)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaBeleza += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Internet)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaInternet += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Moradia)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaMoradia += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Saúde)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaSaude += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Supermercado)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaSupermercado += valor;

            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Transporte)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaTransporte += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Outros)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaOutros += valor;
            }
            else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Telefone)
            {
                decimal valor = novoUsuario.Despesas[i].ValorDespesa;
                somaTelefone += valor;
            }
            Console.WriteLine("DIVISÃO DESPESAS (%)");
            Console.WriteLine("____________________");
            Console.WriteLine("Lazer-{0} %", somaLazer / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Academia-{0} %", somaAcademia / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Beleza-{0} %", somaBeleza / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Internet-{0} %", somaInternet / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Moradia-{0} %", somaMoradia / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Saúde-{0} %", somaSaude / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Supermercado-{0} %", somaSupermercado / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Transporte-{0} %", somaTransporte / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Telefone-{0} %", somaTelefone / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("Outros-{0} %", somaOutros / TotalDespesas(novoUsuario) * 100);
            Console.WriteLine("");
        }
    }
    public static void DivisaoReceitas(Usuario novoUsuario)
    {
        decimal somaPresentes = 0;
        decimal somaPremios = 0;
        decimal somaReembolso = 0;
        decimal somaRendimentos = 0;
        decimal somaSalario = 0;
        decimal somaOutros = 0;


        for (int i = 0; i < novoUsuario.Receitas.Count; i++)
        {
            if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Presentes)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaPresentes += valor;
            }

            else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Prêmios)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaPremios += valor;
            }
            else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Reembolso)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaReembolso += valor;
            }
            else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Rendimentos)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaRendimentos += valor;
            }
            else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Salário)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaSalario += valor;
            }
            else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Outros)
            {
                decimal valor = novoUsuario.Receitas[i].ValorReceita;
                somaOutros += valor;
            }

            Console.WriteLine("DIVISÃO RECEITAS (%)");
            Console.WriteLine("____________________");
            Console.WriteLine("Presentes-{0} %", somaPresentes / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("Prêmios-{0} %", somaPremios / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("Reembolso-{0} %", somaReembolso / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("Rendimentos-{0} %", somaRendimentos / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("Salário-{0} %", somaSalario / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("Outros-{0} %", somaOutros / TotalReceitas(novoUsuario) * 100);
            Console.WriteLine("");
        }
    }
}