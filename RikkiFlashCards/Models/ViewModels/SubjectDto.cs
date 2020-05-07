using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class SubjectDto
    {
        
        public int SubjectId { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public String Title { get; set; }
    }
}
