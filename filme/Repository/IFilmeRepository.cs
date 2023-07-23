using filme.Models;

namespace filme.Repository
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<FilmeResponse>> BuscarFilmesAsync();
        Task<FilmeResponse> BuscarFilmeAsync(int id);
        Task<bool> AdicionaAsync(FilmeRequest request);
        Task<bool> AtualizarAsync(FilmeRequest request, int id);
        Task<bool> DeletarAsync(int id);
    }
}
