using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CoreApiClient.Entities
{
    public class Classification
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int RepeatTime { get; set; }
        public char Type { get; set; }
    }
}