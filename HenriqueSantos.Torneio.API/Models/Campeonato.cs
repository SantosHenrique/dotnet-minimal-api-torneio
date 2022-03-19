namespace HenriqueSantos.Torneio.API.Models
{
    public class Campeonato
    {
        public Guid Id { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public string Nome { get; private set; } = String.Empty;
        public string Descricao { get; private set; } = String.Empty;

        public Campeonato() => Id = Guid.NewGuid();
    }
}
