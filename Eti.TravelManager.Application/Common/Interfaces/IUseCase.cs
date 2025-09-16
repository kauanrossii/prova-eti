using Eti.TravelManager.Application.Common.Dtos;

namespace Eti.TravelManager.Application.Common.Interfaces;

public interface IUseCase<TRequisicao, TResultado>
{
    Task<ResultDto<TResultado>> ExecutarAsync(TRequisicao requisicao, CancellationToken cancellationToken);
}

public interface IUseCase<TRequisicao>
{
    Task ExecutarAsync(TRequisicao requisicao, CancellationToken cancellationToken);
}