namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class ThreeOfAKindChecker : IChecker {
  private const int CARDS_COUNT = 3;

  // Проверяет на наличие 3 карт одного ранга.
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (!cardsCount.ContainsValue(CARDS_COUNT)) {
      return null;
    }

    /*
     Если данное условие выполняется, то метод создает новый объект типа CardsCombination, 
     указывая тип комбинации Сет и ранг карты в тройке.
     */
    var highRank = cardsCount.First(pair => pair.Value == CARDS_COUNT).Key;
    return new CardsCombination(CardsCombinationKind.ThreeOfAKind, highRank);
  }
}