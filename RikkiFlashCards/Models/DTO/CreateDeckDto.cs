using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class CreateDeckDto
    {
        public int DeckId { get; set; }
        public int ResourceId { get; set; }
        public int SubjectId { get; set; }
        [DisplayName("Resource")]
        public string ResourceTitle { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public String Title { get; set; }
        [DisplayName("Shared")]
        public bool IsShared { get; set; }
    }
}
