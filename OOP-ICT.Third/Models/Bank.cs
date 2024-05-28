using OOP_ICT.Interfaces;
namespace OOP_ICT.Models;

public class Bank : IBank {
  
  // Ставки игроков.
  protected readonly Dictionary<Player, int> _bets = new();

  // Получает ставку игрока.
  public int? GetPlayerBet(Player player) {
    return _bets.TryGetValue(player, out var playerBet) ? playerBet : null;
  }

  // Делает ставку от лица игрока.
  public void MakeBet(Player player, int bet) {
    var betException = CanMakeBet(player, bet);
    if (betException != null) {
      throw betException;
    }

    _bets.TryAdd(player, 0);

    player.Chips -= bet;
    _bets[player] += bet;
  }

  // Проверяет может ли игрок сделать ставку.
  public virtual Exception? CanMakeBet(Player player, int bet) {
    return null;
  }
}