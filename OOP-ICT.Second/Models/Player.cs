namespace OOP_ICT.Models;

public class Player {
  
  public readonly int Uid;
  public readonly string Name;
  public readonly CardsHand Hand = new();
  public int Chips;

  public Player(int uid, string name, int initialChips) {
    Uid = uid;
    Name = name;
    Chips = initialChips;
  }

  public override string ToString() {
    return String.Format("Uid: {0}; Name: {1}; Chips: {2}", Uid, Name, Chips);
  }
}