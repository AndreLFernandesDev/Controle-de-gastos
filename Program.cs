﻿using System.Globalization;
using dotenv.net;
using Dominios;
class Financas
{
    private static async Task Main()
    {
        DotEnv.Load();
        int id = 0;
        id = Convert.ToInt32(null);
        string nome = ObterNome();
        decimal salario = ObterSalario();
        decimal meta = ObterMeta();
        await Db.AddUsuario(id, nome, salario, meta);
        string? escolha;
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
                    var despesa = await Db.RetornarDespesa();
                    for (int i = 0; i < despesa.Count; i++)
                    {
                        Despesa x = despesa[i];
                        Console.WriteLine("Id: {0} | Nome: {1} | Valor:R$ {2} | Data de vencimento: {3} | Situação: {4} | Categoria: {5}",
                        x.IdDespesa, x.NomeDespesa, x.ValorDespesa, x.DataDespesa, x.SituacaoDespesa, x.Categoria);
                    }

                    Console.WriteLine("");
                    break;
                case "2":
                    Console.WriteLine("LISTA DE RECEITAS");
                    Console.WriteLine("-----------------");
                    var receita = await Db.RetornarReceita();
                    for (int i = 0; i < receita.Count; i++)
                    {
                        Receita x = receita[i];
                        Console.WriteLine("Id: {0} | Nome: {1} | Valor:R$ {2} | Data recebimento: {3} | Situação: {4} | Categoria: {5}",
                         x.IdReceita, x.NomeReceita, x.ValorReceita, x.DataReceita, x.SituacaoReceita, x.Categoria);

                    }
                    Console.WriteLine("");
                    break;
                case "3":


                    //Adicionar despesa
                    string nomeDespesa = ObterNomeDespesa();
                    decimal valorDespesa = ObterValorDespesa();
                    DateTime dataDespesa = ObterDataDespesa();
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
                    string categoria = ObterCategoria();
                    await Db.AddDespesa(id, nomeDespesa, valorDespesa, dataDespesa, situacao, categoria);
                    break;
                case "4":

                    //Adicionar receita
                    string nomeReceita = ObterNomeReceita();
                    decimal valorReceita = ObterValorReceita();
                    DateTime dataReceita = ObterDataReceita();
                    string situacaoReceita = ObterSituacaoReceita();
                    Console.WriteLine("Digite o número correspondente a categoria da receita:");
                    Console.WriteLine("1- Presentes");
                    Console.WriteLine("2- Prêmios");
                    Console.WriteLine("3- Reembolso");
                    Console.WriteLine("4- Rendimentos");
                    Console.WriteLine("5- Salário");
                    Console.WriteLine("6- Outros");
                    string categoriaReceita = ObterCategoriaReceita();
                    await Db.AddReceita(nomeReceita, valorReceita, dataReceita, situacaoReceita, categoriaReceita);
                    Console.WriteLine("");
                    break;

                case "5":
                    //await DivisaoDespesas();
                    Console.WriteLine("O valor total de despesas é R$ {0}", await TotalDespesas());
                    Console.WriteLine("O valor total de receitas é R$ {0}", await TotalReceitas());
                    Console.WriteLine("O saldo restante após a soma de todas as receitas e subtraido as despesas é de: R${0}", await Restante());
                    Console.WriteLine("Sua meta de gastos foi: {0}", await MetaSituation());
                    Console.WriteLine("");
                    break;

                case "6":
                    Console.WriteLine("Digite o ID da despesa que deseja remover:");
                    RemoverDespesa();
                    Console.WriteLine("");
                    break;

                case "7":
                    Console.WriteLine("Digite o ID da receita que deseja remover:");
                    RemoverReceita();
                    Console.WriteLine("");
                    break;

            }
        } while (escolha != "0");

    }
    public static string ObterNome()
    {
        string? nome;
        do
        {
            Console.WriteLine("Digite seu nome:");
            nome = Console.ReadLine();
        } while (nome == null);
        return nome;
    }
    public static decimal ObterSalario()
    {
        bool? deuCerto;
        decimal salario;
        do
        {
            Console.WriteLine("Digite o valor do seu salário:");
            deuCerto = decimal.TryParse(Console.ReadLine(), out salario);
        } while (deuCerto == null);
        return salario;
    }
    public static decimal ObterMeta()
    {
        bool? deuCerto;
        decimal meta;
        do
        {
            Console.WriteLine("Digite sua meta de gastos:");
            deuCerto = decimal.TryParse(Console.ReadLine(), out meta);
        } while (deuCerto == null);
        return meta;
    }
    public static string ObterNomeDespesa()
    {
        string? nome;
        do
        {
            Console.WriteLine("Digite o nome da despesa:");
            nome = Console.ReadLine();
        } while (nome == null);
        return nome;
    }
    public static decimal ObterValorDespesa()
    {
        decimal valor;
        bool? deuCerto;
        do
        {
            Console.WriteLine("Digite o valor da despesa:");
            deuCerto = decimal.TryParse(Console.ReadLine(), out valor);
        } while (deuCerto == null);
        return valor;
    }
    public static DateTime ObterDataDespesa()
    {
        DateTime dataDespesa;
        bool? deuCerto;
        do
        {
            Console.WriteLine("Digite a data de vencimento da despesa: dd/mm/aaaa");
            CultureInfo provider = new("pt-BR");
            deuCerto = DateTime.TryParse(Console.ReadLine(), provider, out dataDespesa);
        } while (deuCerto == null);
        return dataDespesa;
    }
    public static string ObterCategoria()
    {
        int categoria = Convert.ToInt32(Console.ReadLine());
        string retorno = Convert.ToString((Despesa.CategoriaDespesa)categoria);
        return retorno;

    }
    public static string ObterNomeReceita()
    {
        string? nome;
        do
        {
            Console.WriteLine("Digite o nome da receita:");
            nome = Console.ReadLine();
        } while (nome == null);
        return nome;
    }
    public static decimal ObterValorReceita()
    {
        decimal valor;
        bool? deuCerto;
        do
        {
            Console.WriteLine("Digite o valor da receita:");
            deuCerto = decimal.TryParse(Console.ReadLine(), out valor);
        } while (deuCerto == null);
        return valor;
    }
    public static DateTime ObterDataReceita()
    {
        DateTime dataReceita;
        bool? deuCerto;
        do
        {
            Console.WriteLine("Digite a data do recebimento do valor: dd/mm/aaaa");
            CultureInfo provider = new("pt-BR");
            deuCerto = DateTime.TryParse(Console.ReadLine(), provider, out dataReceita);
        } while (deuCerto == null);
        return dataReceita;
    }
    public static string ObterCategoriaReceita()
    {
        int categoria = Convert.ToInt32(Console.ReadLine());
        string retorno = Convert.ToString((Receita.CategoriaReceita)categoria);
        return retorno;

    }
    public static async Task<decimal> TotalDespesas()
    {
        decimal soma = 0;
        var listaDespesas = await Db.RetornarDespesa();
        for (int i = 0; i < listaDespesas.Count; i++)
        {
            var valorDespesa = listaDespesas[i].ValorDespesa;
            soma += valorDespesa;
        }
        return soma;
    }
    public static async Task<decimal> TotalReceitas()
    {
        decimal soma = 0;
        var listaReceitas = await Db.RetornarReceita();
        for (int i = 0; i < listaReceitas.Count; i++)
        {
            soma += listaReceitas[i].ValorReceita;
        }
        return soma;
    }
    public static async Task<decimal> Restante()
    {
        var listaUsuarios = await Db.RetornarUsuarios();
        decimal restante = 0;
        for (int i = 0; i < listaUsuarios.Count; i++)
        {
            decimal despesas = await TotalDespesas();
            decimal receitas = await TotalReceitas();
            restante = listaUsuarios[i].ValorSalario + receitas - despesas;
        }
        return restante;
    }
    public static async Task<string> MetaSituation()
    {
        decimal meta = 0;
        decimal despesas = await TotalDespesas();
        var listaUsuarios = await Db.RetornarUsuarios();
        for (int i = 0; i < listaUsuarios.Count; i++)
        {
            meta = listaUsuarios[i].MetaGastos;
        }
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
    public static string ObterSituacaoDespesa()
    {
        bool deuCerto;
        string retorno;
        int situacao; ;
        do
        {
            Console.WriteLine("Digite o número referente a situaçao da despesa: ");
            Console.WriteLine("1- Paga");
            Console.WriteLine("2-Não paga");
            deuCerto = int.TryParse(Console.ReadLine(), out situacao);
            if (situacao == 1)
            {
                retorno = "Paga";
            }
            else { retorno = "Não paga"; }
        } while (situacao != 1 && situacao != 2);
        return retorno;
    }
    public static string ObterSituacaoReceita()
    {
        string retorno;
        int situacaoReceita;
        bool deuCerto;
        do
        {
            Console.WriteLine("Digite o número referente a situaçãp da receita:");
            Console.WriteLine("1- Recebida");
            Console.WriteLine("2- Não recebida");
            deuCerto = int.TryParse(Console.ReadLine(), out situacaoReceita);

            if (situacaoReceita == 1)
            {
                retorno = "Recebida";
            }
            else { retorno = "Não recebida"; }
        } while (situacaoReceita != 1 && situacaoReceita != 2);
        return retorno;
    }
    public static async void RemoverDespesa()
    {
        int id = Convert.ToInt32(Console.ReadLine());
        await Db.DeletarDespesa(id);
    }

    public static async void RemoverReceita()
    {
        int id = Convert.ToInt32(Console.ReadLine());
        await Db.DeletarReceita(id);
    }
    // public static async Task DivisaoDespesas()
    // {
    //     var chamada = await Db.ValoresDespesas();
    //     Task<decimal> somaLazer;
    //     Task<decimal> somaAcademia;
    //     Task<decimal> somaBeleza;
    //     Task<decimal> somaInternet;
    //     Task<decimal> somaMoradia;
    //     Task<decimal> somaSaude;
    //     Task<decimal> somaSupermercado;
    //     Task<decimal> somaTransporte;
    //     Task<decimal> somaOutros;
    //     Task<decimal> somaTelefone;

    //     for (int i = 0; i < chamada.Count; i++)
    //     {
    //         Task<List<Despesa>>despesas=await Db.RetornarDespesa();
    //         if (despesas[i]==Despesa.CategoriaDespesa.Lazer)
    //         {
    //             Task<decimal> valor =Despesa.;
    //             somaLazer += valor;
    //         }

    //         else if (.Despesas[i].Categoria == Despesa.CategoriaDespesa.Academia)
    //         {
    //             Task<decimal> valor =despesas[i].ValorDespesa;
    //             somaAcademia += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Beleza)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaBeleza += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Internet)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaInternet += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Moradia)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaMoradia += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Saúde)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaSaude += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Supermercado)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaSupermercado += valor;

    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Transporte)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaTransporte += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Outros)
    //         {
    //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
    //             Task<somaOutros> += valor;
    //         }
    //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Telefone)
    //         {
    //             Task<decimal> valor = novoUsuario.Despesas[i].ValorDespesa;
    //             somaTelefone += valor;
    //         }
    //         Console.WriteLine("DIVISÃO DESPESAS (%)");
    //         Console.WriteLine("____________________");
    //         Console.WriteLine("Lazer-{0} %", somaLazer / TotalDespesas() * 100);
    //         Console.WriteLine("Academia-{0} %", somaAcademia / TotalDespesas() * 100);
    //         Console.WriteLine("Beleza-{0} %", somaBeleza / TotalDespesas() * 100);
    //         Console.WriteLine("Internet-{0} %", somaInternet / TotalDespesas() * 100);
    //         Console.WriteLine("Moradia-{0} %", somaMoradia / TotalDespesas() * 100);
    //         Console.WriteLine("Saúde-{0} %", somaSaude / TotalDespesas() * 100);
    //         Console.WriteLine("Supermercado-{0} %", somaSupermercado / TotalDespesas() * 100);
    //         Console.WriteLine("Transporte-{0} %", somaTransporte / TotalDespesas() * 100);
    //         Console.WriteLine("Telefone-{0} %", somaTelefone / TotalDespesas() * 100);
    //         Console.WriteLine("Outros-{0} %", somaOutros / TotalDespesas() * 100);
    //         Console.WriteLine("");
    //     }
    // }
    // public static async Task DivisaoReceitas()
    // {
    //     var receitas=await Db.RetornarReceita();
    //     Task<decimal> somaPresentes;
    //     Task<decimal> somaPremios;
    //     Task<decimal> somaReembolso;
    //     Task<decimal> somaRendimentos;
    //     Task<decimal> somaSalario;
    //     Task<decimal> somaOutros;


    //     for (int i = 0; i <receitas.Count; i++)
    //     {
    //         if (receitas[i]==Receita.CategoriaReceita.Presentes)
    //         {
    //             Task<decimal> valor = receitas[i].ValorReceita;
    //             somaPresentes += valor;
    //         }

    //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Prêmios)
    //         {
    //             Task<decimal> valor = novoUsuario.Receitas[i].ValorReceita;
    //             somaPremios += valor;
    //         }
    //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Reembolso)
    //         {
    //             Task<decimal> valor = novoUsuario.Receitas[i].ValorReceita;
    //             somaReembolso += valor;
    //         }
    //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Rendimentos)
    //         {
    //            Task<decimal> valor = novoUsuario.Receitas[i].ValorReceita;
    //             somaRendimentos += valor;
    //         }
    //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Salário)
    //         {
    //               Task<decimal> valor = novoUsuario.Receitas[i].ValorReceita;
    //             somaSalario += valor;
    //         }
    //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Outros)
    //         {
    //               Task<decimal> valor = novoUsuario.Receitas[i].ValorReceita;
    //             somaOutros += valor;
    //         }

    //         Console.WriteLine("DIVISÃO RECEITAS (%)");
    //         Console.WriteLine("____________________");
    //         Console.WriteLine("Presentes-{0} %", somaPresentes / TotalReceitas() * 100);
    //         Console.WriteLine("Prêmios-{0} %", somaPremios / TotalReceitas() * 100);
    //         Console.WriteLine("Reembolso-{0} %", somaReembolso / TotalReceitas() * 100);
    //         Console.WriteLine("Rendimentos-{0} %", somaRendimentos / TotalReceitas() * 100);
    //         Console.WriteLine("Salário-{0} %", somaSalario / TotalReceitas() * 100);
    //         Console.WriteLine("Outros-{0} %", somaOutros / TotalReceitas() * 100);
    //         Console.WriteLine("");
    //    }
    //}
}