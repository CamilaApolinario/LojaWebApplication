using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _baseService;

        public ProdutoController(IProdutoService baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> MostraTodosProdutos()
        {
            return Ok(_baseService.BuscarTodos()); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> MostraProdutoId(int id)
        {
            var produto = _baseService.BuscarPorId(id);
            
            if (produto != null)
            {
                return Ok(produto);
            }
            return NotFound($"Produto de id: {id}, não existe!");
        }

        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] Produto produto)
        {
            if (produto != null)
            {
                _baseService.Adicionar(produto);
                return Ok($"Produto {produto} foi adicionado com sucesso");
            }
            return NotFound($"Produto: {produto} não foi adicionado");
        }

        [HttpPut]
        public IActionResult AtualizaProduto(int id, [FromBody] ProdutoUpdate novoProduto)
        {
            var produto = _baseService.BuscarPorId(id);
            if (produto != null)
            {
                produto.Nome = novoProduto.Nome;
                produto.Valor = novoProduto.Valor;
                _baseService.Atualizar(produto);
                return Ok($"Produto {novoProduto} atualizado com sucesso!");
            }
            return NotFound($"Produto de Id: {id} não encontrado!");
        }

        [HttpDelete]
        public IActionResult DeletaProduto([FromQuery] int id)
        {
            var produto = _baseService.BuscarPorId(id);
            if (produto == null)
            {
                return NotFound($"Produto de Id: {id} não encontrado!");
            }

            _baseService.Excluir(produto);
            return Ok($"Produto de id: {id} foi excluido!");
        }
    }
}
