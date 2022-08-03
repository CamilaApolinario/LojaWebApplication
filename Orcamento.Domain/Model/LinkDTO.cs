namespace WebApplicationOrcamento.Model
{
    public class LinkDTO
    {
        public int Id { get; private set; }
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Metodo { get; set; }

        public LinkDTO(string href, string rel, string metodo)
        {
            Href = href;
            Rel = rel;
            Metodo = metodo;
        }
    }
}

