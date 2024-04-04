using System.Globalization;
using System.Security.Cryptography;
using Dominios;
using dotenv.net;

class Financas
{
    public static Usuario UsuarioAtual;

    private static async Task Main()
    {
        DotEnv.Load();
        int opcao;
        Console.WriteLine("Selecione uma opção e digite o número correspondente:");
        Console.WriteLine("1- Criar novo usuário");
        Console.WriteLine("2- Já sou usuário");
        opcao = Convert.ToInt32(Console.ReadLine());
        do
        {
            if (opcao == 1)
            {
                UsuarioAtual = await CriarUsuario();
            }
            else if (opcao == 2)
            {
                do
                {
                    Console.WriteLine("Digite o seu email de usuário:");
                    string email = Console.ReadLine();
                    UsuarioAtual = await Db.BuscarUsuarioPorEmail(email);
                    if (UsuarioAtual == null)
                    {
                        Console.WriteLine("Usuário inválido!");
                    }
                } while (UsuarioAtual == null);
            }
            else
            {
                Console.WriteLine("Opção inválida");
            }
        } while (opcao != 1 && opcao != 2);

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
                    var despesa = await Db.RetornarDespesa(UsuarioAtual.UsuarioId);
                    if (despesa.Count > 0)
                    {
                        for (int i = 0; i < despesa.Count; i++)
                        {
                            Despesa x = despesa[i];
                            Console.WriteLine(
                                "Id: {0} | Nome: {1} | Valor:R$ {2} | Data de vencimento: {3} | Situação: {4} | Categoria: {5}",
                                x.IdDespesa,
                                x.NomeDespesa,
                                x.ValorDespesa,
                                x.DataDespesa,
                                x.SituacaoDespesa,
                                x.CategoriaDespesa
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine("Lista vazia");
                    }

                    Console.WriteLine("");
                    break;
                case "2":
                    Console.WriteLine("LISTA DE RECEITAS");
                    Console.WriteLine("-----------------");
                    var receita = await Db.RetornarReceita(UsuarioAtual.UsuarioId);
                    if (receita.Count > 1)
                    {
                        for (int i = 0; i < receita.Count; i++)
                        {
                            Receita x = receita[i];
                            Console.WriteLine(
                                "Id: {0} | Nome: {1} | Valor:R$ {2} | Data recebimento: {3} | Situação: {4} | Categoria: {5}",
                                x.IdReceita,
                                x.NomeReceita,
                                x.ValorReceita,
                                x.DataReceita,
                                x.SituacaoReceita,
                                x.CategoriaReceita
                            );
                        }
                    }
                    else
                    {
                        Console.WriteLine("Lista vazia");
                    }
                    Console.WriteLine("");
                    break;
                case "3":

                    //Adicionar despesa
                    string nomeDespesa = ObterNomeDespesa();
                    decimal valorDespesa = ObterValorDespesa();
                    DateTime dataDespesa = ObterDataDespesa();
                    string situacao = ObterSituacaoDespesa();
                    Console.WriteLine("Digite o nome correspondente a categoria da despesa:");
                    string categoria = ObterCategoria();
                    await Db.AddDespesa(
                        UsuarioAtual.UsuarioId,
                        nomeDespesa,
                        valorDespesa,
                        dataDespesa,
                        situacao,
                        categoria
                    );
                    break;
                case "4":

                    //Adicionar receita
                    string nomeReceita = ObterNomeReceita();
                    decimal valorReceita = ObterValorReceita();
                    DateTime dataReceita = ObterDataReceita();
                    string situacaoReceita = ObterSituacaoReceita();
                    Console.WriteLine("Digite o nome correspondente a categoria da receita:");
                    string categoriaReceita = ObterCategoriaReceita();
                    await Db.AddReceita(
                        UsuarioAtual.UsuarioId,
                        nomeReceita,
                        valorReceita,
                        dataReceita,
                        situacaoReceita,
                        categoriaReceita
                    );
                    Console.WriteLine("");
                    break;

                case "5":
                    Console.WriteLine("O valor total de despesas é R$ {0}", await SomarDespesas());
                    Console.WriteLine("O valor total de receitas é R$ {0}", await SomarReceitas());
                    Console.WriteLine(
                        "O saldo restante após a soma de todas as receitas e subtraido as despesas é de: R${0}",
                        await Restante()
                    );
                    Console.WriteLine(
                        "Status da sua meta de gastos foi: {0}",
                        await MetaSituation()
                    );
                    await DivisaoDespesasPorCategoria();
                    await DivisaoReceitasPorCategoria();
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

    public static string ObterEmail()
    {
        Console.WriteLine("Digite seu email:");
        string email = Console.ReadLine();
        return email;
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

    public static async Task<Usuario> CriarUsuario()
    {
        int id = 0;
        string email = ObterEmail();
        string nome = ObterNome();
        decimal salario = ObterSalario();
        decimal meta = ObterMeta();
        await Db.AddUsuario(id, email, nome, salario, meta);
        Usuario? novoUsuario = await Db.BuscarUsuarioPorEmail(email);
        return novoUsuario;
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
        string retorno = Console.ReadLine();
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
        string retorno = Console.ReadLine();
        return retorno;
    }

    public static async Task<decimal> SomarDespesas()
    {
        decimal soma = await Db.SomarDespesa(UsuarioAtual.UsuarioId);
        return soma;
    }

    public static async Task<decimal> SomarReceitas()
    {
        decimal soma = await Db.SomarReceita(UsuarioAtual.UsuarioId);
        return soma;
    }

    public static async Task<decimal> Restante()
    {
        decimal despesas = await SomarDespesas();
        decimal receitas = await SomarReceitas();
        decimal restante = despesas - receitas;
        return restante;
    }

    public static async Task<string> MetaSituation()
    {
        var meta = UsuarioAtual.MetaGastos;
        decimal despesas = await SomarDespesas();
        string retorno;
        if (despesas <= meta)
        {
            retorno = "respeitada!";
        }
        else
        {
            retorno = "ultrapassada!";
        }
        return retorno;
    }

    public static string ObterSituacaoDespesa()
    {
        bool deuCerto;
        string retorno;
        int situacao;
        ;
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
            else
            {
                retorno = "Não paga";
            }
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
            else
            {
                retorno = "Não recebida";
            }
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

    public static async Task DivisaoDespesasPorCategoria()
    {
        Console.WriteLine("");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("DIVISÃO DESPESAS POR CATEGORIA:");
        var retorno = await Db.DivisaoDespesaPorCategoria(UsuarioAtual.UsuarioId);
        for (int i = 0; i < retorno.Count; i++)
        {
            Console.WriteLine(
                "Categoria: {0} Total={1} R$",
                retorno.Keys.ElementAt(i),
                retorno.Values.ElementAt(i)
            );
        }
    }

    public static async Task DivisaoReceitasPorCategoria()
    {
        Console.WriteLine("");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("DIVISÃO RECEITAS POR CATEGORIA:");
        var retorno = await Db.DivisaoReceitaPorCategoria(UsuarioAtual.UsuarioId);
        for (int i = 0; i < retorno.Count; i++)
        {
            Console.WriteLine(
                "Categoria: {0} Total={1} R$",
                retorno.Keys.ElementAt(i),
                retorno.Values.ElementAt(i)
            );
        }
    }
}
