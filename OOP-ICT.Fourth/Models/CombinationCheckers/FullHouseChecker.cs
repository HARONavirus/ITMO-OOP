namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class FullHouseChecker : IChecker {
  private const int MAX_CARDS_COUNT = 3;
  private const int MIN_CARDS_COUNT = 2;

  /*
   Проверяет на наличие 3 карт с одинаковым рангом и 2 карт с одинаковым рангом.
   Если данное условие выполняется, метод создает новый объект типа CardsCombination, 
   указывая тип комбинации ФуллХаус и ранг карты со страшей комбинации (Сета).
   */
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {

    if (!cardsCount.ContainsValue(MAX_CARDS_COUNT) || !cardsCount.ContainsValue(MIN_CARDS_COUNT)) {
      return null;
    }

    var highRank = cardsCount.First(pair => pair.Value == MAX_CARDS_COUNT).Key;
    return new CardsCombination(CardsCombinationKind.FullHouse, highRank);
  }
}