using OOP_ICT.Interfaces;
using OOP_ICT.Models;
using Xunit;
namespace OOP_ICT.Fourth.Tests;

public class Tests {
  private CombinationChecker GetDefaultChecker() {
    return new CombinationChecker(new List<IChecker>() {
      new RoyalFlushChecker(),
      new StraightFlushChecker(),
      new FourOfAKindChecker(),
      new FullHouseChecker(),
      new FlushChecker(),
      new StraightChecker(),
      new ThreeOfAKindChecker(),
      new TwoPairChecker(),
      new PairChecker(),
      new HighCardChecker()
    });
  }

  // Проверяем корректность сборки и запуска тестов.
  [Fact]
  public void CheckTestIsWorking_CorrectBuild() {
    Assert.True(true);
  }

  // Проверяет, что у игрока выходит комбинация "Пара" среди всех его карт
  [Fact]
  public void CheckPairCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Two, CardSuit.Spades),
      new(CardRank.Eight, CardSuit.Diamonds),
      new(CardRank.Eight, CardSuit.Clubs),
      new(CardRank.Nine, CardSuit.Hearts),
      new(CardRank.King, CardSuit.Clubs)
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.Pair, combination.Kind);
        Assert.Equal(CardRank.Eight, combination.HighRank);
      }
    );

    Assert.True(true);
  }

  // Проверяет, что у игрока выходит комбинация "ДвеПары" среди всех его карт
  [Fact]
  public void CheckTwoPairCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Two, CardSuit.Spades),
      new(CardRank.Eight, CardSuit.Diamonds),
      new(CardRank.Two, CardSuit.Clubs),
      new(CardRank.Eight, CardSuit.Hearts),
      new(CardRank.King, CardSuit.Clubs)
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.TwoPair, combination.Kind);
        Assert.Equal(CardRank.Eight, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "Сет" среди всех его карт
  [Fact]
  public void CheckThreeOfAKindCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Two, CardSuit.Spades),
      new(CardRank.Eight, CardSuit.Diamonds),
      new(CardRank.Eight, CardSuit.Clubs),
      new(CardRank.Eight, CardSuit.Hearts),
      new(CardRank.King, CardSuit.Clubs)
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.ThreeOfAKind, combination.Kind);
        Assert.Equal(CardRank.Eight, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "Стрит" среди всех его карт
  [Fact]
  public void CheckStraightCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Nine, CardSuit.Spades),
      new(CardRank.Ten, CardSuit.Diamonds),
      new(CardRank.Jack, CardSuit.Clubs),
      new(CardRank.Queen, CardSuit.Hearts),
      new(CardRank.King, CardSuit.Clubs)
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.Straight, combination.Kind);
        Assert.Equal(CardRank.King, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "Флеш" среди всех его карт
  [Fact]
  public void CheckFlushCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Two, CardSuit.Diamonds),
      new(CardRank.Nine, CardSuit.Diamonds),
      new(CardRank.Ten, CardSuit.Diamonds),
      new(CardRank.Six, CardSuit.Diamonds),
      new(CardRank.King, CardSuit.Diamonds)
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.Flush, combination.Kind);
        Assert.Equal(CardRank.King, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "ФуллХаус" среди всех его карт
  [Fact]
  public void CheckFullHouseCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Four, CardSuit.Spades),
      new(CardRank.Nine, CardSuit.Diamonds),
      new(CardRank.Four, CardSuit.Clubs),
      new(CardRank.Nine, CardSuit.Clubs),
      new(CardRank.Nine, CardSuit.Hearts),
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.FullHouse, combination.Kind);
        Assert.Equal(CardRank.Nine, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "Каре" среди всех его карт
  [Fact]
  public void CheckFourOfAKindCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Ten, CardSuit.Clubs),
      new(CardRank.Three, CardSuit.Spades),
      new(CardRank.Ten, CardSuit.Diamonds),
      new(CardRank.Ten, CardSuit.Hearts),
      new(CardRank.Ten, CardSuit.Spades),
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.FourOfAKind, combination.Kind);
        Assert.Equal(CardRank.Ten, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "СтритФлеш" среди всех его карт
  [Fact]
  public void CheckStraightFlushCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Nine, CardSuit.Diamonds),
      new(CardRank.King, CardSuit.Diamonds),
      new(CardRank.Ten, CardSuit.Diamonds),
      new(CardRank.Queen, CardSuit.Diamonds),
      new(CardRank.Jack, CardSuit.Diamonds),
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.StraightFlush, combination.Kind);
        Assert.Equal(CardRank.King, combination.HighRank);
      }
    );
  }

  // Проверяет, что у игрока выходит комбинация "РояльФлеш" среди всех его карт
  [Fact]
  public void CheckRoyalFlushCombination() {
    var checker = GetDefaultChecker();

    List<Card> cards = new() {
      new(CardRank.Ten, CardSuit.Diamonds),
      new(CardRank.Ace, CardSuit.Diamonds),
      new(CardRank.King, CardSuit.Diamonds),
      new(CardRank.Queen, CardSuit.Diamonds),
      new(CardRank.Jack, CardSuit.Diamonds),
    };

    var combination = checker.GetCombination(cards);

    Assert.Multiple(
      () => {
        Assert.Equal(CardsCombinationKind.RoyalFlush, combination.Kind);
        Assert.Equal(CardRank.Ace, combination.HighRank);
      }
    );
  }
}