using Eti.TravelManager.Application.UseCases.Destinos.AtualizarDestino;
using Eti.TravelManager.Application.UseCases.Destinos.CriarDestino;
using Eti.TravelManager.Application.UseCases.Destinos.DetalharDestino;
using Eti.TravelManager.Application.UseCases.Destinos.PesquisarDestinos;
using Eti.TravelManager.Application.UseCases.Destinos.RemoverDestino;
using Microsoft.AspNetCore.Mvc;

namespace Eti.TravelManager.Controllers;

[ApiController]
[Route("/api/destinos")]
public class DestinosController : ControllerBase
{
    private readonly CriarDestinoCommandHandler _criarDestinoCommandHandler;
    private readonly DetalharDestinoQueryHandler _detalharDestinoQueryHandler;
    private readonly PesquisarDestinosQueryHandler _pesquisarDestinosQueryHandler;
    private readonly AtualizarDestinoCommandHandler _atualizarDestinoCommandHandler;
    private readonly RemoverDestinoCommandHandler _removerDestinoCommandHandler;

    public DestinosController(CriarDestinoCommandHandler criarDestinoCommandHandler,
        DetalharDestinoQueryHandler detalharDestinoQueryHandler,
        PesquisarDestinosQueryHandler pesquisarDestinosQueryHandler,
        AtualizarDestinoCommandHandler atualizarDestinoCommandHandler,
        RemoverDestinoCommandHandler removerDestinoCommandHandler)
    {
        _criarDestinoCommandHandler = criarDestinoCommandHandler;
        _detalharDestinoQueryHandler = detalharDestinoQueryHandler;
        _pesquisarDestinosQueryHandler = pesquisarDestinosQueryHandler;
        _atualizarDestinoCommandHandler = atualizarDestinoCommandHandler;
        _removerDestinoCommandHandler = removerDestinoCommandHandler;
    }

    [HttpGet]
    public async Task<ActionResult> GetDestinosAsync([FromQuery] PesquisarDestinosQuery pesquisarDestinosQuery,
        CancellationToken cancellationToken)
    {
        var resultado = await _pesquisarDestinosQueryHandler.ExecutarAsync(pesquisarDestinosQuery, cancellationToken);
        return resultado.Sucesso ? Ok(resultado.Dados) : BadRequest(resultado.Mensagens);
    }

    [HttpGet("{destinoId}")]
    public async Task<IActionResult> GetDestinoAsync([FromRoute] int destinoId, CancellationToken cancellationToken)
    {
        var detalharDestinoQuery = new DetalharDestinoQuery { Id = destinoId };
        var resultado = await _detalharDestinoQueryHandler.ExecutarAsync(detalharDestinoQuery, cancellationToken); 
        return Ok(resultado.Dados);
    }

    [HttpPost]
    public async Task<IActionResult> PostDestinoAsync([FromBody] CriarDestinoCommand criarDestinoCommand,
        CancellationToken cancellationToken)
    {
        var resultado = await _criarDestinoCommandHandler.ExecutarAsync(criarDestinoCommand, cancellationToken);
        return resultado.Sucesso ? Ok(resultado.Dados) : BadRequest(resultado.Mensagens);
    }

    [HttpPut("{destinoId}")]
    public async Task<IActionResult> PutDestinoAsync([FromRoute] int destinoId, [FromBody] AtualizarDestinoCommand atualizarDestinoCommand,
        CancellationToken cancellationToken)
    {
        atualizarDestinoCommand.Id = destinoId;
        var resultado = await  _atualizarDestinoCommandHandler.ExecutarAsync(atualizarDestinoCommand, cancellationToken);
        return resultado.Sucesso ? NoContent() : BadRequest(resultado.Mensagens);
    }

    [HttpDelete("{destinoId}")]
    public async Task<IActionResult> DeleteDestinoAsync([FromRoute] int destinoId, CancellationToken cancellationToken)
    {
        var removerDestinoCommand = new RemoverDestinoCommand { Id = destinoId };
        await _removerDestinoCommandHandler.ExecutarAsync(removerDestinoCommand, cancellationToken);
        return NoContent();
    }
}