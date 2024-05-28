namespace OOP_ICT.Models;

public class CardsCombination {
  public readonly CardsCombinationKind Kind;
  public readonly CardRank HighRank;

  public CardsCombination(CardsCombinationKind kind, CardRank highRank) {
    Kind = kind;
    HighRank = highRank;
  }
}