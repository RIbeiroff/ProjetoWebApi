using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.WebAPI.Model;
using Projeto.WebAPI.Services;

namespace Projeto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ICompraService _compraService;
        private readonly IMapper _mapper;


        public ProdutosController(IProdutoService produtoService,
                                 ICompraService compraService,
                                 IMapper mapper)
        {
            this._produtoService = produtoService;
            this._compraService = compraService;
            this._mapper = mapper;
        }

        // GET api/produtos/
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var result = await _produtoService.GetProdutosComEstoque();
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }

        // GET api/produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var produto = await _produtoService.GetProdutoPorId(id);
                var model = _mapper.Map<ProdutoDetalhado>(produto);

                if (model != null)
                {
                    var ultimaCompra = await _compraService.UltimaCompraPorProdutoId(produto.Id);

                    if (ultimaCompra != null)
                    {
                        model.valorUltimaVenda = ultimaCompra.Valor;
                        model.dataUltimaVenda = ultimaCompra.Data_Transacao.ToString();
                    }
                }

                return Ok(model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }

        // POST api/produtos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _produtoService.InserirProduto(produto);
                    return Ok(result);
                }
                catch (System.Exception)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
                }
            }
            return this.StatusCode(StatusCodes.Status412PreconditionFailed, "Os valores informados não são válidos");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _produtoService.RemoveProduto(id);
                if (result)
                    return Ok("Produto removido com sucesso");
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            } catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            }
        }
    }
}