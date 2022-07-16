
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ApplicationContext _context;

        public ProdutoController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduto()
        {
            var produto = _context.Produto;
            return Ok(produto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {

            var produto = _context.Produto.FirstOrDefault(x => x.Id == id);
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
                _context.Add(produto);
                _context.SaveChanges();
                return Ok(produto);
            }
            return NotFound($"Produto: {produto} não foi adicionado");
        }

        [HttpPut]
        public IActionResult AlteraProduto(int id, [FromBody] Produto novoProduto)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Id == id);
            var nome = novoProduto.Nome;
            var valor = novoProduto.Valor;
            novoProduto.Valor = valor;
            novoProduto.Nome = nome;
            _context.Remove(produto);
            _context.Produto.Update(novoProduto);
            _context.SaveChanges();
            return Ok(novoProduto);
        }



        [HttpDelete]
        public IActionResult DeletaProduto([FromQuery] string nome)
        {
            var produto = _context.Produto.FirstOrDefault(x => x.Nome == nome);
            if (produto == null)
            {
                return NotFound($"Produto {produto} não encontrado!");

            }
            _context.Remove(produto);
            _context.SaveChanges();
            return Ok();
        }
    }
}
