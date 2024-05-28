using OOP_ICT.Interfaces;
namespace OOP_ICT.Models;

public class GameTable : IGameTable {
  
  // Карты на игровом столе.
  public readonly CardsHand Hand = new();

  // Список игроков за столом.
  private readonly List<Player> Players = new();

  // Диллер, раздающий карты.
  private readonly Dealer _dealer = new(new Shuffle<Card>());

  // Добавляет нового игрока за стол.
  public void Join(Player player) {
    Players.Add(player);
  }

  // Удаляет игрока из-за стола.
  public void Leave(Player player) {
    Players.Remove(player);
  }

  // Выдает определенное кол-во карт игроку.
  public void GivePlayerCards(Player player, int count) {
    _dealer.GetCards(count).ForEach((card) => player.Hand.AddCard(card));
  }

  // Выкладывает определенное кол-во карт на стол.
  public void GiveTableCards(int count) {
    _dealer.GetCards(count).ForEach((card) => Hand.AddCard(card));
  }

  // Убирает все карты со стола.
  public void DropTableCards() {
    Hand.DropCards();
  }

  // Ищет игрока за столом по его uid.
  public virtual Player? FindPlayer(int playerUid) {
    return Players.Find(searchPlayer => searchPlayer.Uid == playerUid);
  }

  public List<int> getPlayerIds() {
    return Players.Select((player) => player.Uid).ToList();
  }
}