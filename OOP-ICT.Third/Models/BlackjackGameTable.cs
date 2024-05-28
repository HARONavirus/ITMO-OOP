namespace OOP_ICT.Models;

public class BlackjackGameTable : GameTable {
  
  // Удаляет игрока из-за стола.
  public void Leave(int playerUid) {
    Leave(FindPlayer(playerUid));
  }

  // Выдает определенное кол-во карт игроку.
  public void GivePlayerCards(int playerUid, int count) {
    GivePlayerCards(FindPlayer(playerUid), count);
  }

  // Ищет игрока за столом по его uid.
  public override Player FindPlayer(int playerUid) {
    return base.FindPlayer(playerUid) ?? throw new PlayerNotFoundInGameException(playerUid);
  }
}
