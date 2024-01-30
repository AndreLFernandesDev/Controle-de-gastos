namespace Dominios
{
    using System.Data;
    using System.Runtime.CompilerServices;
    using dotenv.net;
    using MySqlConnector;
    class Db
    {
        private static readonly MySqlConnectionStringBuilder builder;
        static Db()
        {
            var envVars = DotEnv.Read();
            builder = new()
            {
                Server = envVars["DB_SERVER"],
                Database = envVars["DB_NAME"],
                UserID = envVars["DB_USER"],
                Password = envVars["DB_PASSWORD"],
            };
        }
        public static async Task AddUsuario(int id, string nome, decimal valor, decimal meta)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "INSERT INTO Usuario (id_usuario,nome,valor_salario,meta_gastos) VALUES (@id,@nome,@valor,@meta);";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@meta", meta);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task<List<Despesa>> ImprimirDespesa()
        {
            List<Despesa> ListaDespesa = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT*FROM Despesa;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Despesa novaDespesa = new(reader.GetString(0), reader.GetDecimal(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4));
                        ListaDespesa.Add(novaDespesa);
                    }
                }
                return ListaDespesa;
            }
        }
        public static async Task<List<Receita>> ListarReceita()
        {
            List<Receita> ListaReceita = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT *FROM Receita";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Receita novaReceita = new(reader.GetString(0), reader.GetDecimal(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4));
                        ListaReceita.Add(novaReceita);
                    }
                }
                return ListaReceita;
            }
        }
        public static async Task AddDespesa(string nome, decimal valor, DateTime data, string situacao, string categoria)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "INSERT INTO Despesa(nome_despesa,valor_despesa,data_despesa,categoria,situacao)VALUES(@nome,@valor,@data,@categoria,@situacao);";
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task AddReceita(string nome, decimal valor, DateTime data, string situacao, string categoria)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "INSERT INTO Receita(receita_nome,receita_valor,receita_data,receita_categoria,receita_situacao)VALUES (@nome,@valor,@data,@situacao,@categoria);";
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task<List<decimal>> ValoresDespesas()
        {
            List<decimal> ValoresDespesas = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT valor_despesa FROM Despesa;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        decimal valorDespesa = new(reader.GetDouble(0));
                        ValoresDespesas.Add(valorDespesa);
                    }
                }
            }
            return ValoresDespesas;
        }
        public static async Task<List<decimal>> ValoresReceitas()
        {
            List<decimal> ValoresReceitas = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT receita_valor FROM Receita;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        decimal valorReceita = new(reader.GetDouble(0));
                        ValoresReceitas.Add(valorReceita);
                    }
                }
            }
            return ValoresReceitas;
        }
        public static async Task<decimal> RetornarSalario()
        {
            decimal valorSalario = 0;
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT valor_salario FROM Usuario;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        valorSalario = new(reader.GetDouble(0));
                    }
                }
            }
            return valorSalario;
        }
        public static async Task<decimal> RetonarMeta()
        {
            decimal meta = 0;
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT meta_gastos FROM Usuario;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        meta = new(reader.GetDouble(0));
                    }
                }
            }
            return meta;
        }
    }
}
