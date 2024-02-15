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
        public static async Task<List<Usuario>> RetornarUsuarios()
        {
            List<Usuario> ListaUsuarios = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT *FROM Usuario;";
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Usuario novoUsuario = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDecimal(3));
                        ListaUsuarios.Add(novoUsuario);
                    }
                }
                return ListaUsuarios;
            }
        }
        public static async Task<List<Despesa>> RetornarDespesa()
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
                        Despesa novaDespesa = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDateTime(3), reader.GetString(4), reader.GetString(5));
                        ListaDespesa.Add(novaDespesa);
                    }
                }
                return ListaDespesa;
            }
        }
        public static async Task<List<Receita>> RetornarReceita()
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
                        Receita novaReceita = new(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDateTime(3), reader.GetString(4), reader.GetString(5));
                        ListaReceita.Add(novaReceita);
                    }
                }
                return ListaReceita;
            }
        }
        public static async Task AddDespesa(int id, string nome, decimal valor, DateTime data, string situacao, string categoria)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "INSERT INTO Despesa(nome_despesa,valor_despesa,data_despesa,categoria_despesa,situacao_despesa)VALUES(@nome,@valor,@data,@categoria,@situacao);";
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
                    command.CommandText = "INSERT INTO Receita(nome_receita,valor_receita,data_receita,categoria_receita,situacao_receita)VALUES (@nome,@valor,@data,@situacao,@categoria);";
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public static async Task<int> DeletarDespesa(int id)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "DELETE FROM Despesa WHERE id_despesa=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    int rowCount = await command.ExecuteNonQueryAsync();
                    return rowCount;
                }
            }

        }
        public static async Task<int> DeletarReceita(int id)
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "DELETE FROM Receita WHERE id_receita=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    int rowCount = await command.ExecuteNonQueryAsync();
                    return rowCount;
                }
            }

        }
    }
}
