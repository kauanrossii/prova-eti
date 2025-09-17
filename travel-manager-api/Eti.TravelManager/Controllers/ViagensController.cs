using Eti.TravelManager.Application.UseCases.Viagens.AdicionarDestino;
using Eti.TravelManager.Application.UseCases.Viagens.AtualizarViagem;
using Eti.TravelManager.Application.UseCases.Viagens.CriarViagem;
using Eti.TravelManager.Application.UseCases.Viagens.DetalharViagem;
using Eti.TravelManager.Application.UseCases.Viagens.RemoverViagem;
using Eti.TravelManager.Application.UseCases.Viagens.PesquisarViagens;
using Eti.TravelManager.Application.UseCases.Viagens.RemoverDestinoViagem;
using Microsoft.AspNetCore.Mvc;

namespace Eti.TravelManager.Controllers;

[ApiController]
[Route("/api/viagens")]
public class ViagensController : ControllerBase
{
    private readonly CriarViagemCommandHandler _criarViagemCommandHandler;
    private readonly DetalharViagemQueryHandler _detalharViagemQueryHandler;
    private readonly PesquisarViagensQueryHandler _pesquisarViagensQueryHandler;
    private readonly AtualizarViagemCommandHandler _atualizarViagemCommandHandler;
    private readonly RemoverViagemCommandHandler _removerViagemCommandHandler;
    private readonly RemoverDestinoViagemCommandHandler _removerDestinoViagemCommandHandler;
    private readonly AdicionarDestinoViagemCommandHandler _adicionarDestinoViagemCommandHandler;

    public ViagensController(CriarViagemCommandHandler criarViagemCommandHandler,
        DetalharViagemQueryHandler detalharViagemQueryHandler,
        PesquisarViagensQueryHandler pesquisarViagensQueryHandler,
        AtualizarViagemCommandHandler atualizarViagemCommandHandler,
        RemoverViagemCommandHandler removerViagemCommandHandler,
        RemoverDestinoViagemCommandHandler removerDestinoViagemCommandHandler,
        AdicionarDestinoViagemCommandHandler adicionarDestinoViagemCommandHandler)
    {
        _criarViagemCommandHandler = criarViagemCommandHandler;
        _detalharViagemQueryHandler = detalharViagemQueryHandler;
        _pesquisarViagensQueryHandler = pesquisarViagensQueryHandler;
        _atualizarViagemCommandHandler = atualizarViagemCommandHandler;
        _removerViagemCommandHandler = removerViagemCommandHandler;
        _removerDestinoViagemCommandHandler = removerDestinoViagemCommandHandler;
        _adicionarDestinoViagemCommandHandler = adicionarDestinoViagemCommandHandler;
    }

    [HttpGet]
    public async Task<ActionResult> GetViagensAsync([FromQuery] PesquisarViagensQuery pesquisarViagensQuery,
        CancellationToken cancellationToken)
    {
        var resultado = await _pesquisarViagensQueryHandler.ExecutarAsync(pesquisarViagensQuery, cancellationToken);
        return resultado.Sucesso ? Ok(resultado.Dados) : BadRequest(resultado.Mensagens);
    }

    [HttpGet("{viagemId}")]
    public async Task<IActionResult> GetViagemAsync([FromRoute] int viagemId, CancellationToken cancellationToken)
    {
        var detalharViagemQuery = new DetalharViagemQuery { Id = viagemId };
        var resultado = await _detalharViagemQueryHandler.ExecutarAsync(detalharViagemQuery, cancellationToken);
        return Ok(resultado.Dados);
    }

    [HttpPost]
    public async Task<IActionResult> PostViagemAsync([FromBody] CriarViagemCommand criarViagemCommand,
        CancellationToken cancellationToken)
    {
        var resultado = await _criarViagemCommandHandler.ExecutarAsync(criarViagemCommand, cancellationToken);
        return resultado.Sucesso ? Ok(resultado.Dados) : BadRequest(resultado.Mensagens);
    }

    [HttpPut("{viagemId}")]
    public async Task<IActionResult> PutViagemAsync([FromRoute] int viagemId,
        [FromBody] AtualizarViagemCommand atualizarViagemCommand,
        CancellationToken cancellationToken)
    {
        atualizarViagemCommand.Id = viagemId;
        var resultado = await _atualizarViagemCommandHandler.ExecutarAsync(atualizarViagemCommand, cancellationToken);
        return resultado.Sucesso ? NoContent() : BadRequest(resultado.Mensagens);
    }

    [HttpDelete("{viagemId}")]
    public async Task<IActionResult> DeleteViagemAsync([FromRoute] int viagemId, CancellationToken cancellationToken)
    {
        var removerViagemCommand = new RemoverViagemCommand { Id = viagemId };
        await _removerViagemCommandHandler.ExecutarAsync(removerViagemCommand, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{viagemId}/destinos/{destinoId}")]
    public async Task<IActionResult> DeleteDestinoViagemAsync([FromRoute] int viagemId, [FromRoute] int destinoId,
        CancellationToken cancellationToken)
    {
        var removerViagemCommand = new RemoverDestinoViagemCommand { Id = viagemId, DestinoId = destinoId };
        await _removerDestinoViagemCommandHandler.ExecutarAsync(removerViagemCommand, cancellationToken);
        return NoContent();
    }

    [HttpPost("{viagemId}/destinos")]
    public async Task<IActionResult> PostDestinoViagemAsync([FromRoute] int viagemId,
        [FromBody] AdicionarDestinoViagemCommand adicionarDestinoViagemCommand, CancellationToken cancellationToken)
    {
        adicionarDestinoViagemCommand.Id = viagemId;
        var resultado =
            await _adicionarDestinoViagemCommandHandler.ExecutarAsync(adicionarDestinoViagemCommand, cancellationToken);
        return resultado.Sucesso ? Ok(resultado.Dados) : BadRequest(resultado.Mensagens);
    }
}