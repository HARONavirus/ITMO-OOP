namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

/*
 Проверка на то, что все карты одной масти.
 Если все карты имеют одинаковую масть, метод создает новый объект типа CardsCombination, 
 указывая тип комбинации Флеш и ранг самой старшей карты.
 */
public class FlushChecker : IChecker {
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    CardSuit suit = cards.First().Suit;
    if (!cards.All(card => card.Suit == suit)) {
      return null;
    }

    return new CardsCombination(CardsCombinationKind.Flush, cards.Last().Rank);
  }
}