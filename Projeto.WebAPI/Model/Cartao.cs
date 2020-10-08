using System;

namespace Projeto.WebAPI.Model
{
    public class Cartao
    {
        public int Id { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public string Data_Expiracao { get; set; }
        public string Bandeira { get; set; }
        public string Cvv { get; set; }
    }
}