using filme.Models;
using filme.Repository;
using Microsoft.AspNetCore.Mvc;

namespace filme.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FilmesController : ControllerBase

 {
    private readonly IFilmeRepository _repository;

    public FilmesController(IFilmeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]   
    public async Task<IActionResult> Get()
    {
        var filmes = await _repository.BuscarFilmesAsync();

        return filmes.Any() ? Ok(filmes): NoContent();
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var filme = await _repository.BuscarFilmeAsync(id);

        return filme != null ? Ok(filme) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post(FilmeRequest request)
    {
        if (string.IsNullOrEmpty(request.Nome) || request.Ano <= 0 || request.ProdutoraID <=0)
        {
            return BadRequest("Informacao invalida"); 
        }

        var adicionado = await _repository.AdicionaAsync(request);

            return adicionado 
            ? Ok("filme adicionado !")
            : BadRequest("error ao adicionar");

    }

    [HttpPut("id")]
    public async Task<IActionResult> Put(FilmeRequest request,int id)
    {
        if (id <= 0) return BadRequest("filme invalido");

        var filme = await _repository.BuscarFilmeAsync(id);

        if (filme == null) NotFound("filme n existe");

        if (string.IsNullOrEmpty(request.Nome)) request.Nome = filme.Nome;
        if (request.Ano <= 0) request.Ano = filme.Ano;

        var atualizado = await _repository.AtualizarAsync(request, id);

        return atualizado
                    ? Ok("filme atualizado sucesso !")
                    : BadRequest("error ao atualizar");


    }
    [HttpDelete("id")]  

    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("filme invalido");

        var filme = await _repository.BuscarFilmeAsync(id);

        if (filme == null) NotFound("filme n existe");

        var deletado = await _repository.DeletarAsync(id);

        return deletado
                ? Ok("filme deletado sucesso !")
                : BadRequest("error ao deletar");
    }

}

