using OOP_ICT.Interfaces;

namespace OOP_ICT.Models;

public class Dealer : IDealer {
  private readonly IShuffle<Card> _shuffle;
  
  // Экземпляр класса CardDeck, который сейчас есть у дилера.
  private CardDeck _cardDeck;

  public Dealer(IShuffle<Card> shuffle) {
    _shuffle = shuffle;
    ResetCardDeck();
  }

  // Создает новый экземпляр класса CardDeck и перемешивает внутри него карты.
  private void ResetCardDeck() {
    _cardDeck = new CardDeck();
    _cardDeck.Cards = _shuffle.ShuffleList(_cardDeck.Cards);
  }
  
  // Позволяет взять любое кол-во карт сверху колоды.
  public List<Card> GetCards(int takeCount) {
    var takenCards = new List<Card>(takeCount);
    while (takenCards.Count != takeCount) {
      takenCards.Add(Take());
    }
    return takenCards;
  }
  
  /*
  Позволяет взять одну карту сверху колоды. Если в колоде закончились карты,
  диллер создаст новый инстанс колоды, перемешает ее и возьмет карту оттуда.
  */
  private Card Take() {
    if (_cardDeck.Cards.Count == 0) {
      ResetCardDeck();
    }

    var card = _cardDeck.Cards.First();
    _cardDeck.Cards.RemoveAt(0);
    return card;
  }
}