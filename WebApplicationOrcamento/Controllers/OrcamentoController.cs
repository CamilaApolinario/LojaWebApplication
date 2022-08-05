using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Model;
using WebApplicationOrcamento.Service.Service;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        private readonly BaseService<Produto> _serviceProduto;
        private readonly BaseService<Vendedor> _serviceVendedor;


        public OrcamentoController(ILogger<OrcamentoController> logger,
                                   OrcamentoService orcamentoService,
                                   ApplicationContext context,
                                   IUrlHelper urlHelper,
                                   BaseService<Produto> serviceProduto,
                                   BaseService<Vendedor> serviceVendedor
                                   )
        {
            _logger = logger;
            _orcamentoService = orcamentoService;
            _context = context;
            _urlHelper = urlHelper;
            _serviceProduto = serviceProduto;
            _serviceVendedor = serviceVendedor;
        }

        [HttpGet(Name = nameof(MostraTodosOrcamento))]
        public async Task<ActionResult<ColecaoRecursos<Orcamento>>> MostraTodosOrcamento()
        {
            var orcamento = _orcamentoService.GetOrcamento();
            orcamento.ForEach(c => GerarLinks(c));

            var resultado = new ColecaoRecursos<Orcamento>(orcamento);
            resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(MostraTodosOrcamento), new { }), rel: "self", metodo: "GET"));
            resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(AdicionaOrcamento), new { }), rel: "create-cliente", metodo: "POST"));

            return resultado;
        }

        [HttpPost("private")]
        public ActionResult AdicionaOrcamento([FromBody] OrcamentoRequest orcamentoRequest)
        {
            _logger.LogInformation("Start inserting Orçamentos");

            var produtos = _serviceProduto.GetById(orcamentoRequest.IdProduto);
            var quantidadeProduto = orcamentoRequest.QuantidadeProduto;
            var random = new Random();
            var vendedores = _serviceVendedor.Get();

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
            var orcamento = _orcamentoService.GetOrcamento(request.Id);       

            if (orcamento != null && orcamento.Produto != null)
            {
                _orcamentoService.UpdateOrcamento(request);
                
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
            return Ok(_orcamentoService.DeletaOrcamento(id));        
        }
       
    }
}


