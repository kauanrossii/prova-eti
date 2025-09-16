namespace Eti.TravelManager.Domain.Destinos.Entities
{
	public class Destino
	{
		public int Id { get; set; }
		public string Nome { get; protected set; } = string.Empty;
		
		protected Destino() { }

		public Destino(string nome)
		{
			Nome = nome;
		}

		public void AtualizarInformacoes(string nome)
		{
			Nome = nome;
		}
	}
}
