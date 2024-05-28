namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

/*
 Проверяет на наличие одной единственной пары карт.
 Если данное условие выполняется, то метод создает новый объект типа CardsCombination, 
 указывая тип комбинации Пара и ранг карты в паре.
 */
public class PairChecker : IChecker {
  private const int CARDS_COUNT = 2;
  private const int PAIRS_COUNT = 1;

  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (cardsCount.Values.Count(count => count == CARDS_COUNT) != PAIRS_COUNT) {
      return null;
    }
    
    var highRank = cardsCount.Where(pair => pair.Value == CARDS_COUNT).Select(pair => pair.Key).First();
    return new CardsCombination(CardsCombinationKind.Pair, highRank);
  }
}