using OOP_ICT.Models;
using Xunit;
namespace OOP_ICT.BlackjackGame.Tests;

public class Tests {
  
  // Проверяем корректность сборки и запуска тестов.
  [Fact]
  public void CheckTestIsWorking_CorrectBuild() {
    Assert.True(true);
  }

  // Проверяем, что нельзя удалить игрока, который не был добавлен в игру.
  [Fact]
  public void CheckPlayerNotFoundInGameException() {
    var game = new Game(1000);

    Assert.Throws<PlayerNotFoundInGameException>(() => game.RemovePlayer(1));
  }

  // Проверяем, что нельзя удалить игрока, у которого есть ставки.
  [Fact]
  public void CheckCanNotRemovePlayerFromGameException() {
    var game = new Game(1000);
    var player = new Player(1, "", 30);
    game.AddPlayer(player);
    game.MakePlayerBet(1, 10);

    Assert.Throws<CanNotRemovePlayerFromGameException>(() => game.RemovePlayer(1));
  }

  // Проверяем, что игрок не может сделать ставку больше своих денег.
  [Fact]
  public void CheckNotEnoughPlayerChipsException() {
    var game = new Game(1000);
    var player = new Player(1, "", 10);
    game.AddPlayer(player);

    Assert.Throws<NotEnoughPlayerChipsException>(() => game.MakePlayerBet(1, 20));
  }

  // Проверяем, что игрок не может сделать ставку, выигрыш которой больше кол-ва денег в игре.
  [Fact]
  public void CheckNotEnoughGameChipsException() {
    var game = new Game(20);
    var player = new Player(1, "", 30);
    game.AddPlayer(player);

    Assert.Throws<NotEnoughGameChipsException>(() => game.MakePlayerBet(1, 15));
  }

  // Проверяем, что нельзя сравнить руку игрока и диллера, если у диллера меньше 17 очков.
  [Fact]
  public void CheckNotEnoughDealerCardsException() {
    var game = new Game(1000);
    var player = new Player(1, "", 30);
    game.AddPlayer(player);

    Assert.Throws<NotEnoughDealerCardsException>(() => game.EvaluatePlayerHand(1));
  }

  // Проверяем, что нельзя сравнить руку игрока и диллера, если у игрока меньше 2 карт.
  [Fact]
  public void CheckNotEnoughPlayerCardsException() {
    var game = new Game(1000);
    while (game.DealerHand.Value < 16) {
      game.GiveDealerCard();
    }
    var player = new Player(1, "", 30);
    game.AddPlayer(player);

    Assert.Throws<NotEnoughPlayerCardsException>(() => game.EvaluatePlayerHand(1));
  }

  // Проверяем, что игроку начисляется выигрыш при победе.
  [Fact]
  public void CheckPlayerWonTheGame() {
    var game = new Game(1000);
    game.DealerHand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    game.DealerHand.AddCard(new Card(CardRank.Six, CardSuit.Clubs));

    var player = new Player(1, "", 30);
    game.AddPlayer(player);
    player.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    player.Hand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    game.MakePlayerBet(1, 10);
    game.EvaluatePlayerHand(1);
    Assert.Equal(40, player.Chips);
  }

  // Проверяем, что ставка игрока не возвращается при проигрыше.
  [Fact]
  public void CheckPlayerLostTheGame() {
    var game = new Game(1000);
    game.DealerHand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    game.DealerHand.AddCard(new Card(CardRank.Ten, CardSuit.Clubs));

    var player = new Player(1, "", 30);
    game.AddPlayer(player);
    player.Hand.AddCard(new Card(CardRank.Ace, CardSuit.Clubs));
    player.Hand.AddCard(new Card(CardRank.Six, CardSuit.Clubs));

    game.MakePlayerBet(1, 10);
    game.EvaluatePlayerHand(1);
    Assert.Equal(20, player.Chips);
  }
}