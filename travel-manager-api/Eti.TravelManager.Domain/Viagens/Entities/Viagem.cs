using Eti.TravelManager.Domain.Common.Exceptions;
using Eti.TravelManager.Domain.Destinos.Entities;

namespace Eti.TravelManager.Domain.Viagens.Entities
{
	public class Viagem
	{
		public int Id { get; set; }
		public string Nome { get; set; } = string.Empty;
		public DateTimeOffset DataSaida { get; set; }
		public DateTimeOffset DataChegada { get; set; }
		public decimal Valor { get; set; }
		public ICollection<Destino> Destinos { get; set; } = [];
		
		protected Viagem() { }

		public Viagem(string nome, DateTimeOffset dataSaida, DateTimeOffset dataChegada, decimal valor)
		{
			Nome = nome;
			DataSaida = dataSaida;
			DataChegada = dataChegada;
			Valor = valor;
		}

		public void AtualizarInformacoes(string nome, DateTimeOffset dataSaida, DateTimeOffset dataChegada, decimal valor)
		{
			Nome = nome;
			DataSaida = dataSaida;
			DataChegada = dataChegada;
			Valor = valor;
		}

		public bool AddDestino(Destino destino)
		{
			if (Destinos.Any(x => x.Id == destino.Id))
			{
				return false;
			}
			
			Destinos.Add(destino);
			return true;
		}

		public bool RemoveDestino(Destino destino)
		{
			var destinoCadastrado = Destinos.FirstOrDefault(x => x.Id == destino.Id);

			if (destinoCadastrado == null)
			{
				throw new RegistroNaoEncontradoException();
			}
			
			Destinos.Remove(destinoCadastrado);
			return true;
		}
	}
}
