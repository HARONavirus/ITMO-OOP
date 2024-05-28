namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class TwoPairChecker : IChecker {
  private const int CARDS_COUNT = 2;
  private const int PAIRS_COUNT = 2;

  // Проверяет на наличие двух пар по 2 карты с одинаковым рангом (то бишь двух пар).
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (cardsCount.Values.Count(count => count == CARDS_COUNT) != PAIRS_COUNT) {
      return null;
    }

    /*
     Если данное условие выполняется, то метод создает новый объект типа CardsCombination, 
     указывая тип комбинации ДвеПары и ранг самой высокой пары.
     */
    var highRank = cardsCount.Where(pair => pair.Value == CARDS_COUNT).MinBy(pair => (int)pair.Key).Key;
    return new CardsCombination(CardsCombinationKind.TwoPair, highRank);
  }
}