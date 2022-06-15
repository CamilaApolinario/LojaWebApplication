using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Data;

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

        [HttpPost]
        public async Task<ActionResult> Orcamento([FromQuery] OrcamentoRequest orcamentoRequest)
        {
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
                    return Ok(orcamento);
                }
            }
            return NotFound();
        }

    }
}

