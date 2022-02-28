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

        public async Task<List<Card>> Get(int idDeckCard)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"SELECT * FROM [Card], [Classification] 
                    WHERE [Card].IdDeck = @idDeckCard AND [Card].DateShow <= GETDATE()";

                var cards =  conexao.Query<Card, Classification,Card>(sql, (card, classification) => {
                    if(card.Classification == null) card.Classification = new List<Classification>();
                    card.Classification.Add(classification);
                    return card;
                },
                new { idDeckCard = idDeckCard }, splitOn: "Id").Distinct().ToList();

                var result = cards.GroupBy(card => card.Id).Select(g =>
                {
                    var groupedClassification = g.First();
                    groupedClassification.Classification = g.Select(p => p.Classification.Single()).ToList();
                    return groupedClassification;
                });

                return result.ToList();
            }

        }

        public async Task<Card> UpdateCard(Card card)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"UPDATE [Card] SET DataQuestion= @DataQuestion, IdTypeQuestion = @IdTypeQuestion, 
                            DataAnswer = @DataAnswer, IdTypeAnswer = @IdTypeAnswer, DateShow = @DateShow, [Order] = @Order, IdClassification = @IdClassification
                            WHERE IdDeck = @IdDeck AND Id = @Id";


                await conexao.QueryAsync(sql, card);
                return card;
            }

        }

    }   
}
