using Microsoft.AspNetCore.Mvc;
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
                                   BaseService<Vendedor> serviceVendedor)
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

            var produtos = _serviceProduto.GetByName(orcamentoRequest.NomeProduto);
            var quantidadeProduto = orcamentoRequest.QuantidadeProduto;
            var random = new Random(0-1000);
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
            var orcamento = _orcamentoService.GetOrcamento(id);       

            if (orcamento != null && orcamento.Produto != null)
            {
                _orcamentoService.UpdateOrcamento(id, request);                
                return Ok(orcamento);
            }
            return NotFound($"Orçamento de id {id}, não encontrado!");
        }

        [HttpPatch("{id}")]
        public ActionResult AtualizaQuantOrcamento(int id, [FromBody] int quantidade)
        {
            var orcamento = _orcamentoService.GetOrcamento(id);
            
            if (orcamento != null && orcamento.Produto != null)
            {
                _orcamentoService.AtualizaQuantidadeOrcamento(id, quantidade);
                return Ok(orcamento);
            }
            return NotFound($"Orçamento de id {id}, não encontrado!");
        }

        [HttpDelete]
        public IActionResult DeleteOrcamento(int id)
        {
            var orcamento = _orcamentoService.GetOrcamento(id);
            if (orcamento != null)
            {
                return Ok(_orcamentoService.DeletaOrcamento(id));
            }
            return NotFound($"Orcamento de id: {id} não encontrado!");
        }       
    }
}


