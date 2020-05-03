﻿using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public class DeckService : IDeckService
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public DeckService(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }
        public void AddCard(Card newCard)
        {
            this.repositoryWrapper.Card.Create(newCard);
            this.repositoryWrapper.Save();
        }

        public void AddDeck(Deck newDeck)
        {
            this.repositoryWrapper.Deck.Create(newDeck);
            this.repositoryWrapper.Save();
        }

        public void DeleteCard(int CardId)
        {
            var crd = this.repositoryWrapper.Card.FindByCondition(c => c.CardId == CardId).First();
            this.repositoryWrapper.Card.Delete(this.repositoryWrapper.Card.FindByCondition(c=>c.CardId==CardId).First());
            this.repositoryWrapper.Save();
        }

        public void DeleteDeck(int DeckId)
        {
            var dck = this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId).First();
            this.repositoryWrapper.Deck.Delete(this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId).First());
            this.repositoryWrapper.Save();
        }

        public void EditCard(Card editCard)
        {
            
            this.repositoryWrapper.Card.Update(editCard);
            this.repositoryWrapper.Save();
        }

        public void EditDeck(Deck editDeck)
        {
            this.repositoryWrapper.Deck.Update(editDeck);
            this.repositoryWrapper.Save();
        }

        public void EndRevision(int RevisionId)
        {
            var rev = this.repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisionId).First();
            rev.EndTime = DateTime.Now;
            rev.IsProperlyClosed = true;
            repositoryWrapper.Revision.Update(rev);
            repositoryWrapper.Save();
        }

        public Deck GetDeck(int DeckId)
        {
            return this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                    .Include(d=>d.Cards)
                    .Include(d=>d.Revisions)
                    .First();
        }

        public Resource GetResource(int ResourceId)
        {
            return this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
                    .First();
        }

        public Card GetCard(int CardId)
        {
            return this.repositoryWrapper.Card.FindByCondition(c => c.CardId == CardId)
                    .First();
        }

        public Revision GetRevision(int RevisonId)
        {
            return this.repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisonId)
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
            var rev = this.repositoryWrapper.Revision.FindByCondition(r => r.RevisionId == RevisionId).First();
            var currentCardIndex = (currentCardId == 0) ? 0 : rev.CardOrderList.FindIndex(ci => ci == currentCardId);
            var nextCardId = (currentCardId == 0)?(rev.CardOrderList[0]) :rev.CardOrderList.ElementAtOrDefault(currentCardIndex + additive);
            

            Card nextCard = null;

            if (nextCardId != 0)
            {
                var nextCardIndex = rev.CardOrderList.FindIndex(ci => ci == nextCardId);
                var lastCardIndex = rev.CardOrderList.FindIndex(ci => ci == rev.LastCardId);

                rev.LastCardId = (nextCardIndex > lastCardIndex) ? (nextCardId) : rev.LastCardId;
                
                rev.RevisedCount = (isNext == true && nextCardIndex > lastCardIndex) ? rev.RevisedCount + 1 : rev.RevisedCount;

                nextCard = this.repositoryWrapper.Card.FindByCondition(c => c.CardId == nextCardId).First();
            }

            repositoryWrapper.Save();
            return nextCard;
        }

        //public CardListDto ListCards(int DeckId, String Direction, int Skip = 0, int Take = 10, String SearchFor = "")
        //{
        //    var selDeck = this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
        //                .First();
        //    IQueryable<Card> cards = this.repositoryWrapper.Card.FindByCondition(c => c.DeckId == DeckId);
        //    if (!String.IsNullOrWhiteSpace(SearchFor))
        //    {
        //        cards = cards.Where(c => c.Front.Contains(SearchFor, StringComparison.OrdinalIgnoreCase) || c.Back.Contains(SearchFor, StringComparison.OrdinalIgnoreCase));
        //    }
        //    //cards = cards.ToList<Card>();
            
        //    Skip = NavigationHelper.CalculateSkip(Direction, Skip, Take, cards.Count());

        //    var cList = new CardListDto
        //    {
        //        DeckId = DeckId,
        //        DeckTitle = selDeck.Title,
        //        ResourceId = selDeck.ResourceId,
        //        IsShared = selDeck.IsShared,
        //        CardCount = (!String.IsNullOrWhiteSpace(SearchFor)) ?(cards.Count()):(selDeck.CardCount),
        //        Cards = cards.Skip(Skip).Take(Take),
        //        SearchFor = SearchFor,
        //        Skip = Skip,
        //        Take = Take
        //    };
        //    return cList;
        //}

        public CardListDto ListCards(int DeckId, int NextPage, int ItemsPerPage, String SearchFor = "")
        {
            var selDeck = this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                        .First();
            IQueryable<Card> cards = this.repositoryWrapper.Card.FindByCondition(c => c.DeckId == DeckId);
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

        //public DeckListDto ListDecks(int ResourceId, String Direction, int Skip = 0, int Take = 5)
        //{
        //    var selResource = this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
        //                .Include(r => r.Decks)
        //                    .ThenInclude(d=>d.Cards)
        //                .Include(r=>r.Decks)
        //                    .ThenInclude(d=>d.Revisions)
        //                .First();

        //    var decks = selResource.Decks.OrderBy(d => d.Title)
        //                .Select(d => new DeckViewDto
        //                {
        //                    DeckId = d.DeckId,
        //                    ResourceId = d.ResourceId,
        //                    SubjectId = 0,
        //                    ResourceTitle = d.Resource.Title,
        //                    Title = d.Title,
        //                    IsShared = d.IsShared,
        //                    RevisionCount = d.RevisionCount,
        //                    CardCount = d.CardCount,
        //                    LastRevisionDateTime = d.Revisions.OrderByDescending(r=>r.EndTime).Select(r=>r.EndTime.ToShortDateString()).FirstOrDefault()??"Never Revised"
        //                }).ToList();
        //    Skip = NavigationHelper.CalculateSkip(Direction, Skip, Take, decks.Count());

        //    var dList = new DeckListDto
        //    {
        //        ResourceId = ResourceId,
        //        ResourceTitle = selResource.Title,
        //        SubjectId = selResource.SubjectId,
        //        Decks = decks.Skip(Skip).Take(Take),
        //        Skip = Skip,
        //        Take = Take
        //    };
        //    return dList;
        //}


        public DeckListDto ListDecks(int ResourceId, int NextPage, int ItemsPerPage)
        {
            var selResource = this.repositoryWrapper.Resource.FindByCondition(r => r.ResourceId == ResourceId)
                        .Include(r => r.Decks)
                            .ThenInclude(d => d.Cards)
                        .Include(r => r.Decks)
                            .ThenInclude(d => d.Revisions)
                        .First();

            var decks = selResource.Decks.OrderBy(d => d.Title)
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
                            LastRevisionDateTime = d.Revisions.OrderByDescending(r => r.EndTime).Select(r => r.EndTime.ToShortDateString()).FirstOrDefault() ?? "Never Revised"
                        }).ToList();

            var dList = new DeckListDto
            {
                ResourceId = ResourceId,
                ResourceTitle = selResource.Title,
                SubjectId = selResource.SubjectId,
                Decks = decks.Skip((NextPage-1) * ItemsPerPage).Take(ItemsPerPage),
                Skip = (NextPage - 1) *ItemsPerPage,
                Take = ItemsPerPage,
                TotalDeckCount = decks.Count()
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
            var cardIds = this.repositoryWrapper.Deck.FindByCondition(d => d.DeckId == DeckId)
                            .Include(r => r.Cards)
                            .First().Cards.Select(c=>c.CardId);
            rev.CardOrderList = (IsExam == true)?cardIds.OrderBy(c => rand.Next(0, cardIds.Count())).ToList():cardIds.ToList();
            rev.TotalCardCount = cardIds.Count();
            repositoryWrapper.Revision.Create(rev);
            repositoryWrapper.Save();
            return rev;
        }

        public IEnumerable<Card> SearchCards(String SearchFor)
        {
            return this.repositoryWrapper.Card.FindByCondition(c => c.Front.Contains(SearchFor) || c.Back.Contains(SearchFor));
        }
    }
}
