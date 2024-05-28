namespace OOP_ICT.Models;

public static class PlayerFactory {
  
  // Уникальный айди последнего созданного игрока.
  static int _lastPlayerUid = 0;
  
  // Генератор рандомных чисел для создания игроков.
  static readonly Random _randomGenerator = new();

  // Создает нового игрока с рандомным кол-вом денег.
  public static Player NewPlayer() {
    _lastPlayerUid += 1;
    return new Player(_lastPlayerUid, String.Format("Player_{0}", _lastPlayerUid), _randomGenerator.Next(10, 100));
  }
}