using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrcamentoController : ControllerBase
    {

        //private void GerarLinks(Orcamento orcamento)
        //{
        //    orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(MostraTodosOrcamento), new { id = orcamento.Id }), rel: "self", metodo: "GET"));

        //    orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(AtualizaOrcamento), new { id = orcamento.Id }), rel: "update-cliente", metodo: "PUT"));

        //    orcamento.Links.Add(new LinkDTO(_urlHelper.Link(nameof(DeleteOrcamento), new { id = orcamento.Id }), rel: "delete-cliente", metodo: "DELETE"));
        //}

        private readonly IOrcamentoService _orcamentoService;
        private readonly ILogger<OrcamentoController> _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly IProdutoService _serviceProduto;
        private readonly IVendedorService _serviceVendedor;

        public OrcamentoController(ILogger<OrcamentoController> logger,
                                   IOrcamentoService orcamentoService,
                                   IUrlHelper urlHelper,
                                   IProdutoService serviceProduto,
                                   IVendedorService serviceVendedor)
        {
            _logger = logger;
            _orcamentoService = orcamentoService;
            _urlHelper = urlHelper;
            _serviceProduto = serviceProduto;
            _serviceVendedor = serviceVendedor;
        }

        [HttpGet(Name = nameof(MostraTodosOrcamento))]
        //public async Task<ActionResult<ColecaoRecursos<Orcamento>>> MostraTodosOrcamento()
        public async Task<IActionResult> MostraTodosOrcamento()
        {
            var orcamento = _orcamentoService.BuscarTodos();
            //orcamento.ForEach(c => GerarLinks(c));

            //var resultado = new ColecaoRecursos<Orcamento>(orcamento);
            //resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(MostraTodosOrcamento), new { }), rel: "self", metodo: "GET"));
            //resultado.Links.Add(new LinkDTO(_urlHelper.Link(nameof(AdicionaOrcamento), new { }), rel: "create-cliente", metodo: "POST"));
       
            return Ok(orcamento);
        }

        [HttpPost("private")]
        public ActionResult AdicionaOrcamento([FromBody] OrcamentoRequest orcamentoRequest)
        {
            _logger.LogInformation("Start inserting Orçamentos");

            var produtos = _serviceProduto.BuscarPorNome(orcamentoRequest.NomeProduto);
            var quantidadeProduto = orcamentoRequest.QuantidadeProduto;
            var random = new Random(0-1000);
            var vendedores = _serviceVendedor.BuscarTodos();


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
            var orcamento = _orcamentoService.BuscarPorId(id);       

            if (orcamento != null && orcamento.Produto != null)
            {
                _orcamentoService.AtualizaOrcamento(id, request);                
                return Ok(orcamento);
            }
            return NotFound($"Orçamento de id {id}, não encontrado!");        }
        
        [HttpPatch("{id}")]
        public ActionResult AtualizaQuantOrcamento(int id, [FromBody] int quantidade)
        {
            var orcamento = _orcamentoService.BuscarPorId(id);
            
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
            var orcamento = _orcamentoService.BuscarPorId(id);
            if (orcamento != null)
            {
                _orcamentoService.Excluir(orcamento);
                return Ok("Orçamento excluído!");
            }
            return NotFound($"Orcamento de id: {id} não encontrado!");
        }       
    }
}


