using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrcamentoController : ControllerBase
    {

        private void GerarLinks(Orcamento orcamento)
        {
            orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(MostraTodosOrcamento), new { id = orcamento.Id }), rel: "self", metodo: "GET"));

            orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(AtualizaOrcamento), new { id = orcamento.Id }), rel: "update-cliente", metodo: "PUT"));

            orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(DeleteOrcamento), new { id = orcamento.Id }), rel: "delete-cliente", metodo: "DELETE"));

        }

        private readonly ApplicationContext _context;
        private readonly OrcamentoService _orcamentoService;
        private readonly ILogger<OrcamentoController> _logger;
        private readonly IUrlHelper _urlHelper;

        public OrcamentoController(ILogger<OrcamentoController> logger,
                                   OrcamentoService orcamentoService,
                                   ApplicationContext context,
                                   IUrlHelper urlHelper)
        {
            _logger = logger;
            _orcamentoService = orcamentoService;
            _context = context;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(MostraTodosOrcamento))]
        [AllowAnonymous]
        public async Task<ActionResult<ColecaoRecursos<Orcamento>>> MostraTodosOrcamento()
        {
            var orcamento = await _context.Orcamento
                .Include(p=> p.Produto)
                .Include(v=> v.Vendedor)
                .ToListAsync();
            orcamento.ForEach(c => GerarLinks(c));

            var resultado = new ColecaoRecursos<Orcamento>(orcamento);
            resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(MostraTodosOrcamento), new { }), rel: "self", metodo: "GET"));
            resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(AdicionaOrcamento), new { }), rel: "create-cliente", metodo: "POST"));

            return resultado;
        }

        [HttpPost("private")]
        [AllowAnonymous]
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
                    
                    _logger.LogInformation("Success inserting Orçamentos");

                    return Ok(orcamento);
                }
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult AtualizaOrcamento(int id, [FromBody] UpdateOrcamentoRequest request)
        {           
            var orcamento = _context.Orcamento
                .Include(p => p.Produto)
                .FirstOrDefault(x => x.Id == id);
            var produto = _context.Produto.FirstOrDefault(x => x.Nome == request.Produto.Nome);
            var vendedor = _context.Vendedor.FirstOrDefault(x => x.Nome == request.Vendedor.Nome);

            if (orcamento != null && orcamento.Produto != null)
            {
                var valorTotal = request.Quantidade * produto.Valor;
                orcamento.ValorTotal = valorTotal;
                orcamento.QuantidadeProduto = request.Quantidade;
                orcamento.Produto = produto;
                orcamento.Vendedor = vendedor;
                _context.Orcamento.Update(orcamento);
                _context.SaveChanges();
                return Ok(orcamento);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public ActionResult AtualizaQuantOrcamento(int id, [FromBody] int quantidade)
        {
            var orcamento = _context.Orcamento
                .Include(p => p.Produto)
                .FirstOrDefault(x => x.Id == id);
            
            if (orcamento != null && orcamento.Produto != null)
            {
                var valorTotal = quantidade * orcamento.Produto.Valor;
                orcamento.ValorTotal = valorTotal;
                orcamento.QuantidadeProduto = quantidade;
                
                _context.Orcamento.Update(orcamento);
                _context.SaveChanges();
                return Ok(orcamento);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteOrcamento(int id)
        {
            var orcamento = _context.Orcamento.FirstOrDefault(x => x.Id == id);
            _context.Orcamento.Remove(orcamento);
            _context.SaveChanges();
            return Ok();
        }
       
    }
}


