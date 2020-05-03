using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkiFlashCards.Models.Domain;

namespace AnkiFlashCards.Models.DTO
{
    public class ResourceListDto
    {
        public int SubjectId { get; set; }
        public String SubjectTitle { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<Resource> Resources { get; set; }

    }

}
