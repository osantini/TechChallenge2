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
        public IActionResult BuscaNoticias()
        {
            var token = TokenManager.GenerateToken();
            var tokenName = TokenManager.ValidateToken(token);

            if (tokenName == "postech")
            {
                Noticia result = new Noticia();
                List<Noticia> resultFinal = new List<Noticia>();

                SqlConnection connection = Connection.OpenConnectionSql();
                connection.Open();

                string sqlQuery = "select * from Noticia";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    resultFinal.Add(new Noticia()
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
}
