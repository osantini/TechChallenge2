using System.Data.SqlClient;

namespace TechChallenge2.Services
{
    public class Connection
    {
        public static SqlConnection OpenConnectionSql()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conn = configuration.GetConnectionString("InformacoesDataContext");

            SqlConnection connection = new SqlConnection(conn);

            return connection;
        }
    }
}
