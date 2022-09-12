using CinemaClient.Entities;

namespace CinemaClient.Models
{
    public class SessaoSugestaoDataInicioViewModel
    {
        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int? Disponivel { get; set; }

        public Guid? FilmeId { get; set; }

        public Guid? SalaId { get; set; }
    }
}
