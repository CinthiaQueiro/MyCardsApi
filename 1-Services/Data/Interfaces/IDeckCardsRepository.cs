using CoreApiClient.Entities;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDeckCardsRepository : IRepositoryBase<DeckCard>
    {
        Task<List<DeckCard>> GetDeckCardsUser(int idUser);
        Task<DeckCard> SaveDeck(DeckCard deckCard);

        Task<DeckCard> EditDeckCard(DeckCard deckCard);

        Task<DeckCard> DeleteDeckCard(DeckCard deckCard);
        
    }
}
