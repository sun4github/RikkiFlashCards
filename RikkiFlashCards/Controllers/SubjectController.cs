using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnkiFlashCards.Controllers
{
    public class SubjectController : Controller
    {
        private IRepositoryWrapper repositoryWrapper;
        private int ItemsPerPage = 5;

        public SubjectController(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public ViewResult Index(String Direction, int NextPage = 1)
        {
            var subjects = this.repositoryWrapper.Subject.FindAll().OrderBy(s=>s.Title).ToList();
            var Skip = (NextPage - 1 ) * ItemsPerPage;

            var subjListDto = new SubjectListDto
            {
                Skip = Skip,
                Take = ItemsPerPage,
                Subjects = subjects.Skip(Skip).Take(ItemsPerPage).ToList()
            };
            ViewBag.ItemsPerPage = ItemsPerPage;
            return View(subjListDto);
        }

      

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SubjectDto subjectDto)
        {
            try
            {
                var newSubject = new Subject()
                {
                    Title = subjectDto.Title
                };
                repositoryWrapper.Subject.Create(newSubject);
                repositoryWrapper.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult Edit(int SubjectId)
        {
            var subj = repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == SubjectId).First();
            var subjDto = new SubjectDto()
            {
                Title = subj.Title,
                SubjectId = subj.SubjectId
            };
            return View(subjDto);
        }

        [HttpPost]
        public ActionResult Edit(SubjectDto editSubject)
        {
            try
            {
                var subj = repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == editSubject.SubjectId).First();
                subj.Title = editSubject.Title;
                repositoryWrapper.Subject.Update(subj);
                repositoryWrapper.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int SubjectId)
        {
            var subj = repositoryWrapper.Subject.FindByCondition(s => s.SubjectId == SubjectId).First();
            repositoryWrapper.Subject.Delete(subj);
            repositoryWrapper.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}