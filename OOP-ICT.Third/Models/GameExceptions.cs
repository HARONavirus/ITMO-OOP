namespace OOP_ICT.Models;

public class PlayerHasBetLeftException : Exception {
  public PlayerHasBetLeftException() : base("Player has some bet left") { }
}