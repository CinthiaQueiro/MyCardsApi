using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CoreApiClient.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public int IdDeck { get; set; }
        public string DataQuestion { get; set; }
        public int IdTypeQuestion { get; set; }
        public string DataAnswer { get; set; }
        public int IdTypeAnswer { get; set; }
        public DateTime DateShow { get; set; }
        public int Order { get; set; }
        public int IdClassification { get; set; }
        public List<Classification> Classification {get; set;}
    }
}