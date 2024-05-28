namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class FourOfAKindChecker : IChecker {
  private const int CARDS_COUNT = 4;

  /*
   Проверяет на наличие 4 карт одного ранга (потому что для Каре нужна комбинация из 4 карт)
   Если данное условие выполняется, метод создает новый объект типа CardsCombination, 
   указывая тип комбинации Каре и ранг карты, которая повторяется 4 раза.
   */
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    if (!cardsCount.ContainsValue(CARDS_COUNT)) {
      return null;
    }

    var highRank = cardsCount.First(pair => pair.Value == CARDS_COUNT).Key;
    return new CardsCombination(CardsCombinationKind.FourOfAKind, highRank);
  }
}