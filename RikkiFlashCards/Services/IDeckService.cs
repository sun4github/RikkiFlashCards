using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Services
{
    public interface IDeckService
    {
        public Resource GetResource(int ResourceId);
        #region Card
        //public CardListDto ListCards(int DeckId, String Direction, int Skip = 0, int Take = 10, String SearchFor = "");
        public CardListDto ListCards(int DeckId, int NextPage, int ItemsPerPage, String SearchFor = "");
        public Card GetNextCard(int RevisionId);
        public Card GetNextCard(int RevisionId, int currentCardId);
        public Card GetPreviousCard(int RevisionId, int currentCardId);
        public void AddCard(Card newCard);
        public void EditCard(Card editCard);
        public void DeleteCard(int CardId);
        public Card GetCard(int CardId);

        #endregion Card

        #region Deck
        public Deck GetDeck(int DeckId);
        public void AddDeck(Deck newDeck);
        public void EditDeck(Deck editDeck);
        public void DeleteDeck(int DeckId);
        //public DeckListDto ListDecks(int ResourceId, String Direction, int Skip=0, int Take=10);
        public DeckListDto ListDecks(int ResourceId, int NextPage, int ItemsPerPage);
        public Revision ReviseDeck(int DeckId, bool isExam);
        public void EndRevision(int RevisionId);
        public Revision GetRevision(int RevisonId);

        #endregion Deck
    }
}
