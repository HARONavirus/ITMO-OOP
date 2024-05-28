namespace OOP_ICT.Models;

public class PockerTable : GameTable {
  private readonly CombinationChecker _combinationChecker;

  public PockerTable(CombinationChecker combinationChecker) : base() {
    _combinationChecker = combinationChecker;
  }

  public void Leave(int playerUid) {
    Leave(FindPlayer(playerUid));
  }

  public void GivePlayerCards(int playerUid, int count) {
    GivePlayerCards(FindPlayer(playerUid), count);
  }

  public override Player FindPlayer(int playerUid) {
    return base.FindPlayer(playerUid) ?? throw new PlayerNotFoundInGameException(playerUid);
  }

  public CardsCombination GetPlayerCardsCombination(int playerUid) {
    var player = FindPlayer(playerUid);
    var cards = GetPlayerCardsWithTable(player);
    return _combinationChecker.GetCombination(cards);
  }

  private List<Card> GetPlayerCardsWithTable(Player player) {
    var cards = player.Hand.Cards.ToList();
    cards.AddRange(Hand.Cards);
    return cards;
  }
}
