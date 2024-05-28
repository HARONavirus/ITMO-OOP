namespace OOP_ICT.Models;

public class BlackjackBank : Bank {
  
  // Кол-во денег у банка.
  private int _chips;

  // Множитель выигрыша.
  private const double WIN_MULTIPLIER = 2;

  // Минимально возмодная ставка.
  private const int MIN_BET = 1;

  public BlackjackBank(int initialBankChips) : base() {
    _chips = initialBankChips;
  }

  // Проверяет может ли игрок сделать ставку.
  public override Exception? CanMakeBet(Player player, int bet) {
    if (bet < MIN_BET) {
      return new TooSmallBetException(bet);
    }

    if (player.Chips < bet) {
      return new NotEnoughPlayerChipsException();
    }

    if ((int)Math.Ceiling(bet * WIN_MULTIPLIER) + GetPossibleBetsWon() > _chips) {
      return new NotEnoughBankChipsException();
    }

    return null;
  }

  // Начисляет фишки в случае выигрыша.
  public void CreditTheWin(Player player) {
    CheckHasBet(player);

    int playerWon = (int)Math.Ceiling(WIN_MULTIPLIER * _bets[player]);
    player.Chips += playerWon;
    _chips -= playerWon;
    _bets.Remove(player);
  }

  // Списывает фишки в случае проигрыша.
  public void CreditTheLoss(Player player) {
    CheckHasBet(player);

    _chips += _bets[player];
    _bets.Remove(player);
  }

  // Начисляет ставку обратно в случае ничьи.
  public void CreditTheDraw(Player player) {
    CheckHasBet(player);

    player.Chips += _bets[player];
    _bets.Remove(player);
  }


  // Считает максимальный возможный выигрыш всех ставок на данный момент.
  private int GetPossibleBetsWon() {
    int possibleBetsWon = 0;
    _bets.ToList().ForEach(pair => {
      possibleBetsWon += (int)Math.Ceiling(pair.Value * WIN_MULTIPLIER);
    });
    return possibleBetsWon;
  }

  // Проверяет есть ли ставка у конкретного игрока.
  private void CheckHasBet(Player player) {
    if (GetPlayerBet(player) == null) {
      throw new HasNoBetException();
    }
  }

}