namespace OOP_ICT.Models;

public class PlayerNotFoundInGameException : Exception {
  public PlayerNotFoundInGameException(int playerUid) : base(String.Format("Player with uid {0} was not found in game", playerUid)) { }
}

public class NotEnoughPlayerChipsException : Exception {
  public NotEnoughPlayerChipsException() : base("Player has not enough chips to make bet") { }
}

public class CanNotRemovePlayerFromGameException : Exception {
  public CanNotRemovePlayerFromGameException() : base("Player can not be removed from game, because he has some bet left") { }
}

public class NotEnoughGameChipsException : Exception {
  public NotEnoughGameChipsException() : base("Game has not enough chips to cover that bet") { }
}

public class NotEnoughDealerCardsException : Exception {
  public NotEnoughDealerCardsException() : base("Dealer has not enough cards to evaluate player hand") { }
}

public class NotEnoughPlayerCardsException : Exception {
  public NotEnoughPlayerCardsException() : base("Player's hand has less than two cards to be evaluated") { }
}