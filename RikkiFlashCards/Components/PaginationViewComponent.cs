using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Primitives;
using RikkiFlashCards.Extensions;
using RikkiFlashCards.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RikkiFlashCards.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly IUrlHelper urlHelper;

        public PaginationViewComponent(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            this.urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public IViewComponentResult Invoke(int itemCount, int itemsPerPage, params KeyValuePair<string, string>[] AdditionalRouteValues)
        {
            var paginationObj = new PaginationBuilder
            {
                PageCount = (int)Math.Ceiling((decimal)itemCount / (decimal)itemsPerPage),
                Action = (string)RouteData.Values["action"],
                Controller = (string)RouteData.Values["controller"]
            };

            var queryValueDictionary = HttpContext.Request.Query.ToQueryValueDictionary();
            paginationObj.CurrentPage = (queryValueDictionary.ContainsKey("NextPage")) ? int.Parse(queryValueDictionary["NextPage"]):1;

            if(AdditionalRouteValues != null)
            {
                foreach (var kvPair in AdditionalRouteValues)
                {
                    if(!queryValueDictionary.ContainsKey(kvPair.Key))
                    {
                        queryValueDictionary.Add(kvPair.Key, kvPair.Value);
                    }
                }
            }

            for (int i = 1; i <= paginationObj.PageCount; i++)
            {
                queryValueDictionary["NextPage"] = i.ToString();
                paginationObj.PaginationLinks.Add(urlHelper.Action((string)RouteData.Values["action"], (string)RouteData.Values["controller"], queryValueDictionary));
            }
            return View("",paginationObj);
        }
    }

    public class PaginationBuilder
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public List<string> PaginationLinks { get; set; } = new List<string>();
    }
}
