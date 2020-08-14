using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RikkiFlashCards.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public class Card : IEntityModel
    {
        [DisplayName("ID")]
        public int CardId { get; set; }
        [ForeignKey("Deck")]
        public int DeckId { get; set; }
        public Deck Deck { get; set; }
        [DisplayName("Question")]
        [Required]
        [MaxLength(5000)]
        public String Front { get; set; }

        [DisplayName("Question")]
        [Required]
        public String ShortFront { get {

                return (Front.Length > 100) ? (Front.Substring(0, 100) + " ...") : Front;
            }
        }
        [DisplayName("Answer")]
        [Required]
        [MaxLength(5000)]
        public String Back { get; set; }

        [DisplayName("Answer")]
        public String ShortBack
        {
            get
            {

                return (Back.Length > 100) ? (Back.Substring(0,100) + " ..."):Back;
            }
        }
        public DifficultyLevel Level { get; set; }

        public bool IsDeleted { get; set; }
                
        [BindNever]
        public ICollection<ImageFile> ImageFiles { get; set; }
    }

    public enum DifficultyLevel
    {
        Easy=0,
        Medium=1,
        Hard=2
    }
}
