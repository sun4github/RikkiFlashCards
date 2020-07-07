using AnkiFlashCards.Controllers;
using AnkiFlashCards.Data;
using AnkiFlashCards.Models.Domain;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services.Contracts;
using Moq;
using RikkiFlashCards.Services.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace RikkiFlashCards.Tests
{
    public class CardControllerTests
    {
        private Mock<IDeckService> _deckService;
        private Mock<IRepositoryWrapper> _repositoryWrapper;
        private Mock<ICodeRenderService> _codeRenderService;

        public CardControllerTests()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            _deckService = new Mock<IDeckService>();
            _repositoryWrapper = new Mock<IRepositoryWrapper>();
            _codeRenderService = new Mock<ICodeRenderService>();

            var mockCardListDto = new CardListDto
            {
                CardCount = 2,
                Cards = new List<Card> {
                        new Card
                        {
                            CardId = 1,
                            Front = "f1",
                            Back = "b1"
                        }
                        ,new Card
                        {
                            CardId = 2,
                            Front = "f2",
                            Back = "b2"
                        }
                    },
                DeckId = 1,
                DeckTitle = "Dummy Deck",
                IsShared = false,
                ResourceId = 1,
                ResourceTitle = "Dummy Resource",
                Skip = 0,
                Take = 2,
                TotalCardCount = 20

            };

            _deckService
                .Setup(d => d.ListCards(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(mockCardListDto)
                .Verifiable() ;

            _deckService
                .Setup(ds => ds.SearchCardsInResource(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(mockCardListDto)
                .Verifiable();

        }


        [Fact]
        public void Index_Should_ReturnCardListDto()
        {
            //Arrange
            var _sut = new CardController(_deckService.Object, _repositoryWrapper.Object, _codeRenderService.Object);

            //Act
            var result = _sut.Index(1).ViewData.Model;

            //Assert
            Assert.IsType<CardListDto>(result);
            _deckService.Verify(ds => ds.ListCards(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SearchCardsInAllDecks_Should_ReturnCardListDto()
        {
            //Arrange
            var _sut = new CardController(_deckService.Object, _repositoryWrapper.Object, _codeRenderService.Object);

            //Act
            var result = _sut.SearchCardsInAllDecks(1).ViewData.Model;

            //Assert
            Assert.IsType<CardListDto>(result);
            _deckService.Verify(ds => ds.SearchCardsInResource(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}
