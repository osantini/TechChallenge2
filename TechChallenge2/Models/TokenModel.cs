namespace TechChallenge2.Models
{
    public class TokenModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string Codigo { get; set; }
        public string Token { get; set; }

    }
}
