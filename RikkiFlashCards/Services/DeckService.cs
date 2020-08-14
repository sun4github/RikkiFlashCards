using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RikkiFlashCards.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public class DeckService : IDeckService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeckService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        public void AddCard(Card newCard)
        {           
            this._repositoryWrapper.Card.Create(newCard);
            this._repositoryWrapper.Save();
        }

        public void AddDeck(Deck newDeck)
        {
            this._repositoryWrapper.Deck.Create(newDeck);
            this._repositoryWrapper.Save();
        }

        public void DeleteCard(int CardId)
        {
            var crd = this._repositoryWrapper.Card.FindByCondition(c => c.CardId == CardId).First();
            this._repositoryWrapper.Card.Delete(this._repositoryWrapper.Card.FindByCondition(c=>c.CardId==CardId).First());
            this._repositoryWrapper.Save();
        }

        public void DeleteDeck(int DeckId)
        {
            var dck = this._repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId).First();
            this._repositoryWrapper.Deck.Delete(this._repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId).First());
            this._repositoryWrapper.Save();
        }

        public void EditCard(Card editCard)
        {
            
            this._repositoryWrapper.Card.Update(editCard);
            this._repositoryWrapper.Save();
        }

        public void EditDeck(Deck editDeck)
        {
            this._repositoryWrapper.Deck.Update(editDeck);
            this._repositoryWrapper.Save();
        }

        public void EndRevision(int RevisionId)
        {
            var rev = this._repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisionId).First();
            rev.EndTime = DateTime.Now;
            rev.IsProperlyClosed = true;
            _repositoryWrapper.Revision.Update(rev);
            _repositoryWrapper.Save();
        }

        public Deck GetDeck(int DeckId)
        {
            return this._repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                    .Include(d=>d.Cards)
                    .Include(d=>d.Revisions)
                    .First();
        }

        public Resource GetResource(int ResourceId)
        {
            return this._repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
                    .First();
        }

        public Card GetCard(int CardId)
        {
            return this._repositoryWrapper.Card.FindByCondition(c => c.CardId == CardId)
                        .Include(c=>c.ImageFiles)
                        .First();
        }

        public Revision GetRevision(int RevisonId)
        {
            return this._repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisonId)
                    .First();
        }

        public Card GetNextCard(int RevisionId)
        {
            return GetACard(RevisionId, 0, true);
        }


        public Card GetNextCard(int RevisionId, int currentCardId)
        {
            return GetACard(RevisionId, currentCardId, true);
        }

        public Card GetPreviousCard(int RevisionId, int currentCardId)
        {
            return GetACard(RevisionId, currentCardId, false);
        }

        private Card GetACard(int RevisionId, int currentCardId = 0, bool isNext = true)
        {
            var additive = (isNext == true) ? 1 : -1;
            var rev = this._repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisionId).First();
            var currentCardIndex = (currentCardId == 0) ? 0 : rev.CardOrderList.FindIndex(ci => ci == currentCardId);
            var nextCardId = (currentCardId == 0)?(rev.CardOrderList[0]) :rev.CardOrderList.ElementAtOrDefault(currentCardIndex + additive);
            

            Card nextCard = null;

            if (nextCardId != 0)
            {
                var nextCardIndex = rev.CardOrderList.FindIndex(ci => ci == nextCardId);
                var lastCardIndex = rev.CardOrderList.FindIndex(ci => ci == rev.LastCardId);

                rev.LastCardId = (nextCardIndex > lastCardIndex) ? (nextCardId) : rev.LastCardId;
                
                rev.RevisedCount = (isNext == true && nextCardIndex > lastCardIndex) ? rev.RevisedCount + 1 : rev.RevisedCount;

                nextCard = this._repositoryWrapper.Card.FindByCondition(c => c.CardId == nextCardId).First();
            }

            _repositoryWrapper.Save();
            return nextCard;
        }

        public CardListDto ListCards(int DeckId, int NextPage, int ItemsPerPage, String SearchFor = "")
        {
            var selDeck = this._repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                        .First();
            IQueryable<Card> cards = this._repositoryWrapper.Card.FindByCondition(c => c.DeckId == DeckId);
            if (!String.IsNullOrWhiteSpace(SearchFor))
            {
                cards = cards.Where(c => c.Front.Contains(SearchFor, StringComparison.OrdinalIgnoreCase) || c.Back.Contains(SearchFor, StringComparison.OrdinalIgnoreCase));
            }

            var cList = new CardListDto
            {
                DeckId = DeckId,
                DeckTitle = selDeck.Title,
                ResourceId = selDeck.ResourceId,
                IsShared = selDeck.IsShared,
                CardCount = cards.Count(),
                Cards = cards.Skip((NextPage-1) * ItemsPerPage).Take(ItemsPerPage),
                SearchFor = SearchFor,
                Skip = (NextPage - 1) * ItemsPerPage,
                Take = ItemsPerPage,
                TotalCardCount = cards.Count()
            };
            return cList;
        }

        public CardListDto SearchCardsInResource(int ResourceId, int NextPage, int ItemsPerPage, String SearchFor = "")
        {
            var selResource = this._repositoryWrapper.Resource
                                .FindByCondition(d => d.ResourceId == ResourceId)
                                .Include(r => r.Decks)
                                .First();
            var selDeckIDs = selResource.Decks.Select(d => d.DeckId);
            IQueryable<Card> cards = this._repositoryWrapper.Card.FindByCondition(c => selDeckIDs.Contains(c.DeckId));

            if (!String.IsNullOrWhiteSpace(SearchFor))
            {
                cards = cards.Where(c => c.Front.Contains(SearchFor, StringComparison.OrdinalIgnoreCase) || c.Back.Contains(SearchFor, StringComparison.OrdinalIgnoreCase));
            }

            var cList = new CardListDto
            {
               
                DeckTitle = "All Decks",
                ResourceId = selResource.ResourceId,
                ResourceTitle = selResource.Title,
                IsShared = false,
                CardCount = cards.Count(),
                Cards = cards.Skip((NextPage - 1) * ItemsPerPage).Take(ItemsPerPage),
                SearchFor = SearchFor,
                Skip = (NextPage - 1) * ItemsPerPage,
                Take = ItemsPerPage,
                TotalCardCount = cards.Count()
            };
            return cList;
        }


        public DeckListViewModel ListDecks(int ResourceId, int NextPage, int ItemsPerPage, string OrderBy = null, string SearchText = "")
        {
            var selResource = this._repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
                        .Include(r => r.Decks)
                            .ThenInclude(d => d.Cards)
                        .Include(r => r.Decks)
                            .ThenInclude(d => d.Revisions)
                        .First();

            var deckViews = selResource.Decks
                                .Where(d => d.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                                .Select(d => new DeckViewDto
                                {
                                    DeckId = d.DeckId,
                                    ResourceId = d.ResourceId,
                                    SubjectId = 0,
                                    ResourceTitle = d.Resource.Title,
                                    Title = d.Title,
                                    IsShared = d.IsShared,
                                    RevisionCount = d.RevisionCount,
                                    CardCount = d.CardCount,
                                    LastRevisionDateString = d.Revisions.OrderByDescending(r => r.EndTime).Select(r => r.EndTime.ToShortDateString()).FirstOrDefault() ?? "Never Revised",
                                    LastRevisionDateTime = d.Revisions.OrderByDescending(r => r.EndTime).Select(r => r.EndTime).FirstOrDefault()
                                })
                                .OrderBy(dv=>(OrderBy != null)? (dv.GetType().GetProperty(OrderBy).GetValue(dv)):(dv.Title))
                                .ToList();

            var dList = new DeckListViewModel
            {
                ResourceId = ResourceId,
                ResourceTitle = selResource.Title,
                SubjectId = selResource.SubjectId,
                Decks = deckViews.Skip((NextPage-1) * ItemsPerPage).Take(ItemsPerPage),
                Skip = (NextPage - 1) *ItemsPerPage,
                Take = ItemsPerPage,
                TotalDeckCount = deckViews.Count()
            };

            return dList;
        }

        public Revision ReviseDeck(int DeckId, bool IsExam)
        {
            var rev = new Revision
            {
                StartTime = DateTime.Now,
                IsExam = IsExam,
                DeckId = DeckId
            };

            var rand = new Random();
            var cardIds = this._repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                            .Include(r => r.Cards)
                            .First().Cards.Select(c=>c.CardId);
            rev.CardOrderList = (IsExam == true)?cardIds.OrderBy(c => rand.Next(0, cardIds.Count())).ToList():cardIds.ToList();
            rev.TotalCardCount = cardIds.Count();
            _repositoryWrapper.Revision.Create(rev);
            _repositoryWrapper.Save();
            return rev;
        }

        public IEnumerable<Card> SearchCards(String SearchFor)
        {
            return this._repositoryWrapper.Card.FindByCondition(c => c.Front.Contains(SearchFor) || c.Back.Contains(SearchFor));
        }

        public void AddFilesToCard(Card card, IEnumerable<IFormFile> files)
        {
            if(files != null)
            {
                foreach(var formFile in files)
                {
                    if(formFile.Length > 0)
                    {
                        //save a file record to get id
                        var newImageFile = new ImageFile
                        {
                            CardId = card.CardId                            
                        };
                        _repositoryWrapper.ImageFile.Create(newImageFile);
                        _repositoryWrapper.Save();

                        //get full path to the final location where we save the file
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath,"ImageFiles",string.Concat(newImageFile.ImageFileId.ToString(),Path.GetExtension(formFile.FileName)));
                        using(var stream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }

                        newImageFile.FileName = Path.GetFileName(filePath);
                        newImageFile.FilePath = filePath;
                        _repositoryWrapper.ImageFile.Update(newImageFile);
                        _repositoryWrapper.Save();
                    }
                }
            }

            //now add files card
        }
    }
}
