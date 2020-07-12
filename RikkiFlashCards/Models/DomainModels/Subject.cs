using RikkiFlashCards.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public class Subject : IEntityModel
    {
        [DisplayName("ID")]
        public int SubjectId { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public String Title { get; set; }
        public int ResourceCount { 
            get {
                return Resources.Count();    
            } 
        }
        public ICollection<Resource> Resources { get; set; }

        public bool IsDeleted { get; set; }
                
        public FlashCardUser FlashCardUser { get; set; }
    }
}
