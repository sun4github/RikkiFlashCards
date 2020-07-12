using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Models.DomainModels
{
    public class FlashCardUser : IdentityUser
    {
        public bool IsStudent { get; set; }
        public int LoginCount { get; set; }
    }
}
