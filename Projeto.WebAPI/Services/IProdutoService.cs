using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Services
{
    public interface IProdutoService
    {
        /// <summary>
        /// Inserção de produto
        /// </summary>
        /// <param name="produto">Model Produto</param>
        /// <returns>1 se atualizou banco sem erros</returns>
        Task<int> InserirProduto(Produto produto);

        /// <summary>
        /// Busca todos os produtos
        /// </summary>
        /// <returns>Lista de produtos</returns>
        Task<List<Produto>> GetTodosProdutos();

        /// <summary>
        /// Busca todos os produtos disponíveis para venda (estoque maior que zero)
        /// </summary>
        /// <returns>Lista de produtos</returns>
        Task<List<Produto>> GetProdutosComEstoque();

        /// <summary>
        /// Busca o produto pelo id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto</returns>
        Task<Produto> GetProdutoPorId(int id);

        /// <summary>
        /// Update produto
        /// </summary>
        /// <param name="produto">Model produto</param>
        /// <returns></returns>
        Task UpdateProduto(Produto produto);

        /// <summary>
        /// Remove um produto pelo seu id
        /// </summary>
        /// <param name="produtoId">Produto Id</param>
        /// <returns>True se removeu</returns>
        Task<bool> RemoveProduto(int produtoId);
    }
}