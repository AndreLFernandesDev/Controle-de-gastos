using System.Globalization;
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
        Usuario novoUsuario = new(nome, salario, meta);
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
                    var despesa = await Db.ImprimirDespesa();
                    for (int i = 0; i < despesa.Count; i++)
                    {
                        Despesa x = despesa[i];
                        Console.WriteLine("Nome: {0} | Valor:R$ {1} | Data de vencimento: {2} | Situação: {3} | Categoria: {4}",
                        x.NomeDespesa, x.ValorDespesa, x.DataDespesa, x.SituacaoDespesa, x.Categoria);
                    }

                    Console.WriteLine("");
                    break;
                case "2":
                    Console.WriteLine("LISTA DE RECEITAS");
                    Console.WriteLine("-----------------");
                    var receita = await Db.ListarReceita();
                    for (int i = 0; i < receita.Count; i++)
                    {
                        Receita x = receita[i];
                        Console.WriteLine("Nome: {0} | Valor:R$ {1} | Data recebimento: {2} | Situação: {3} | Categoria: {4}",
                         x.NomeReceita, x.ValorReceita, x.DataReceita, x.SituacaoReceita, x.Categoria);

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
                    await Db.AddDespesa(nomeDespesa, valorDespesa, dataDespesa, situacao, categoria);
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
                    await DivisaoDespesas();
                    Console.WriteLine("O valor total de despesas é R$ {0}", await TotalDespesas());
                    Console.WriteLine("O valor total de receitas é R$ {0}", await TotalReceitas());
                    Console.WriteLine("O saldo restante após a soma de todas as receitas e subtraido as despesas é de: R${0}", await Restante());
                    Console.WriteLine("Sua meta de gastos foi: {0}", await MetaSituation());
                    Console.WriteLine("");
                    break;

                case "6":
                    Console.WriteLine("Digite o número da despesa que deseja remover:");
                    RemoverDespesa(novoUsuario);
                    Console.WriteLine("");
                    break;

                case "7":
                    Console.WriteLine("Digite o número da receita que deseja remover:");
                    RemoverReceita(novoUsuario);
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
        var listaValores = await Db.ValoresDespesas();
        for (int i = 0; i < listaValores.Count; i++)
        {
            soma += listaValores[i];
        }
        return soma;
    }
    public static async Task<decimal> TotalReceitas()
    {
        decimal soma = 0;
        var listaReceitas = await Db.ValoresReceitas();
        for (int i = 0; i < listaReceitas.Count; i++)
        {
            soma += listaReceitas[i];
        }
        return soma;
    }
    public static async Task<decimal> Restante()
    {
        decimal salario = await Db.RetornarSalario();
        decimal despesas = await TotalDespesas();
        decimal receitas = await TotalReceitas();
        decimal restante = salario + receitas - despesas;
        return restante;
    }
    public static async Task<string> MetaSituation()
    {
        decimal despesas = await TotalDespesas();
        decimal meta = await Db.RetonarMeta();
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
    public static void RemoverDespesa(Usuario novoUsuario)
    {
        if (novoUsuario.Despesas.Count == 0)
        {
            Console.WriteLine("Não há despesas para remover!");
        }
        else
        {
            int pos = Convert.ToInt32(Console.ReadLine());
            novoUsuario.Despesas.RemoveAt(pos - 1);
            Console.WriteLine("Despesa removida!");
        }
    }
    public static void RemoverReceita(Usuario novoUsuario)
    {
        if (novoUsuario.Receitas.Count == 0)
        {
            Console.WriteLine("Não há receitas para remover!");
        }
        else
        {
            int pos = Convert.ToInt32(Console.ReadLine());
            novoUsuario.Receitas.RemoveAt(pos - 1);
            Console.WriteLine("Receita removida!");
        }
    }
    public static async Task DivisaoDespesas()
    {
        var chamada = await Db.ValoresDespesas();
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

        //     for (int i = 0; i <chamada.Count; i++)
        //     {
        //         if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Lazer)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaLazer += valor;
        //         }

        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Academia)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaAcademia += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Beleza)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaBeleza += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Internet)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaInternet += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Moradia)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaMoradia += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Saúde)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaSaude += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Supermercado)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaSupermercado += valor;

        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Transporte)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaTransporte += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Outros)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaOutros += valor;
        //         }
        //         else if (novoUsuario.Despesas[i].Categoria == Despesa.CategoriaDespesa.Telefone)
        //         {
        //             decimal valor = novoUsuario.Despesas[i].ValorDespesa;
        //             somaTelefone += valor;
        //         }
        //         Console.WriteLine("DIVISÃO DESPESAS (%)");
        //         Console.WriteLine("____________________");
        //         Console.WriteLine("Lazer-{0} %", somaLazer / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Academia-{0} %", somaAcademia / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Beleza-{0} %", somaBeleza / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Internet-{0} %", somaInternet / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Moradia-{0} %", somaMoradia / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Saúde-{0} %", somaSaude / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Supermercado-{0} %", somaSupermercado / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Transporte-{0} %", somaTransporte / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Telefone-{0} %", somaTelefone / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("Outros-{0} %", somaOutros / TotalDespesas(novoUsuario) * 100);
        //         Console.WriteLine("");
        //     }
        // }
        // public static void DivisaoReceitas(Usuario novoUsuario)
        // {
        //     decimal somaPresentes = 0;
        //     decimal somaPremios = 0;
        //     decimal somaReembolso = 0;
        //     decimal somaRendimentos = 0;
        //     decimal somaSalario = 0;
        //     decimal somaOutros = 0;


        //     for (int i = 0; i < novoUsuario.Receitas.Count; i++)
        //     {
        //         if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Presentes)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaPresentes += valor;
        //         }

        //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Prêmios)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaPremios += valor;
        //         }
        //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Reembolso)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaReembolso += valor;
        //         }
        //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Rendimentos)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaRendimentos += valor;
        //         }
        //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Salário)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaSalario += valor;
        //         }
        //         else if (novoUsuario.Receitas[i].Categoria == Receita.CategoriaReceita.Outros)
        //         {
        //             decimal valor = novoUsuario.Receitas[i].ValorReceita;
        //             somaOutros += valor;
        //         }

        //         Console.WriteLine("DIVISÃO RECEITAS (%)");
        //         Console.WriteLine("____________________");
        //         Console.WriteLine("Presentes-{0} %", somaPresentes / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("Prêmios-{0} %", somaPremios / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("Reembolso-{0} %", somaReembolso / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("Rendimentos-{0} %", somaRendimentos / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("Salário-{0} %", somaSalario / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("Outros-{0} %", somaOutros / TotalReceitas(novoUsuario) * 100);
        //         Console.WriteLine("");
        //     }
    }
}