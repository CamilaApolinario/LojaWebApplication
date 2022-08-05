using WebApplicationOrcamento.Domain.Interfaces;

namespace WebApplicationOrcamento.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
