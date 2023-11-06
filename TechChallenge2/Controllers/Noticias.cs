using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TechChallenge2.Models;
using TechChallenge2.Services;

namespace TechChallenge2.Controllers
{
    [Route("api/busca-noticias")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        [HttpGet]
        public IActionResult BuscaNoticias(string token)
        {
            var tokenName = TokenManager.ValidateToken(token);

            if (tokenName == "postech")
            {
                NoticiaModel result = new NoticiaModel();
                List<NoticiaModel> resultFinal = new List<NoticiaModel>();

                SqlConnection connection = Connection.OpenConnectionSql();
                connection.Open();

                string sqlQuery = "select * from Noticia";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultFinal.Add(new NoticiaModel()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Titulo = reader["Titulo"].ToString(),
                        Conteudo = reader["Conteudo"].ToString(),
                        DataPublicacao = Convert.ToDateTime(reader["DataPublicacao"]),
                        Autor = reader["Autor"].ToString()
                    });
                }
                reader.Close();
                connection.Close();

                return Ok(resultFinal);
            }
            return Ok("Token Inválido");
        }
    }

    [Route("api/gerar-token")]
    [ApiController]

    public class TokenController : ControllerBase
    {
        [HttpGet]
        public string GerarToken()
        {
            try
            {
                var token = TokenManager.GenerateToken();
                bool tokenSalvo = TokenController.SalvarToken(token);

                return token;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static bool SalvarToken(string token)
        {
            try
            {
                TokenModel tokenResult = new TokenModel();
                SqlConnection connection = Connection.OpenConnectionSql();
                connection.Open();

                string insertQuery = "INSERT INTO Token (DataCriacao, DataValidade, Codigo, Token) VALUES (@DataCriacao, @DataValidade, @Codigo, @Token)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DataValidade", DateTime.Now.AddHours(1));
                    cmd.Parameters.AddWithValue("@Codigo", "postech");
                    cmd.Parameters.AddWithValue("@Token", token);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Inserção realizada com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Falha ao inserir os dados.");
                    }
                }


                return true;

            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
