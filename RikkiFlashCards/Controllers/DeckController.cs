﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services;
using AnkiFlashCards.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnkiFlashCards.Controllers
{
    public class DeckController : Controller
    {
        private readonly IDeckService deckService;
        private readonly IRepositoryWrapper repositoryWrapper;
        private int ItemsPerPage = 5;

        public DeckController(IDeckService deckService, IRepositoryWrapper repositoryWrapper)
        {
            this.deckService = deckService;
            this.repositoryWrapper = repositoryWrapper;
        }

        public ViewResult Index(int ParentId, int NextPage=1, string OrderBy = null, string SearchText = "")
        {
            var deckList = deckService.ListDecks(ParentId, NextPage, ItemsPerPage, OrderBy, SearchText);
            ViewBag.ItemsPerPage = ItemsPerPage;
            return View(deckList);
        }

        [HttpGet]
        public ViewResult Edit(int DeckId)
        {
            var dck = deckService.GetDeck(DeckId);
            var res = deckService.GetResource(dck.ResourceId);
            var ed = new EditDeckDto
            {
                DeckId = dck.DeckId,
                ResourceId = dck.ResourceId,
                IsShared = dck.IsShared,
                Title = dck.Title,
                ResourceTitle = res.Title
            };
            return View(ed);
        }

        [HttpPost]
        public ActionResult Edit(EditDeckDto editDeckDto)
        {
            try
            {
                var updatedDeck = deckService.GetDeck(editDeckDto.DeckId);
                updatedDeck.Title = editDeckDto.Title;
                updatedDeck.IsShared = editDeckDto.IsShared;
                deckService.EditDeck(updatedDeck);
                return RedirectToAction(nameof(Index), new { ParentId = editDeckDto.ResourceId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult Create(int ResourceId)
        {
            var res = repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId).First();
            var newDeckDto = new CreateDeckDto
            {
                ResourceId = ResourceId,
                ResourceTitle = res.Title,
                SubjectId = res.SubjectId
            };
            return View(newDeckDto);
        }

        [HttpPost] 
        public ActionResult Create(CreateDeckDto createDeckDto)
        {
            try
            {
                var newDeck = new Deck()
                {
                    ResourceId = createDeckDto.ResourceId,
                    Title = createDeckDto.Title,
                    IsShared = createDeckDto.IsShared
                };
                deckService.AddDeck(newDeck);

                return RedirectToAction(nameof(Index), new { ParentId = createDeckDto.ResourceId });
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult View(int DeckId)
        {
            var dck = deckService.GetDeck(DeckId);
            var res = deckService.GetResource(dck.ResourceId);
            DateTime? lastRevisionTime = dck.Revisions.Where(r=>r.IsProperlyClosed == true).OrderByDescending(r => r.EndTime).Select(r => r.EndTime)?.FirstOrDefault();

            var curDeckDto = new DeckViewDto
            {
                DeckId = dck.DeckId,
                ResourceId = dck.ResourceId,
                SubjectId = res.SubjectId,
                IsShared = dck.IsShared,
                Title = dck.Title,
                CardCount = dck.CardCount,
                RevisionCount = dck.RevisionCount,
                ResourceTitle = res.Title,
                LastRevisionDateString = (lastRevisionTime != DateTime.MinValue) ? (string.Concat(lastRevisionTime?.ToShortDateString(), " ", lastRevisionTime?.ToShortTimeString())) : ("Not Revised")
            };
            return View(curDeckDto);
        }

        [HttpGet]
        public ActionResult Revise(int DeckId)
        {
            var rev = deckService.ReviseDeck(DeckId, false);
            ClearRevisionStartTime();

            return RedirectToAction("NextCard", "Card", new { CardId = 0, RevisionId = rev.RevisionId });
        }

        [HttpGet]
        public ActionResult EndRevision(int RevisionId)
        {
            deckService.EndRevision(RevisionId);
            return RedirectToAction("ViewRevisionSummary", "Card", new {RevisionId = RevisionId });
        }

        [HttpGet]
        public ActionResult Quiz(int DeckId)
        {
            var rev = deckService.ReviseDeck(DeckId, true);
            ClearRevisionStartTime();

            return RedirectToAction("NextCard", "Card", new { CardId = 0, RevisionId = rev.RevisionId });
        }

        [HttpGet]
        public ActionResult Delete(int DeckId)
        {
            var dck = deckService.GetDeck(DeckId);
            deckService.DeleteDeck(DeckId);
            return RedirectToAction(nameof(Index), new { ParentId = dck.ResourceId });
        }

        private void ClearRevisionStartTime()
        {
           HttpContext.Session.Remove("revisionSessionStartTime");
        }
    }
}