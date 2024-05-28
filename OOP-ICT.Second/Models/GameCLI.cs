using System.Text;

namespace OOP_ICT.Models;

enum GameAction {
  StarRound = 1,
  ListPlayers = 2,
  AddPlayer = 3,
  RemovePlayer = 4,
}

public class GameCLI {
  private readonly Game _game;

  public GameCLI(int initialHouseChips) {
    _game = new Game(initialHouseChips);
  }

  // Запрашивает у пользователя число, пока оно не будет соответствовать условию acceptCondition.
  private int AcceptUserInt(string? inputText = null, Func<int, bool>? acceptCondition = null, string? acceptErrorText = null) {
    inputText ??= "";

    while (true) {
      Console.Write(inputText);
      var input = Console.ReadLine();
      if (input == null || input.Length == 0 || !int.TryParse(input, out int inputInt) || (acceptCondition != null && !acceptCondition(inputInt))) {
        if (acceptErrorText != null) {
          Console.WriteLine(acceptErrorText);
        }
        continue;
      }

      return inputInt;
    }
  }

  // Запрашивает у пользователя верное значение GameAction.
  private GameAction AcceptUserAction() {
    var avaliableActions = GetAvaliableActions();

    while (true) {
      var rawAction = AcceptUserInt("Your action: ", (rawAction) => Enum.IsDefined(typeof(GameAction), (GameAction)rawAction) && avaliableActions.Contains((GameAction)rawAction), "Incorrect action passed");
      return (GameAction)rawAction;
    }
  }

  // Возвращает текстовое значение для GameAction.
  private string GetActionText(GameAction action) {
    return action switch {
      GameAction.StarRound => "Start game round",
      GameAction.ListPlayers => "List avaliable players",
      GameAction.AddPlayer => "Add new player",
      GameAction.RemovePlayer => "Remove player from game",
      _ => "",
    };
  }

  // Возвращает разрешенные действия для текущего состояния игры.
  private HashSet<GameAction> GetAvaliableActions() {
    var avaliableActions = new HashSet<GameAction>();

    foreach (var action in Enum.GetValues(typeof(GameAction)).Cast<GameAction>()) {
      switch (action) {
        case GameAction.StarRound:
        case GameAction.ListPlayers:
        case GameAction.RemovePlayer:
          if (_game.Players.Count == 0) {
            continue;
          }
          break;

        case GameAction.AddPlayer:
          break;
      }
      avaliableActions.Add(action);
    }

    return avaliableActions;
  }

  // Возвращает текст игрового меню.
  private string GetActionMenu() {
    var result = new StringBuilder();
    result.AppendLine("Actons:");

    var avaliableActions = GetAvaliableActions();

    foreach (var action in Enum.GetValues(typeof(GameAction)).Cast<GameAction>()) {
      if (avaliableActions.Contains(action)) {
        result.AppendLine(String.Format("* {0}({1})", GetActionText(action), (int)action));
      }
    }

    return result.ToString();
  }

  // Возвращает список игроков в текстовом виде.
  private string GetPlayersList() {
    var result = new StringBuilder();

    result.AppendLine("Players:");
    _game.Players.ForEach((player) => result.AppendLine(player.ToString()));
    return result.ToString();
  }

  // Возвращает список карт игрока в текством виде.
  private string GetPlayerCards(Player player) {
    var result = new StringBuilder();

    result.AppendLine(String.Format("Player(uid: {0}) hand:", player.Uid));
    result.AppendLine(player.Hand.ToString());
    return result.ToString();
  }

  private string GetDelimiter() {
    return String.Concat(Enumerable.Repeat("-", 25));
  }

  // Запускает проведение игрового раунда.
  private void PlayGameRound() {
    var inRoundPlayers = new List<Player>();

    _game.Players.ForEach((player) => {
      var playerBet = AcceptUserInt(string.Format("Player(uid: {0}) bet: ", player.Uid), (rawBet) => rawBet >= 0 && rawBet < player.Chips, "Incorrect bet value");
      if (playerBet == 0) {
        Console.WriteLine("Player(uid: {0}) skipped round", player.Uid);
        return;
      }

      _game.MakePlayerBet(player.Uid, playerBet);
      inRoundPlayers.Add(player);
    });

    if (inRoundPlayers.Count == 0) {
      Console.WriteLine("All players skipped round");
      return;
    }

    inRoundPlayers.ForEach((player) => {
      _game.GivePlayerCard(player.Uid);
      _game.GivePlayerCard(player.Uid);
    });

    _game.GiveDealerCard();

    inRoundPlayers.ForEach((player) => {
      Console.Write(GetPlayerCards(player));

      while (true) {
        var needMore = AcceptUserInt("Need more cards (1 - yes, 0 - no): ", (rawNeedMode) => rawNeedMode == 1 || rawNeedMode == 0, "Value can be only 1 or 0");
        if (needMore == 1) {
          _game.GivePlayerCard(player.Uid);
          Console.Write(GetPlayerCards(player));
          continue;
        }
        break;
      }
    });

    while (_game.DealerHand.Value < 16) {
      _game.GiveDealerCard();
    }

    Console.WriteLine("Dealer hand:");
    Console.WriteLine(_game.DealerHand.ToString());

    inRoundPlayers.ForEach((player) => {
      var playerWon = _game.EvaluatePlayerHand(player.Uid);
      Console.WriteLine("Player(uid: {0}) {1} round. Chips: {2}", player.Uid, playerWon ? "won" : "lost", player.Chips);
    });

    _game.DropDealerHand();
    Console.WriteLine("Round finished");
  }

  // Добавляет рандомного игрока в игру.
  private void AddPlayer() {
    var player = PlayerFactory.NewPlayer();
    _game.AddPlayer(player);
    Console.WriteLine("Added player with uid: {0}", player.Uid);
  }

  // Позволяет удалить игрока из игры по его uid.
  private void RemovePlayer() {
    var playerUid = AcceptUserInt("Player uid to remove: ", (rawUid) => _game.Players.FindIndex((player) => player.Uid == rawUid) != -1, "Player with that uid was not found");
    _game.RemovePlayer(playerUid);
    Console.WriteLine("Player with uid: {0} was removed", playerUid);
  }

  // Запускает игру.
  public void Start() {
    Console.WriteLine("Welcome to BlackJack game!\n");

    while (true) {
      Console.WriteLine(GetDelimiter());
      Console.WriteLine(GetActionMenu());

      var action = AcceptUserAction();

      switch (action) {
        case GameAction.StarRound:
          PlayGameRound();
          break;

        case GameAction.ListPlayers:
          Console.Write(GetPlayersList());
          break;

        case GameAction.AddPlayer:
          AddPlayer();
          break;

        case GameAction.RemovePlayer:
          RemovePlayer();
          break;
      }
    }
  }
}