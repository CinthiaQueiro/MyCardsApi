using CoreApiClient.Entities;
using Dapper;
using Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class DeckCardsRepository : RepositoryBase<DeckCard>, IDeckCardsRepository
    {
        private IConfiguration _config;

        public DeckCardsRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<DeckCard>> GetDeckCardsUser(int idUser)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = "SELECT * FROM [DeckCards] WHERE IdUser = @idUser";
                var retorno = await conexao.QueryAsync<DeckCard>(sql, new{ idUser = idUser});
                return retorno.ToList();
            }
        }



    }   
}
