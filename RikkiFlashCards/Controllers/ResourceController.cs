using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnkiFlashCards.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IRepositoryWrapper repositoryWrapper;
        private int ItemsPerPage = 5;

        public ResourceController(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public ActionResult Delete(int ResourceId)
        {
            var res = repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId).First();
            repositoryWrapper.Resource.Delete(res);
            repositoryWrapper.Save();
            return RedirectToAction(nameof(Index), new { SubjectId = res.SubjectId });
        }

        [HttpGet]
        public ViewResult Create(int SubjectId)
        {
            var newResourceDto = new ResourceCreateDto()
            {
                SubjectId = SubjectId
            };
            return View(newResourceDto);
        }

        [HttpPost]
        public ActionResult Create(ResourceCreateDto createResourceDto)
        {
            try
            {
                var newResource = new Resource()
                {
                    Type = createResourceDto.Type,
                    Notes = createResourceDto.Notes,
                    Url = createResourceDto.Url,
                    Title = createResourceDto.Title,
                    SubjectId = createResourceDto.SubjectId
                };
                repositoryWrapper.Resource.Create(newResource);
                repositoryWrapper.Save();

                return RedirectToAction(nameof(Index),new { SubjectId = createResourceDto.SubjectId });
            }
            catch
            {
                return View();
            }
        }

        //public ViewResult Index(int SubjectId, String Direction, int Skip = 0, int Take = 5)
        //{
        //    var selSubj = this.repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == SubjectId)
        //        .Include(s => s.Resources)
        //            .ThenInclude(r=>r.Decks)
        //        .First();

        //    var resources = selSubj.Resources.OrderBy(r => r.Title).ToList();
        //    Skip = NavigationHelper.CalculateSkip(Direction, Skip, Take, resources.Count());

        //    var ResourceListDto = new ResourceListDto()
        //    {
        //        SubjectId = SubjectId,
        //        SubjectTitle = selSubj.Title,
        //        Resources = resources.Skip(Skip).Take(Take),
        //        Skip = Skip,
        //        Take = Take
        //    };
        //    return View(ResourceListDto);
        //}

        public ViewResult Index(int SubjectId, int NextPage = 1)
        {
            var selSubj = this.repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == SubjectId)
                .Include(s => s.Resources)
                    .ThenInclude(r => r.Decks)
                .First();

            var resources = selSubj.Resources.OrderBy(r => r.Title).ToList();
            var Skip = (NextPage-1) * ItemsPerPage;

            var ResourceListDto = new ResourceListDto()
            {
                SubjectId = SubjectId,
                SubjectTitle = selSubj.Title,
                Resources = resources.Skip(Skip).Take(ItemsPerPage),
                Skip = Skip,
                Take = ItemsPerPage
            };
            ViewBag.ItemsPerPage = ItemsPerPage;
            return View(ResourceListDto);
        }

        [HttpGet]
        public ViewResult View(int ResourceId)
        {
            var curRes = this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
                            .Include(r=>r.Decks)
                            .First();
            var subjTitle = this.repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == curRes.SubjectId).First().Title;
            var dto = new ResourceViewDto()
            {
                ResourceId = curRes.ResourceId,
                SubjectId = curRes.SubjectId,
                Title = curRes.Title,
                Url = curRes.Url,
                DeckCount = curRes.DeckCount,
                Notes = curRes.Notes,
                Type = curRes.Type,
                SubjectTitle = subjTitle

            };
            return View(dto);
        }

        [HttpGet]
        public ViewResult Edit(int ResourceId)
        {
            var curRes = this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId).First();
            var subjTitle = this.repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == curRes.SubjectId).First().Title;
            var newResourceDto = new ResourceEditDto()
            {
                ResourceId = curRes.ResourceId,
                SubjectId = curRes.SubjectId,
                Title = curRes.Title,
                Url = curRes.Url,
                Notes = curRes.Notes,
                Type = curRes.Type,
                SubjectTitle = subjTitle
            };
            return View(newResourceDto);
        }

        [HttpPost]
        public ActionResult Edit(ResourceEditDto editResourceDto)
        {
          
            try
            {
                var curRes = this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == editResourceDto.ResourceId).First();
                curRes.Title = editResourceDto.Title;
                curRes.Url = editResourceDto.Url;
                curRes.Notes = editResourceDto.Notes;
                curRes.Type = editResourceDto.Type;

                repositoryWrapper.Resource.Update(curRes);
                repositoryWrapper.Save();

                return RedirectToAction(nameof(Index),new { SubjectId = curRes.SubjectId});
            }
            catch
            {
                return View();
            }
        }
    }
}