using filme.Repository;
using Microsoft.AspNetCore.Mvc;
using filme.Models;
using System.Threading.Tasks;

namespace filme.Controllers;
[ApiController]
[Route("api/[controller]")]

public class ProdutoraController : ControllerBase

 {
  
    private readonly IProdutoraRepository _repository;

    public ProdutoraController(IProdutoraRepository repository)
    {
       _repository = repository;
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Produtora invalido");

            var produtora = await _repository.BuscarProdutoraAsync(id);

            if (produtora == null) NotFound("Produtora n existe");

            var deletado = await _repository.DeletarAsync(id);

            return deletado
                    ? Ok("produtora deletado sucesso !")
                    : BadRequest("error ao deletar produtora");
        }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
       var filmes = await _repository.BuscarProdutoraAsync();

       return filmes.Any() ? Ok(filmes) : NoContent();
    }



    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var produtora = await _repository.BuscarProdutoraAsync(id);

        return produtora != null ? Ok(produtora) : NotFound();
    }

    [HttpPut("id")]
    public async Task<IActionResult> Put(ProdutoraRequest request, int id)
    {
        if (id <= 0) return BadRequest("Produtora invalido");

        var produtora = await _repository.BuscarProdutoraAsync(id);

        if (produtora == null) NotFound("Produtora n existe");

        if (string.IsNullOrEmpty(request.Nome)) 
        {
            request.Nome = produtora.Nome;
        } 
       

        var atualizado = await _repository.AtualizarAsync(request, id);

        return atualizado
                    ? Ok("produtora atualizado sucesso !")
                    : BadRequest("error ao atualizar");


    }
    [HttpPost]
    public async Task<IActionResult> Post(ProdutoraRequest request)
    {
        if (string.IsNullOrEmpty(request.Nome))
        {
            return BadRequest("Informacao invalida");
        }

        var adicionado = await _repository.AdicionaAsync(request);

        return adicionado
        ? Ok("filme adicionado !")
        : BadRequest("error ao adicionar");

    }

}
