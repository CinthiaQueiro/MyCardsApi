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
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        private IConfiguration _config;

        public CardRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

       
        public async Task<Card> SaveCard(Card card)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"INSERT INTO [Card] VALUES(@IdDeck,
                                @DataQuestion, @IdTypeQuestion, @DataAnswer, @IdTypeAnswer, GETDATE(), @Order, @IdClassification)
                            SELECT @@IDENTITY";


                card.Id = await conexao.QueryFirstAsync<int>(sql, card);
                return card;
            }

        }

    }   
}
