namespace WebApplicationOrcamento.Model
{
    public class Recurso : IRecurso
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }

}
