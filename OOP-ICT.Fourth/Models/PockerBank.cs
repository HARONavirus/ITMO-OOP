namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class PockerBank : Bank {
  private const int MIN_BET = 1;

  public PockerBank() : base() { }

  /* Проверяет может ли игрок сделать ставку (проверяет больше ли ставка,
   чем минимальнас ставка и достаточно ли у игрока фишек).
   */
  public override Exception? CanMakeBet(Player player, int bet) {
    if (bet < MIN_BET) {
      return new TooSmallBetException(bet);
    }

    if (player.Chips < bet) {
      return new NotEnoughPlayerChipsException();
    }

    return null;
  }

  // Применяет выбранную игроком стратегию ставок
  public void ApplyStrategy(Player player, IStrategy strategy) {
    MakeBet(player, strategy.GetBet(_bets));
  }

  // Распределяет сделанные ставки между победителями в равной степени
  public void AllocateBets(List<Player> winners) {
    var winBet = GetBetsSum() / winners.Count;
    winners.ForEach(player => player.Chips += winBet);
    _bets.Clear();
  }

  // Подсчитывает сумму всех ставок
  private int GetBetsSum() {
    int betsSum = 0;
    _bets.ToList().ForEach(pair => {
      betsSum += pair.Value;
    });
    return betsSum;
  }
}