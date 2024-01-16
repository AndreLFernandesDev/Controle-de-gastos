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
    }
}