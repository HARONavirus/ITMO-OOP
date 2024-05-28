using OOP_ICT.Models;
namespace OOP_ICT.Interfaces;

public interface IBank {
  int? GetPlayerBet(Player player);
  void MakeBet(Player player, int bet);
  Exception? CanMakeBet(Player player, int bet);
}