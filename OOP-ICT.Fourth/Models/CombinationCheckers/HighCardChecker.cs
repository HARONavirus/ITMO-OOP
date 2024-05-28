namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

/*
 Метод создает новый объект типа CardsCombination, 
 указывая тип комбинации СтаршаяКарта и значение самого большого ранга.
 которое он сумеет найти (тобишь последнюю карту).
 */
public class HighCardChecker : IChecker {
  public CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount) {
    return new CardsCombination(CardsCombinationKind.HighCard, cards.Last().Rank);
  }
}