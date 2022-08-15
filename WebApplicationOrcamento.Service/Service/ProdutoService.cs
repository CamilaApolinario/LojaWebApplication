using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationOrcamento.Domain.Interfaces;
using WebApplicationOrcamento.Model;

namespace WebApplicationOrcamento.Service.Service
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        private readonly IBaseRepository<Produto> _produtoRepository;


        public ProdutoService(IBaseRepository<Produto> baseRepository) : base(baseRepository)
        {
            _produtoRepository = baseRepository;
        }

        public Produto BuscarPorNome(string nome) => _produtoRepository.SelectId(nome);
    }
}
