namespace OOP_ICT.Models;

public class Card {
  
  public readonly CardRank Rank;
  
  public readonly CardSuit Suit;

  public Card(CardRank rank, CardSuit suit) {
    Rank = rank;
    Suit = suit;
  }

  public override string ToString() {
    return $"{Rank} {Suit}";
  }
}