using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrcamentoController : ControllerBase
    {

        private ApplicationContext _context;
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
        public ActionResult Orcamento([FromBody] OrcamentoRequest orcamentoRequest)
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
            var orcamento = _context.Orcamento.FirstOrDefault(x=> x.Id == id); 
            if(orcamentoRequest.Quantidade != null && orcamento.Produto != null)
            {
                var valorTotal = orcamentoRequest.Quantidade * orcamento.Produto.Valor;
                orcamento.ValorTotal = valorTotal;
                _context.Orcamento.Update(orcamento);
                _context.SaveChanges();
                return Ok(orcamento);
            }
            return BadRequest();      
        }
    }
}

