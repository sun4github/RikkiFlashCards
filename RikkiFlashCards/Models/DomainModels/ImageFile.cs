using AnkiFlashCards.Models.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Models.DomainModels
{
    public class ImageFile : IEntityModel
    {
        [DisplayName("ID")]
        public int ImageFileId { get; set; }
        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string FilePath { get; set; }
        public bool IsDeleted { get; set; }

    }
}
