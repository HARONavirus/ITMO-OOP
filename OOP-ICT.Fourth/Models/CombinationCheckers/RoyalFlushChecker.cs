namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class RoyalFlushChecker : IChecker {
  private const int CARDS_COUNT = 5;

  // Проверка кол-ва карт (для рояль флеша нужна комбинация из 5 карт).
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (cards.Count != CARDS_COUNT) {
      return null;
    }
    
    // Проверка на то, что первая карта в списке - 10.
    var firstCard = cards.First();
    if (firstCard.Rank != CardRank.Ten) {
      return null;
    }

    // Проверка на одинковую масть.
    if (cards.Any(card => card.Suit != firstCard.Suit)) {
      return null;
    }

    // Проверка на то, что карты идут по рангу начиная с 10 заканчивая тузом.
    for (int i = 0; i < cards.Count - 1; i++) {
      if ((int)cards[i].Rank - 1 != (int)cards[i + 1].Rank) {
        return null;
      }
    }

    return new CardsCombination(CardsCombinationKind.RoyalFlush, CardRank.Ace);
  }
}