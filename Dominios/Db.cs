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

        public static async Task AddUsuario(
            int id,
            string email,
            string nome,
            decimal valor,
            decimal meta
        )
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText =
                        "INSERT INTO Usuario (id_usuario,email_usuario,nome,valor_salario,meta_gastos) VALUES (@id,@email,@nome,@valor,@meta);";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@meta", meta);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<Usuario?> BuscarUsuarioPorEmail(string email)
        {
            Usuario? novoUsuario = null;
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT *FROM Usuario WHERE email_usuario=@email;";
                    command.Parameters.AddWithValue("@email", email);
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        novoUsuario = new(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDecimal(3),
                            reader.GetDecimal(4)
                        );
                    }
                }
            }
            return novoUsuario;
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
                        Usuario novoUsuario =
                            new(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDecimal(3),
                                reader.GetDecimal(4)
                            );
                        ListaUsuarios.Add(novoUsuario);
                    }
                }
                return ListaUsuarios;
            }
        }

        public static async Task<List<Despesa>> RetornarDespesa(int idUsuario)
        {
            List<Despesa> ListaDespesa = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT*FROM Despesa WHERE id_usuario=@idUsuario;";
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Despesa novaDespesa =
                            new(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetDecimal(3),
                                reader.GetDateTime(4),
                                reader.GetString(5),
                                reader.GetString(6)
                            );
                        ListaDespesa.Add(novaDespesa);
                    }
                }
                return ListaDespesa;
            }
        }

        public static async Task<List<Receita>> RetornarReceita(int idUsuario)
        {
            List<Receita> ListaReceita = new();
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText = "SELECT *FROM Receita WHERE id_usuario=@idUsuario";
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Receita novaReceita =
                            new(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetDecimal(3),
                                reader.GetDateTime(4),
                                reader.GetString(5),
                                reader.GetString(6)
                            );
                        ListaReceita.Add(novaReceita);
                    }
                }
                return ListaReceita;
            }
        }

        public static async Task AddDespesa(
            int idUsuario,
            string nome,
            decimal valor,
            DateTime data,
            string situacao,
            string categoria
        )
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText =
                        "INSERT INTO Despesa(id_usuario,nome_despesa,valor_despesa,data_despesa,situacao_despesa,categoria_despesa)VALUES(@idUsuario,@nome,@valor,@data,@situacao,@categoria);";
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task AddReceita(
            int idUsuario,
            string nome,
            decimal valor,
            DateTime data,
            string situacao,
            string categoria
        )
        {
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText =
                        "INSERT INTO Receita(id_usuario,nome_receita,valor_receita,data_receita,situacao_receita,categoria_receita)VALUES (@idUsuario,@nome,@valor,@data,@situacao,@categoria);";
                    command.Parameters.AddWithValue("idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@valor", valor);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@situacao", situacao);
                    command.Parameters.AddWithValue("@categoria", categoria);
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<decimal> SomarDespesa(int id)
        {
            decimal soma = 0;
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText =
                        "SELECT SUM(valor_despesa) FROM Despesa WHERE id_usuario=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.IsDBNull(0))
                        {
                            return 0;
                        }
                        else
                        {
                            soma = new(reader.GetDouble(0));
                        }
                    }
                    return soma;
                }
            }
        }

        public static async Task<decimal> SomarReceita(int id)
        {
            decimal soma = 0;
            using var connection = new MySqlConnection(builder.ConnectionString);
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                {
                    command.CommandText =
                        "SELECT SUM(valor_receita) FROM Receita WHERE id_usuario=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    using var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        if (reader.IsDBNull(0))
                        {
                            return 0;
                        }
                        else
                        {
                            soma = reader.GetDecimal(0);
                        }
                    }
                    return soma;
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
