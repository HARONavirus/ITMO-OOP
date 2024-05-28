using Xunit;
using OOP_ICT.Models;
namespace OOP_ICT.CardsDealer.Tests;

public class Tests {
  // Вспомогательный метод создания дилера.
  private static Dealer CreateDealer() {
    var shuffle = new Shuffle<Card>();
    return new Dealer(shuffle);
  }

  // Вспомогательный класс, позволяющий полоучить колоду карт до перемешивания.
  private class TestsCardDeck : CardDeck { }

  // Проверяем корректность сборки и запуска тестов.
  [Fact]
  public void CheckTestIsWorking_CorrectBuild() {
    Assert.True(true);
  }


  // Проверяем, колоду на уникальность 52 карт.
  [Fact]
  public void CheckCardDeckInitialization() {
    var cards = CreateDealer().GetCards(CardDeck.CARDS_COUNT);
    var uniqueCards = new HashSet<string>();

    foreach (Card card in cards) {
      uniqueCards.Add(card.ToString());
    }

    Assert.Equal(CardDeck.CARDS_COUNT, uniqueCards.Count);
  }

  // Проверяем работу шафлера путем сравнения оригинальной колоды и перемешанной
  [Fact]
  public void CheckCardDeckNotDefaultOrder() {
    var shuffledCards = CreateDealer().GetCards(CardDeck.CARDS_COUNT);
    var defaultCardDeck = new TestsCardDeck();

    Assert.NotEqual(shuffledCards, defaultCardDeck.Cards);
  }
}