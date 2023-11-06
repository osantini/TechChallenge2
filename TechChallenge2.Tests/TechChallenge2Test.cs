using Microsoft.AspNetCore.Mvc;
using TechChallenge2.Controllers;
using TechChallenge2.Models;

namespace TechChallenge2.Tests
{
    public class TechChallenge2Test
    {
        [Fact]
        public void BuscarNoticias()
        {
            var Noticias = new NoticiasController();
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InBvc3RlY2giLCJuYmYiOjE2OTkyODIwOTgsImV4cCI6MTY5OTM2ODQ5OCwiaWF0IjoxNjk5MjgyMDk4fQ.tETxibaCQC2A4IH6KP1EHosc6nPum_g_GK8Zlov4J08";

            var resultado = Noticias.BuscaNoticias(token);

            var viewResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(200, viewResult.StatusCode);
        }

        [Fact]
        public void GerarToken()
        {
            var Token = new TokenController();

            var resultado = Token.GerarToken();

            var viewResult = Assert.IsType<String>(resultado);
            Assert.Equal(resultado, viewResult);
        }

        [Fact]
        public void SalvarToken()
        {
            bool resultado = TokenController.SalvarToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InBvc3RlY2giLCJuYmYiOjE2OTkyODIwOTgsImV4cCI6MTY5OTM2ODQ5OCwiaWF0IjoxNjk5MjgyMDk4fQ.tETxibaCQC2A4IH6KP1EHosc6nPum_g_GK8Zlov4J08");

            Assert.True(resultado);
        }
    }
}