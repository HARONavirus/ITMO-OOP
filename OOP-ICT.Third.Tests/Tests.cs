using OOP_ICT.Models;
using Xunit;
namespace OOP_ICT.Third.Tests;

public class Tests {
  private (BlackjackGame game, BlackjackBank bank, BlackjackGameTable table) GenerateGame(int initialChips = 1000) {
    var bank = new BlackjackBank(initialChips);
    var table = new BlackjackGameTable();
    return (new BlackjackGame(table, bank), bank, table);
  }

  // Проверяем корректность сборки и запуска тестов.
  [Fact]
  public void CheckTestIsWorking_CorrectBuild() {
    Assert.True(true);
  }

  // Проверяем, что нельзя удалить игрока, который не был добавлен в игру.
  [Fact]
  public void CheckPlayerNotFoundInGameException() {
    var game = GenerateGame().game;
    Assert.Throws<PlayerNotFoundInGameException>(() => game.Leave(1));
  }

  // Проверяем, что нельзя удалить игрока, у которого есть ставки.
  [Fact]
  public void CheckCanNotRemovePlayerFromGameException() {
    var game = GenerateGame().game;
    var player = new Player(1, "", 30);
    game.Join(player);
    game.MakeBet(1, 10);

    Assert.Throws<PlayerHasBetLeftException>(() => game.Leave(1));
  }

  // Проверяем, что игрок не может сделать ставку больше своих денег.
  [Fact]
  public void CheckNotEnoughPlayerChipsException() {
    var game = GenerateGame().game;
    var player = new Player(1, "", 10);
    game.Join(player);

    Assert.Throws<NotEnoughPlayerChipsException>(() => game.MakeBet(1, 20));
  }

  // Проверяем, что игрок не может сделать ставку, выигрыш которой больше кол-ва денег в игре.
  [Fact]
  public void CheckNotBankGameChipsException() {
    var game = GenerateGame(20).game;
    var player = new Player(1, "", 30);
    game.Join(player);

    Assert.Throws<NotEnoughBankChipsException>(() => game.MakeBet(1, 15));
  }

  // Проверяем, что нельзя сравнить руку игрока и диллера, если у диллера меньше 16 поинтов.
  [Fact]
  public void CheckNotEnoughDealerCardsException() {
    var game = GenerateGame().game;
    var player = new Player(1, "", 30);
    game.Join(player);

    Assert.Throws<NotEnoughDealerCardsException>(() => game.EvaluatePlayerHand(1));
  }

  // Проверяем, что нельзя сравнить руку игрока и диллера, если у игрока меньше 2 карт.
  [Fact]
  public void CheckNotEnoughPlayerCardsException() {
    var (game, _, table) = GenerateGame();
    while (table.Hand.Value < 16) {
      game.GiveDealerCard();
    }
    var player = new Player(1, "", 30);
    game.Join(player);

    Assert.Throws<NotEnoughPlayerCardsException>(() => game.EvaluatePlayerHand(1));
  }

  // Проверяем поведение при выигрыше игрока.
  [Fact]
  public void CheckPlayerWonTheGame() {
    var (game, _, table) = GenerateGame();
    table.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    table.Hand.AddCard(new Card(CardRank.Six, CardSuit.Clubs));

    Console.WriteLine(table.Hand.Value);

    var player = new Player(1, "", 30);
    game.Join(player);
    player.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    player.Hand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    game.MakeBet(1, 10);
    Assert.Equal(BlackjackHandEvaluation.Won, game.EvaluatePlayerHand(1));
    Assert.Equal(40, player.Chips);
  }

  // Проверяем поведение при проигрыше игрока.
  [Fact]
  public void CheckPlayerLostTheGame() {
    var (game, _, table) = GenerateGame();
    table.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    table.Hand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    var player = new Player(1, "", 30);
    game.Join(player);
    player.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    player.Hand.AddCard(new Card(CardRank.Six, CardSuit.Clubs));

    game.MakeBet(1, 10);
    Assert.Equal(BlackjackHandEvaluation.Loss, game.EvaluatePlayerHand(1));
    Assert.Equal(20, player.Chips);
  }

  // Проверяем поведение при ничьей.
  [Fact]
  public void CheckPlayerDrawTheGame() {
    var (game, _, table) = GenerateGame();
    table.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    table.Hand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    var player = new Player(1, "", 30);
    game.Join(player);
    player.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    player.Hand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    game.MakeBet(1, 10);
    Assert.Equal(BlackjackHandEvaluation.Draw, game.EvaluatePlayerHand(1));
    Assert.Equal(30, player.Chips);
  }
}