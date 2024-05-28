namespace OOP_ICT.Models;

public class PlayerAlreadyHasCards : Exception {
  public PlayerAlreadyHasCards() : base("Player already has cards") { }
}

public class PlayerDoesntHaveCards : Exception {
  public PlayerDoesntHaveCards() : base("Player doesn't have cards to play round") { }
}

public class TableHasMaxCards : Exception {
  public TableHasMaxCards() : base("Table already have max cards") { }
}