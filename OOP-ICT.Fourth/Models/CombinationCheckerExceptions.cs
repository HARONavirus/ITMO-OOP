namespace OOP_ICT.Models;

public class CheckersNotCoverAllCombinations : Exception {
  public CheckersNotCoverAllCombinations() : base("There is no checker that can handle this cards") { }
}