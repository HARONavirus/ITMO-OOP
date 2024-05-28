using OOP_ICT.Models;
namespace OOP_ICT.Interfaces;

public interface IGameTable {
  void Join(Player player);
  void Leave(Player player);
  void GivePlayerCards(Player player, int count);
  void GiveTableCards(int count);
  void DropTableCards();
  Player? FindPlayer(int playerUid);
}