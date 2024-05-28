using OOP_ICT.Models;
namespace OOP_ICT.Interfaces;

public interface IStrategy {
  int GetBet(Dictionary<Player, int> bets);
}