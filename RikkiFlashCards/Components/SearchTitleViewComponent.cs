using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Components
{
    public class SearchTitleViewComponent : ViewComponent
    {
        public SearchTitleViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            var action = RouteData.Values["action"];
            var controller = RouteData.Values["controller"];

            return View("",new { Action = action, Controller = controller });
        }
    }
}
