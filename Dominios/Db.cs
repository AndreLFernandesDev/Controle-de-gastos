namespace Dominios
{
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
        public static async Task AddDespesa(string nome, decimal valor, DateTime data, string situacao, Despesa.CategoriaDespesa categoria)
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
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}