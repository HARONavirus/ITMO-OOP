using System.Text;

namespace OOP_ICT.Models;

public class CardsHand {
  
  // Карты в руке.
  public readonly List<Card> Cards = new();
  
  // Суммарное значение игровой руки.
  public int Value { get; private set; }

  // Добавляет карту в руку и увеличивает общее значение руки.
  public void AddCard(Card newCard) {
    Cards.Add(newCard);
    Value += GetCardValue(newCard);
  }

  // Сбрасывает все карты и обнуляет общее значение руки.
  public void DropCards() {
    Cards.Clear();
    Value = 0;
  }

  // Получает числовое значение карты в зависимости от текущей руки.
  private int GetCardValue(Card card) {
    return card.Rank switch {
      CardRank.Ace => Value > 21 ? 1 : 11,
      CardRank.King or CardRank.Queen or CardRank.Jack or CardRank.Ten => 10,
      CardRank.Nine => 9,
      CardRank.Eight => 8,
      CardRank.Seven => 7,
      CardRank.Six => 6,
      CardRank.Five => 5,
      CardRank.Four => 4,
      CardRank.Three => 3,
      CardRank.Two => 2,
      _ => 0,
    };
  }

  public override string ToString() {
    var result = new StringBuilder();
    Cards.ForEach(card => result.AppendLine(card.ToString()));
    result.AppendLine(String.Format("Hand value: {0}", Value));
    return result.ToString();
  }
}