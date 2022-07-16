using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento.Data;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento
{
    public class OrcamentoService
    {
        private readonly ApplicationContext _context;
        public OrcamentoService(ApplicationContext context)
        {
            _context = context;
        }

        public Orcamento AdicionaOrcamento(Produto produto, Vendedor vendedor, int quantidadeProduto)
        {
            var orcamento = new Orcamento(vendedor, produto, quantidadeProduto);
            return orcamento;
        }

        //public Orcamento AtualizaOrcamento(Orcamento orcamento)
        //{

        //    _context.Update(orcamento).State = EntityState.Modified;
        //    _context.SaveChangesAsync();
        //    return orcamento;
        //}
    
    }
}
