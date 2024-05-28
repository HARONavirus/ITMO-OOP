namespace OOP_ICT.Models;

using OOP_ICT.Interfaces;

public class CombinationChecker {
  private readonly List<IChecker> _checkers;

  public CombinationChecker(List<IChecker> checkers) {
    _checkers = checkers;
  }

  // Проверяет на наличие комбинации карт.
  public CardsCombination GetCombination(List<Card> cards) {
    cards = SortCards(cards);
    var cardsCount = CountCards(cards);

    foreach (IChecker checker in _checkers) {
      var combination = checker.Check(cards, cardsCount);
      if (combination != null) {
        return combination;
      }
    }

    throw new CheckersNotCoverAllCombinations();
  }

  // Сортирует карты в порядке убывания их ранга.
  private List<Card> SortCards(List<Card> cards) {
    return cards.OrderBy(card => -1 * (int)card.Rank).ToList();
  }

  // Подсчитывает кол-во карт одного ранга в списке (3 Туза, 4 короля итд).
  private Dictionary<CardRank, int> CountCards(List<Card> cards) {
    Dictionary<CardRank, int> cardsCount = new();
    cards.ForEach((card) => {
      if (!cardsCount.ContainsKey(card.Rank)) {
        cardsCount[card.Rank] = 0;
      }
      cardsCount[card.Rank]++;

    });

    return cardsCount;
  }
}