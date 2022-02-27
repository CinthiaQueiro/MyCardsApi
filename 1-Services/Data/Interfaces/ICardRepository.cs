using CoreApiClient.Entities;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICardRepository : IRepositoryBase<Card>
    {
        Task<Card> SaveCard(Card card);
        
    }
}
