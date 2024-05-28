namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

/*
 Проверяет на наличие 5 карт идущих подряд путём сверки их рангов (не должны отличаться больше чем на 1).
 Если данное условие выполняется, то метод создает новый объект типа CardsCombination, 
 указывая тип комбинации Стрит и максимальный ранг карты в кобинации.
 */
public class StraightChecker : IChecker {
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    for (int i = 0; i < cards.Count - 1; i++) {
      if ((int)cards[i].Rank - 1 != (int)cards[i + 1].Rank) {
        return null;

      }
    }

    var highRank = cards.Last().Rank;
    return new CardsCombination(CardsCombinationKind.Straight, highRank);
  }
}