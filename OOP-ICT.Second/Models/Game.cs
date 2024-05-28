namespace OOP_ICT.Models;

public class Game {
  
  // Список игроков в игре.
  public readonly List<Player> Players = new();
  
  // Диллер, раздающий карты.
  private readonly Dealer _dealer = new(new Shuffle<Card>());
  
  // Рука карт диллера.
  public readonly CardsHand DealerHand = new();
  
  // Запас денег внутри игры.
  private int _houseChips;
  
  // Ставки игроков в текущем раунде.
  private readonly Dictionary<Player, int> _playersBets = new();

  private static readonly double WIN_MULTIPLIER = 2;
  private static readonly int DEALER_MIN_HAND_VALUE = 17;

  public Game(int initialHouseChips) {
    _houseChips = initialHouseChips;
  }

  // Возвращает игрока в игре по его uid.
  private Player FindPlayer(int playerUid) {
    var player = Players.Find(searchPlayer => searchPlayer.Uid == playerUid);
    if (player == null) {
      throw new PlayerNotFoundInGameException(playerUid);
    }

    return player;
  }

  // Добавляет нового игрока в игру.
  public void AddPlayer(Player newPlayer) {
    Players.Add(newPlayer);
    _playersBets.Add(newPlayer, 0);
  }

  // Удаляет игрока из игры.
  public void RemovePlayer(int playerUid) {
    var player = FindPlayer(playerUid);
    Players.Remove(player);

    if (_playersBets[player] != 0) {
      throw new CanNotRemovePlayerFromGameException();
    }
    _playersBets.Remove(player);
  }

  // Выдача карты игроку.
  public void GivePlayerCard(int playerUid) {
    FindPlayer(playerUid).Hand.AddCard(_dealer.GetCards(1)[0]);
  }

  // Выдача карты диллеру.
  public void GiveDealerCard() {
    DealerHand.AddCard(_dealer.GetCards(1)[0]);
  }

  // Сброс руки диллера.
  public void DropDealerHand() {
    DealerHand.DropCards();
  }

  // Делает ставку со стороны игрока.
  public void MakePlayerBet(int playerUid, int bet) {
    var player = FindPlayer(playerUid);

    if (player.Chips < bet) {
      throw new NotEnoughPlayerChipsException();
    }

    var possiblePlayersWonBet = (int)Math.Ceiling(bet * WIN_MULTIPLIER);
    _playersBets.ToList().ForEach(pair => {
      possiblePlayersWonBet += (int)Math.Ceiling(pair.Value * WIN_MULTIPLIER);
    });

    if (possiblePlayersWonBet > _houseChips) {
      throw new NotEnoughGameChipsException();
    }

    _playersBets[player] += bet;
    player.Chips -= bet;
  }

  // Сравнивение руки игрока и диллера и начисление выигрыша.
  public bool EvaluatePlayerHand(int playerUid) {
    var player = FindPlayer(playerUid);

    if (DealerHand.Value < DEALER_MIN_HAND_VALUE) {
      throw new NotEnoughDealerCardsException();
    }

    if (player.Hand.Cards.Count < 2) {
      throw new NotEnoughPlayerCardsException();
    }

    if (player.Hand.Value > 21 || player.Hand.Value < DealerHand.Value) {
      _houseChips += _playersBets[player];
      _playersBets[player] = 0;
      player.Hand.DropCards();
      return false;
    }

    if (player.Hand.Value == DealerHand.Value) {
      player.Chips += _playersBets[player];
      _playersBets[player] = 0;
      player.Hand.DropCards();
      return false;
    }

    player.Chips += (int)Math.Ceiling(WIN_MULTIPLIER * _playersBets[player]);
    _playersBets[player] = 0;
    player.Hand.DropCards();
    return true;
  }

  // Пополнение банка казино.
  public void AddHouseChips(int chips) {
    _houseChips += chips;
  }
}