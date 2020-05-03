using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class ResourceViewDto
    {
        [DisplayName("ID")]
        public int ResourceId { get; set; }
        public int SubjectId { get; set; }
        [DisplayName("Subject")]
        public String SubjectTitle { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public ResourceType Type { get; set; }
        public String Notes { get; set; }
        public int DeckCount { get; set; }
    }
}
