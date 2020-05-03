using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class ResourceCreateDto
    {
        [DisplayName("ID")]
        public int ResourceId { get; set; }
        public int SubjectId { get; set; }
        
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public String Title { get; set; }
        [UrlAttribute]
        public String Url { get; set; }
        [Required]
        public ResourceType Type { get; set; }
        public String Notes { get; set; }
    }
}
