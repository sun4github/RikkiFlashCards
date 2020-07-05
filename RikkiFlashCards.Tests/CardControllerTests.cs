using AnkiFlashCards.Data;
using AnkiFlashCards.Models.DTO;
using AnkiFlashCards.Services.Contracts;
using Moq;
using RikkiFlashCards.Services.Contracts;
using System;
using Xunit;

namespace RikkiFlashCards.Tests
{
    public class CardControllerTests
    {
        private IDeckService _deckService;
        private IRepositoryWrapper _repositoryWrapper;
        private ICodeRenderService _codeRenderService;

        public CardControllerTests()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            var deckService = new Mock<IDeckService>();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            var codeRenderService = new Mock<ICodeRenderService>();

            deckService
                .Setup(d => d.ListCards(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns( new CardListDto
                {

                });



            _deckService = deckService.Object;
            _repositoryWrapper = repositoryWrapper.Object;
            _codeRenderService = codeRenderService.Object;
        }


        [Fact]
        public void Test1()
        {

        }
    }
}
