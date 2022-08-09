using Microsoft.AspNetCore.Mvc;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Domain;
using WebApplicationOrcamento.Model;
using WebApplicationOrcamento.Service.Service;

namespace WebApplicationOrcamento.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly BaseService<Vendedor> _baseService;
        private readonly ApplicationContext _context;

        public VendedorController(BaseService<Vendedor> baseService, ApplicationContext context)
        {
            _baseService = baseService;
            _context = context;
        }

        [HttpGet]
        public IActionResult MostraTodosVendedor()
        {
            return Ok(_baseService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult MostraVendedorId(int id)
        {
            var vendedor = _baseService.GetById(id);
            if (vendedor != null)
            {
                var orcamento = _context.Orcamento;
                var query = from Orcamento in orcamento
                            where Orcamento.Vendedor.Id == id
                            select Orcamento.ValorTotal;
                var valor = query.Sum();

                VendedorResponse vendedorResponse = new(valor)
                {
                    Id = id,
                    Nome = vendedor.Nome
                };

                return Ok(vendedorResponse);
            }
            return NotFound($"Vendedor com id: {id}, não existe");
        }

        [HttpPost]
        public IActionResult AdicionaVendedor([FromBody] Vendedor vendedor)
        {
            if (vendedor != null)
            {
                _baseService.Add(vendedor);
                return Ok($"Vwndedor {vendedor} foi adicionado!");
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult AtualizaVendedor(int id, [FromBody] string nome)
        {
            var vendedor = _baseService.GetById(id);
            if (vendedor != null)
            {
                vendedor.Nome = nome;
                _baseService.Update(vendedor);               
                return Ok($"Vendedor {vendedor} foi atualizado!");
            }
            return NotFound($"Vendedor {id} não encontrado!");          
        }

        [HttpDelete]
        public IActionResult DeletaVendedor([FromQuery] int id)
        {
            var vendedor = _baseService.GetById(id);
            if (vendedor == null)
            {
                return NotFound($"Vendedor {id} não encontrado!");

            }
            _baseService.Delete(id);
            return Ok($"Vendedor de id {id}, foi excluido!");
        }
    }
}
