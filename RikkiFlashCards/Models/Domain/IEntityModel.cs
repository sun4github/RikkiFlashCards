using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public interface IEntityModel
    {
        public bool IsDeleted { get; set; }
    }
}
