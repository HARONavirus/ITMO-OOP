namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class StraightFlushChecker : IChecker {
  private const int CARDS_COUNT = 5;

  // Проверяет кол-во карт (для СтритФлеша нужна комбинация из 5 карт).
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (cards.Count != CARDS_COUNT) {
      return null;
    }

    // Проверка на наличие одной масти у всех 5 карт.
    if (cards.Any(card => card.Suit != cards.First().Suit)) {
      return null;
    }

    // Проверка на последовательность карт путем сравнения рангов карт (должны отличаться не больше чем на 1.
    for (int i = 0; i < cards.Count - 1; i++) {
      if ((int)cards[i].Rank - 1 != (int)cards[i + 1].Rank) {
        return null;
      }
    }
    /*
     Если данное условие выполняется, то метод создает новый объект типа CardsCombination, 
     указывая тип комбинации СтритФлеш и максимальный ранг карты в кобинации (ранг последней карты в списке).
    */
    return new CardsCombination(CardsCombinationKind.StraightFlush, cards.Last().Rank);
  }
}