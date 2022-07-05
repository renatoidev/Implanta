using Microsoft.AspNetCore.Mvc;
using Implanta.Service.Interfaces;
using Implanta.ViewModels;
using Implanta.Service.ViewModels;

namespace Implanta.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class ProfissionalController : ControllerBase
{

    private readonly IProfissionalService _profissionalService;

    public ProfissionalController(IProfissionalService profissionalController)
    {
        _profissionalService = profissionalController;
    }

    [HttpGet()]
    [Route("v1/profissionais")]
    public async Task<IActionResult> GetProfissionais()
    {
        var obj = await _profissionalService.GetProfissionais();
        return Ok(new ResultViewModel<List<GetProfissionalViewModel>>(obj));
    }

    [HttpGet()]
    [Route("v1/profissionais/{de}/{ate}")]
    public async Task<IActionResult> GetProfissionais(int de, int ate)
    {
        var obj = await _profissionalService.GetProfissionais(de, ate);
        return Ok(new ResultViewModel<List<GetProfissionalViewModel>>(obj));
    }

    [HttpGet()]
    [Route("v1/profissionais/ativos")]
    public async Task<IActionResult> GetProfissionaisAtivos()
    {
        var obj = await _profissionalService.GetProfissionaisAtivos(true);
        return Ok(new ResultViewModel<List<GetProfissionalViewModel>>(obj));
    }

    [HttpPost()]
    [Route("v1/profissionais/")]
    public async Task<IActionResult> AddProfissional(
        [FromBody] AddProfissionalViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>("Dados inválidos"));

        var obj = await _profissionalService.AddProfissional(model);

        if (obj is null)
            return BadRequest(new ResultViewModel<string>("Erro interno no servidor"));

        return Ok(new ResultViewModel<dynamic>(obj));
    }

    [HttpPut()]
    [Route("v1/profissionais/{id}")]
    public async Task<IActionResult> EditProfissional(
        [FromRoute]int id,
        [FromBody] EditProfissionalViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>("Dados inválidos"));

        var obj = await _profissionalService.EditProfissional(id, model);

        if (obj is null)
            return BadRequest(new ResultViewModel<string>("Erro interno no servidor"));

        return Ok(new ResultViewModel<dynamic>(obj));
    }

    [HttpDelete]
    [Route("v1/profissionais/{id}")]
    public async Task<IActionResult> DeleteProfissional(
        [FromRoute] int id)
    {
        try
        {
            var obj = await _profissionalService.DeleteProfissional(id);
            return Ok(new ResultViewModel<string>("Profissional excluído com sucesso"));
        }
        catch (Exception)
        {
            return BadRequest(new ResultViewModel<string>("Erro interno no servidor"));
        }
    }
}
