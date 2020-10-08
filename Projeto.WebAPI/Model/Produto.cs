using System;
using Newtonsoft.Json;

namespace Projeto.WebAPI.Model
{
    public class Produto
    {
        public int Id { get; set; }   
        public string Nome { get; set; }
        public double Valor_Unitario { get; set; }
        public int Qtde_Estoque { get; set; }

    }
}