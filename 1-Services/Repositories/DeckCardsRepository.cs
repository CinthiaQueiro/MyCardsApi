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
                string sql = @"SELECT [DeckCards].*, COUNT(DISTINCT [easy].Id) Easy,COUNT(DISTINCT [medium].Id) Medium,COUNT(DISTINCT [hard].Id) Hard FROM[DeckCards] WITH(NOLOCK)
                    LEFT JOIN[Card] easy WITH(NOLOCK) ON[easy].IdDeck = [DeckCards].Id AND[easy].IdClassification = 1 AND[easy].DateShow <= GETDATE()
                    LEFT JOIN[Card] medium WITH(NOLOCK) ON[medium].IdDeck = [DeckCards].Id AND[medium].IdClassification = 2 AND[medium].DateShow <= GETDATE()
                    LEFT JOIN[Card] hard WITH(NOLOCK) ON[hard].IdDeck = [DeckCards].Id AND[hard].IdClassification = 3 AND[hard].DateShow <= GETDATE()
                    WHERE IdUser =  @idUser
                GROUP BY[DeckCards].Id,[DeckCards].IdUser, [DeckCards].Description ORDER BY [DeckCards].Description";

                var retorno = await conexao.QueryAsync<DeckCard>(sql, new{idUser = idUser});
                return retorno.ToList();
            }
        }

        public async Task<DeckCard> SaveDeck(DeckCard deckCard)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"INSERT INTO [DeckCards] (IdUser, Description) VALUES(@user ,@description)
                            SELECT @@IDENTITY";

                var param = new { description = deckCard.Description, user = deckCard.User.Id};
                deckCard.Id = await conexao.QueryFirstAsync<int>(sql, param);
                return deckCard;
            }
        }

        public async Task<DeckCard> EditDeckCard(DeckCard deckCard)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"UPDATE [DeckCards] SET Description= @description WHERE id =  @id";

                var param = new { description = deckCard.Description, id = deckCard.Id };
                await conexao.ExecuteAsync(sql, param);
                return deckCard;
            }
        }
        public async Task<DeckCard> DeleteDeckCard(DeckCard deckCard)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                string sql = @"DELETE FROM [card] WHERE IdDeck = @id DELETE FROM [DeckCards] WHERE id = @id";

                var param = new { description = deckCard.Description, id = deckCard.Id };
                await conexao.ExecuteAsync(sql, param);
                return deckCard;
            }
        }

    }   
}
