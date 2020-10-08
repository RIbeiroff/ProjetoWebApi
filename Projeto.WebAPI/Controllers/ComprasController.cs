using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.WebAPI.Model;
using Projeto.WebAPI.Services;

namespace Projeto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly IProdutoService _produtoService;

        public ComprasController(ICompraService compraService,
                                IProdutoService produtoService)
        {
            this._compraService = compraService;
            this._produtoService = produtoService;
        }

        // POST api/compras
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _compraService.ValidarSolicitacao(solicitacao);
                    if (result)
                        return await EfetuarTransacaoAsync(solicitacao);
                }
                catch (System.Exception)
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
                }
            }
            return this.StatusCode(StatusCodes.Status412PreconditionFailed, "Os valores informados não são válidos");
        }

        [NonAction]
        private async Task<IActionResult> EfetuarTransacaoAsync(Solicitacao solicitacao)
        {
            // Montando JSON da api
            var produto = await _produtoService.GetProdutoPorId(solicitacao.Produto_Id);
            var request = new
            {
                Valor = solicitacao.Qtde_Comprada * produto.Valor_Unitario,
                Cartao = solicitacao.Cartao
            };

            /*
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://sv-dev-01.pareazul.com.br:8080/");
                var response = client.PostAsJsonAsync("api/gateways/compras", request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var compra = new Compra()
                    {
                        Valor = request.Valor,
                        Data_Transacao = DateTime.Now,
                        Solicitacao = solicitacao
                    };

                    var result = await _compraService.ProcessarCompra(compra);
                    return Ok(result);
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
                }
            }
            return this.StatusCode(StatusCodes.Status400BadRequest, "Ocorreu um erro desconhecido");
            */

            var compra = new Compra()
            {
                Valor = request.Valor,
                Data_Transacao = DateTime.Now,
                Solicitacao = solicitacao
            };

            var result = await _compraService.ProcessarCompra(compra);
            return Ok(result);

        }
    }
}