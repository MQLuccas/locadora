using System.Data.SqlClient;
using Dapper;
using filme.Models;
using Microsoft.AspNetCore.Mvc;


namespace filme.Repository
{
    public class ProdutoraRepository : IProdutoraRepository
       
    {
        private readonly IConfiguration _configuration;

        private readonly string connectionString;
        private object request;
        public ProdutoraRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public async Task<bool> DeletarAsync(int id)
        {
            string sql = @"DELETE FROM tb_produtora
                            WHERE id = @Id";

            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;




        }

        public async Task<ProdutoraResponse> BuscarProdutoraAsync(int id)
        {
            string sql = @"SELECT  p.id  Id,
		                    p.nome	Nome
                            FROM	tb_produtora p
                            WHERE	 p.id = @Id";

            using var con = new SqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<ProdutoraResponse>(sql, new { Id = id });

        }

        public async Task<IEnumerable<ProdutoraResponse>> BuscarProdutoraAsync()
        {
            string sql = @"SELECT 
                             p.id Id,
                             p.nome	Nome
                             FROM	tb_produtora p";

            using var con = new SqlConnection(connectionString);
            return await con.QueryAsync<ProdutoraResponse>(sql);


        }
        public async Task<bool> AtualizarAsync(ProdutoraRequest request, int id)
        {
            string sql = @"UPDATE tb_produtora SET
	                        nome = @Nome   
                            WHERE id = @Id";
            var parametros = new DynamicParameters();
            
            parametros.Add("Nome", request.Nome);
            parametros.Add("id", id);



            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql,parametros) > 0;
        }
        public async Task<bool> AdicionaAsync(ProdutoraRequest request)
        {
            string sql = @"INSERT INTO tb_produtora(nome)
                            VALUES (@Nome)";

            using var con = new SqlConnection(connectionString);
            return await con.ExecuteAsync(sql, request) > 0;
        }

    }
}
