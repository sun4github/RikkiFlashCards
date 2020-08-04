using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class SubjectListDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public int TotalSubjectCount { get; set; }
    }
}
