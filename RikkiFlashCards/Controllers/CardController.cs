using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnkiFlashCards.Services;

namespace AnkiFlashCards.Controllers
{
    public class CardController : Controller
    {
        private readonly IDeckService deckService;
        private IRepositoryWrapper repositoryWrapper;
        private int ItemsPerPage = 10;

        public CardController(IDeckService deckService, IRepositoryWrapper repositoryWrapper)
        {
            this.deckService = deckService;
            this.repositoryWrapper = repositoryWrapper;
        }

        //[HttpGet]
        //[HttpPost]
        //public ViewResult Index(int DeckId, String Direction, int Skip = 0, int Take = 10, String SearchFor = "")
        //{
        //    var cardList = deckService.ListCards(DeckId, Direction, Skip, Take, SearchFor);
        //    return View(cardList);
        //}

        [HttpGet]
        [HttpPost]
        public ViewResult Index(int DeckId, int NextPage=1, String SearchFor = "")
        {
            var cardList = deckService.ListCards(DeckId, NextPage, ItemsPerPage, SearchFor);
            ViewBag.ItemsPerPage = ItemsPerPage;
            return View(cardList);
        }

        [HttpGet]
        public RedirectToActionResult Clone(int CardId)
        {
            var crd = deckService.GetCard(CardId);
            var dck = deckService.GetDeck(crd.DeckId);
            var newCardDto = new CreateCardDto
            {
                DeckId = dck.DeckId,
                Front = crd.Front,
                Back = crd.Back,
                Level = crd.Level
            };
            return RedirectToAction("Create",  newCardDto );
        }

        [HttpGet]
        public ViewResult Create(int DeckId, CreateCardDto createCardDto)
        {
            
            if (String.IsNullOrWhiteSpace(createCardDto.Front) && String.IsNullOrWhiteSpace(createCardDto.Back))
            {
                var dck = repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId).First();
                var aNewCardDto = new CreateCardDto
                {
                    DeckId = DeckId,
                    DeckTitle = dck.Title
                };
                ModelState.Clear();
                return View(aNewCardDto);
            }
            else
            {
                return View(createCardDto);
            }
        }

      

        [HttpPost]
        public ActionResult Create(CreateCardDto createCardDto)
        {
            try
            {
                var newCard = new Card()
                {
                    DeckId = createCardDto.DeckId,
                    Front = createCardDto.Front,
                    Back = createCardDto.Back,
                    Level = createCardDto.Level
                };

                newCard = CodeRenderHelper.DecodeCardForCode(newCard);
                deckService.AddCard(newCard);

                return RedirectToAction(nameof(View), new { CardId = newCard.CardId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [HttpGet]
        public ViewResult Edit(int CardId)
        {
            
            var crd = deckService.GetCard(CardId);
            var dck = deckService.GetDeck(crd.DeckId);
            var ec = new EditCardDto
            {
                DeckId = dck.DeckId,
                DeckTitle = dck.Title,
                Front = crd.Front,
                Back = crd.Back,
                Level = crd.Level,
                CardId = CardId
            };
            return View(ec);
        }

        [HttpPost]
        public ActionResult Edit(EditCardDto editCardDto)
        {
            try
            {
                var updatedCard = deckService.GetCard(editCardDto.CardId);
                updatedCard.Front = editCardDto.Front;
                updatedCard.Back = editCardDto.Back;
                updatedCard = CodeRenderHelper.DecodeCardForCode(updatedCard);
                updatedCard.Level = editCardDto.Level;
                deckService.EditCard(updatedCard);
                return RedirectToAction(nameof(View), new { CardId = updatedCard.CardId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult View(int CardId)
        {
            var crd = deckService.GetCard(CardId);
            crd = CodeRenderHelper.EncodeCardForCode(crd);
            var dck = deckService.GetDeck(crd.DeckId);
            var curCardDto = this.GetCardViewDto(crd, dck);
            return View(curCardDto);
        }


        private CardViewDto GetCardViewDto(Card currentCard, Deck dck, bool isExam = false)
        {
            //currentCard = CodeRenderHelper.RestoreNewLine(currentCard);
            var curCardDto = new CardViewDto
            {
                CardId = currentCard.CardId,
                DeckId = dck.DeckId,
                DeckTitle = dck.Title,
                Front = currentCard.Front,  
                Back = currentCard.Back,   
                Level = currentCard.Level,
                IsExam = isExam
            };



            return curCardDto;
        }

        [HttpGet]
        public ViewResult NextCard(int CardId, int RevisionId)
        {
           
            var nextCard = deckService.GetNextCard(RevisionId, CardId);
           

            if (nextCard != null)
            {
                nextCard = CodeRenderHelper.EncodeCardForCode(nextCard);
                var rev = deckService.GetRevision(RevisionId);
                var dck = deckService.GetDeck(nextCard.DeckId);
                var curCardDto = new CardRevisionViewDto(this.GetCardViewDto(nextCard, dck, rev.IsExam));
                curCardDto.RevisionId = RevisionId;
                curCardDto.TotalRevisedCount = rev.RevisedCount;
                curCardDto.TotalCardCount = rev.TotalCardCount;
                return View("ViewExam", curCardDto);
            }
            else
            {
                deckService.EndRevision(RevisionId);
                return ViewRevisionSummary(RevisionId);
            }
               

        }

        [HttpGet]
        public ViewResult PrevCard(int CardId, int RevisionId)
        {
            var nextCard = deckService.GetPreviousCard(RevisionId, CardId);
           

            if (nextCard != null)
            {
                nextCard = CodeRenderHelper.EncodeCardForCode(nextCard);
                var rev = deckService.GetRevision(RevisionId);
                var dck = deckService.GetDeck(nextCard.DeckId);
                var curCardDto = new CardRevisionViewDto(this.GetCardViewDto(nextCard, dck, rev.IsExam));
                curCardDto.RevisionId = RevisionId;
                curCardDto.TotalRevisedCount = rev.RevisedCount;
                curCardDto.TotalCardCount = rev.TotalCardCount;
                return View("ViewExam", curCardDto);
            }
            else
            {
                var currentCard = deckService.GetCard(CardId);
                currentCard = CodeRenderHelper.EncodeCardForCode(currentCard);
                var rev = deckService.GetRevision(RevisionId);
                var dck = deckService.GetDeck(currentCard.DeckId);
                var curCardDto = new CardRevisionViewDto(this.GetCardViewDto(currentCard, dck, rev.IsExam));
                curCardDto.RevisionId = RevisionId;
                curCardDto.TotalRevisedCount = rev.RevisedCount;
                curCardDto.TotalCardCount = rev.TotalCardCount;
                return View("ViewExam", curCardDto);
            }
                
        }

        public ViewResult ViewRevisionSummary(int RevisionId)
        {
            var rev = deckService.GetRevision(RevisionId);
            var dck = deckService.GetDeck(rev.DeckId);
            var revDto = new RevisionViewDto
            {
                DeckId = rev.DeckId,
                DeckTitle = dck.Title,
                StartTime = rev.StartTime,
                EndTime = rev.EndTime,
                RevisionId = rev.RevisionId,
                IsExam = rev.IsExam,
                ResourceId = dck.ResourceId,
                RevisedCount = rev.RevisedCount
            };
            return View("ViewRevisionSummary",revDto);
        }

        [HttpGet]
        public ActionResult Delete(int CardId)
        {
            var crd = deckService.GetCard(CardId);
            deckService.DeleteCard(CardId);
            return RedirectToAction(nameof(Index), new { DeckId = crd.DeckId });
        }

    }
}