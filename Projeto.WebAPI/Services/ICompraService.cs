using System.Threading.Tasks;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Services
{
    public interface ICompraService
    {
        /// <summary>
        /// Solicitação de uma compra
        /// </summary>
        /// <param name="solicitacao">Solicitação</param>
        /// <returns>True se tiver estoque</returns>
        Task<bool> ValidarSolicitacao(Solicitacao solicitacao);

        /// <summary>
        /// Inserção de compra
        /// </summary>
        /// <param name="compra">Model Compra</param>
        /// <returns>1 se atualizou banco sem erros</returns>
        Task<int> InserirCompra(Compra compra);

        /// <summary>
        /// Processamento de compra
        /// </summary>
        /// <param name="compra">Model compra</param>
        /// <returns>1 se atualizou no banco sem erros</returns>
        Task<int> ProcessarCompra(Compra compra);

        /// <summary>
        /// Ultima compra determinado produto
        /// </summary>
        /// <param name="produtoId">Id do produto</param>
        /// <returns>Última compra</returns>
        Task<Compra> UltimaCompraPorProdutoId(int produtoId);
        
    }
}