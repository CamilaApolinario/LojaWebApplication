using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrcamentoController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly OrcamentoService _orcamentoService;
        private readonly ILogger<OrcamentoController> _logger;

        public OrcamentoController(ILogger<OrcamentoController> logger,
                                   OrcamentoService orcamentoService,
                                   ApplicationContext context)
        {
            _logger = logger;
            _orcamentoService = orcamentoService;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> MostraTodosOrcamento()
        {
            var orcamento = _context.Orcamento;
            return Ok(orcamento);
        }

        [HttpPost]
        public ActionResult AdicionaOrcamento([FromBody] OrcamentoRequest orcamentoRequest)
        {
            _logger.LogInformation("Start inserting Orçamentos");

            var produtos = _context.Produto.FirstOrDefault(x => x.Nome == orcamentoRequest.NomeProduto);
            var quantidadeProduto = orcamentoRequest.QuantidadeProduto;
            var random = new Random();
            var vendedores = _context.Vendedor.ToList();

            if (produtos != null)
            {
                var orcamento = _orcamentoService.AdicionaOrcamento(produtos, vendedores[random.Next(vendedores.Count - 1)], quantidadeProduto);
                if (orcamento != null)
                {
                    _context.Add(orcamento);
                    _context.SaveChanges();
                    _logger.LogInformation("Success inserting Orçamentos");

                    return Ok(orcamento);
                }
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult AtualizaOrcamento(int id, [FromBody] UpdateOrcamentoRequest orcamentoRequest)
        {
            var orcamento = _context.Orcamento
                .Include(p => p.Produto)
                .FirstOrDefault(x=> x.Id == id);
            var produto = _context.Produto.FirstOrDefault(x => x.Nome == orcamentoRequest.Produto.Nome);
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Nome == orcamentoRequest.Vendedor.Nome);

            if (orcamento != null && orcamento.Produto != null)
            {
                var valorTotal = orcamentoRequest.Quantidade * produto.Valor;
                orcamento.ValorTotal = valorTotal;
                orcamento.QuantidadeProduto = orcamentoRequest.Quantidade;
                orcamento.Produto = produto;
                orcamento.Vendedor = vendedor;
                _context.Orcamento.Update(orcamento);
                _context.SaveChanges();
                return Ok(orcamento);
            }
            return BadRequest();      
        }

        [HttpDelete]
        public IActionResult DeleteOrcamento(int id)
        {
            var orcamento = _context.Orcamento.FirstOrDefault(x=>x.Id == id);
            _context.Orcamento.Remove(orcamento);
            _context.SaveChanges();
            return Ok();

        }
    }
}

