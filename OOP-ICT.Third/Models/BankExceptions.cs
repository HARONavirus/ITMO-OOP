namespace OOP_ICT.Models;

public class NotEnoughBankChipsException : Exception {
  public NotEnoughBankChipsException() : base("Bank has not enough chips to handle this bet") { }
}

public class TooSmallBetException : Exception {
  public TooSmallBetException(int bet) : base(string.Format("The bet is to low. The minimum bet is {0}", bet)) { }
}

public class HasNoBetException : Exception {
  public HasNoBetException() : base("Player has no bet") { }
}