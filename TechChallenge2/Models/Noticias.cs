﻿namespace TechChallenge2.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Autor { get; set; }
    }
}
