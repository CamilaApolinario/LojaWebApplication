using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;
using WebApplicationOrcamento.Service;
using WebApplicationOrcamento.Service.Service;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _baseService;

        public VendedorController(IVendedorService baseService)
        {
            _baseService = baseService;  
        }

        [HttpGet]
        public IActionResult MostraTodosVendedor()
        {
            return Ok(_baseService.BuscarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult MostraVendedorId(int id)
        {
            var vendedor = _baseService.BuscarPorId(id);
            if (vendedor != null)
            {
                var vendedorResponse = _baseService.CalculaComissao(vendedor);
                return Ok(vendedorResponse);
            }
            return NotFound($"Vendedor com id: {id}, não existe");
        }

        [HttpPost]
        public IActionResult AdicionaVendedor([FromBody] Vendedor vendedor)
        {
            if (vendedor != null)
            {
                _baseService.Adicionar(vendedor);
                return Ok($"Vwndedor {vendedor} foi adicionado!");
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult AtualizaVendedor(int id, [FromBody] string nome)
        {
            var vendedor = _baseService.BuscarPorId(id);
            if (vendedor != null)
            {
                vendedor.Nome = nome;
                _baseService.Atualizar(vendedor);               
                return Ok($"Vendedor {vendedor} foi atualizado!");
            }
            return NotFound($"Vendedor {id} não encontrado!");          
        }

        [HttpDelete]
        public IActionResult DeletaVendedor([FromQuery] int id)
        {
            var vendedor = _baseService.BuscarPorId(id);
            if (vendedor == null)
            {
                return NotFound($"Vendedor {id} não encontrado!");

            }
            _baseService.Excluir(vendedor);
            return Ok($"Vendedor de id {id}, foi excluido!");
        }
    }
}
