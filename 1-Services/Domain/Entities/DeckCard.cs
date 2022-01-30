using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CoreApiClient.Entities
{
    public class DeckCard
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }
        public string Description { get; set; }

    }
}