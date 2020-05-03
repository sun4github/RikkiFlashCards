using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public static class NavigationHelper
    {
        public static int CalculateSkip(string Direction, int Skip, int Take, int RecordCount)
        {
            if (!String.IsNullOrWhiteSpace(Direction))
            {
                Skip = (Direction == "Next") ? (Skip + Take) : (Skip - Take);
                Skip = (Skip > RecordCount) ? (Skip - Take) : Skip;
                Skip = (Skip < 0) ? 0 : Skip;
            }

            return Skip;
        }
    }
}
