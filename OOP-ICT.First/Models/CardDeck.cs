namespace OOP_ICT.Models;

public class CardDeck {
 
  public static readonly int CARDS_COUNT = 52;
  
  public List<Card> Cards { get; set; }


  protected internal CardDeck() {
    SetUpDeck();
  }

 // Заполняем колоду (список) стандартным набором карт
  private void SetUpDeck() {
    Cards = new List<Card>(CARDS_COUNT);

    foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit))) {
      foreach (CardRank rank in Enum.GetValues(typeof(CardRank))) {
        Cards.Add(new Card(rank, suit));
      }
    }
  }
}