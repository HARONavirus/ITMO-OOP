namespace OOP_ICT.Models;
using OOP_ICT.Interfaces;

public class PockerGame {
  private readonly PockerBank _bank;
  private readonly PockerTable _table;

  private const int PLAYER_CARDS_COUNT = 2;
  private const int INITIAL_TABLE_CARDS = 3;
  private const int MAX_TABLE_CARDS = 5;

  public PockerGame(PockerTable table, PockerBank bank) {
    _bank = bank;
    _table = table;
  }

  // Добавляет игрока за стол.
  public void Join(Player player) {
    _table.Join(player);
  }

  // Удаляет игрока из-за стола.
  public void Leave(int playerUid) {
    var player = _table.FindPlayer(playerUid);
    if (_bank.GetPlayerBet(player) != null) {
      throw new PlayerHasBetLeftException();
    }

    _table.Leave(player);
  }

  // Позволяет игроку сделать ставку с учётом выбранной стратегии.
  public void MakeBet(int playerUid, IStrategy strategy) {
    _bank.ApplyStrategy(_table.FindPlayer(playerUid), strategy);
  }

  // Добавляет карты из колоды на стол.
  public void LayoutTableCards() {
    if (_table.Hand.Cards.Count == MAX_TABLE_CARDS) {
      throw new TableHasMaxCards();
    }

    var giveCount = _table.Hand.Cards.Count == 0 ? INITIAL_TABLE_CARDS : 1;
    _table.GiveTableCards(giveCount);
  }

  // Удаляет карты со стола.
  public void DropTableCards() {
    _table.DropTableCards();
  }

  // Выдаёт игроку 2 карты
  public void GivePlayerCards(int playerUid) {
    var player = _table.FindPlayer(playerUid);

    if (player.Hand.Cards.Count != 0) {
      throw new PlayerAlreadyHasCards();
    }

    _table.GivePlayerCards(playerUid, PLAYER_CARDS_COUNT);
  }

  /*
   Оценивает ситуацию на столе: определяет лучшие комбинации,
   назначает победителей, распределяет выигрыш между победителями.
   */
  public void EvaluateGameRound() {
    CheckCanEvaluateGameRound();

    Dictionary<Player, CardsCombination> playerCardsCombinations = new();
    _table.getPlayerIds().ForEach(playerId => {
      var player = _table.FindPlayer(playerId);
      playerCardsCombinations[player] = _table.GetPlayerCardsCombination(playerId);
    });

    var bestCombination = playerCardsCombinations.Values
      .OrderBy(_ => (int)_.Kind)
      .ThenBy(_ => (int)_.HighRank)
      .ToList()
      .Last();

    var winners = playerCardsCombinations
      .Where(pair => pair.Value == bestCombination)
      .Select(pair => pair.Key)
      .ToList();

    _bank.AllocateBets(winners);
  }

  /*
   Убеждается, что раунд готов к оценке:
   проверяет достаточно ли карт на столе, и у каждого ли игрока есть карты
   */
  private void CheckCanEvaluateGameRound() {
    if (_table.Hand.Cards.Count < INITIAL_TABLE_CARDS) {
      throw new TableHasMaxCards();
    }

    _table.getPlayerIds().ForEach((playerId) => {
      if (_table.FindPlayer(playerId).Hand.Cards.Count != PLAYER_CARDS_COUNT) {
        throw new PlayerDoesntHaveCards();
      }
    });
  }

}