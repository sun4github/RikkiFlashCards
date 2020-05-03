using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class DeckViewDto
    {
        [DisplayName("Id")]
        public int DeckId { get; set; }
        public int ResourceId { get; set; }
        public int SubjectId { get; set; }
        [DisplayName("Resource")]
        public String ResourceTitle { get; set; }
        [DisplayName("Title")]
        public String Title { get; set; }
        [DisplayName("Shared")]
        public bool IsShared { get; set; }
        [DisplayName("Number of Revisions")]
        public int RevisionCount { get; set; }
        [DisplayName("Number of Cards")]
        public int CardCount { get; set; }
        [DisplayName("Last Revision Date & Time")]
        public string LastRevisionDateTime { get; set; }
    }
}
