using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.WebAPI.Data;
using Projeto.WebAPI.Model;
using System.Linq;

namespace Projeto.WebAPI.Services
{
    public class CompraService : ICompraService
    {
        private DataContext _repositorio;
        private readonly IProdutoService _produtoService;
        

        public CompraService(DataContext repositorio,
                            IProdutoService produtoService){
            this._repositorio = repositorio;
            this._produtoService = produtoService;
        }

        public async Task<bool> ValidarSolicitacao(Solicitacao solicitacao){
            var produto = await _produtoService.GetProdutoPorId(solicitacao.Produto_Id);
            if (solicitacao.Qtde_Comprada <= produto.Qtde_Estoque){
                await _repositorio.Solicitacoes.AddAsync(solicitacao);
                return true;
            }
            return false;
        }

        public async Task<int> InserirCompra(Compra compra){
            await _repositorio.Compras.AddAsync(compra);
            return await _repositorio.SaveChangesAsync();
        }

        public async Task<int> ProcessarCompra(Compra compra){
            // Baixando estoque de produto
            var produto = await _produtoService.GetProdutoPorId(compra.Solicitacao.Produto_Id);
            produto.Qtde_Estoque -= compra.Solicitacao.Qtde_Comprada;
            await _produtoService.UpdateProduto(produto);

            // Armazenando compra
            await _repositorio.Compras.AddAsync(compra);
            return await _repositorio.SaveChangesAsync();
        }

        public async Task<Compra> UltimaCompraPorProdutoId(int produtoId){
            var query = from c in _repositorio.Compras
                        orderby c.Id descending
                        where c.Solicitacao.Produto_Id == produtoId
                        select c;
            return await query.FirstOrDefaultAsync();
        }


    }
}