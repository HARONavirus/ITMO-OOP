namespace OOP_ICT.Models;

public class BlackjackGame {
  protected readonly BlackjackBank _bank;
  protected readonly BlackjackGameTable _table;

  // Минимальная сумма в руке у диллера.
  private const int DEALER_MIN_HAND_VALUE = 17;

  // Минимальное кол-во карт в руке игрока.
  private const int PLAYER_MIN_CARDS_COUNT = 2;

  // Сумма очков карт для победы.
  private const int WIN_CARDS_VALUE = 21;

  public BlackjackGame(BlackjackGameTable table, BlackjackBank bank) {
    _bank = bank;
    _table = table;
  }

  // Добавляет нового игрока в игру.
  public void Join(Player player) {
    _table.Join(player);
  }

  // Удаляет игрока из игры.
  public void Leave(int playerUid) {
    var player = _table.FindPlayer(playerUid);
    if (_bank.GetPlayerBet(player) != null) {
      throw new PlayerHasBetLeftException();
    }

    _table.Leave(player);
  }

  // Выдает игроку карту.
  public void GivePlayerCard(int playerUid) {
    _table.GivePlayerCards(playerUid, 1);
  }

  // Выдает диллеру карту.
  public void GiveDealerCard() {
    _table.GiveTableCards(1);
  }

  // Сбрасывает руку диллера.
  public void DropDealerHand() {
    _table.DropTableCards();
  }

  // Делает ставку со стороны игрока.
  public void MakeBet(int playerUid, int bet) {
    _bank.MakeBet(_table.FindPlayer(playerUid), bet);
  }

  // Сравнивает руку игрока и диллера, начисляет фишки в зависимости от результата.
  public BlackjackHandEvaluation EvaluatePlayerHand(int playerUid) {
    var player = _table.FindPlayer(playerUid);

    if (_table.Hand.Value < DEALER_MIN_HAND_VALUE) {
      throw new NotEnoughDealerCardsException();
    }

    if (player.Hand.Cards.Count < PLAYER_MIN_CARDS_COUNT) {
      throw new NotEnoughPlayerCardsException();
    }

    if (player.Hand.Value > WIN_CARDS_VALUE || player.Hand.Value < _table.Hand.Value) {
      _bank.CreditTheLoss(player);
      player.Hand.DropCards();
      return BlackjackHandEvaluation.Loss;
    }

    if (player.Hand.Value == _table.Hand.Value) {
      _bank.CreditTheDraw(player);
      player.Hand.DropCards();
      return BlackjackHandEvaluation.Draw;
    }

    _bank.CreditTheWin(player);
    player.Hand.DropCards();
    return BlackjackHandEvaluation.Won;
  }
}