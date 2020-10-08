using System;

namespace Projeto.WebAPI.Model
{
    public class ProdutoDetalhado : Produto
    {
        public double valorUltimaVenda { get; set; }

        public string dataUltimaVenda { get; set; }
        
    }
}