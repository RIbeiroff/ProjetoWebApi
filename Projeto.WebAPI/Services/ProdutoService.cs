using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto.WebAPI.Data;
using Projeto.WebAPI.Model;
using System.Linq;

namespace Projeto.WebAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private DataContext _repositorio;

        public ProdutoService(DataContext repositorio){
            this._repositorio = repositorio;
        }
    
        public async Task<int> InserirProduto(Produto produto){
            await _repositorio.Produtos.AddAsync(produto);
            return await _repositorio.SaveChangesAsync();
        }

        public async Task<List<Produto>> GetTodosProdutos(){
            return await _repositorio.Produtos.ToListAsync();
        }
        
        public async Task<List<Produto>> GetProdutosComEstoque(){
            var query = from p in _repositorio.Produtos
                        where p.Qtde_Estoque > 0
                        select p;
            return await query.ToListAsync();
        }

        public async Task<Produto> GetProdutoPorId(int id){
            return await _repositorio.Produtos.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateProduto(Produto produto){
            var model = await GetProdutoPorId(produto.Id);
            if (model != null){
                model = produto;
                await _repositorio.SaveChangesAsync();
            }
        }

        public async Task<bool> RemoveProduto(int produtoId){
            var model = await GetProdutoPorId(produtoId);
            if (model != null){
                _repositorio.Produtos.Remove(model);
                await _repositorio.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}