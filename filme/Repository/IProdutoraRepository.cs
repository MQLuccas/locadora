using filme.Models;
namespace filme.Repository
{
    public interface IProdutoraRepository
    {
        Task<bool> DeletarAsync(int id);
        Task<ProdutoraResponse> BuscarProdutoraAsync(int id);
        Task<IEnumerable<ProdutoraResponse>> BuscarProdutoraAsync();
        Task<bool> AtualizarAsync(ProdutoraRequest request, int id);
        Task<bool> AdicionaAsync(ProdutoraRequest request);



    }
}
