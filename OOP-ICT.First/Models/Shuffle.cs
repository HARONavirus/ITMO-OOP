using OOP_ICT.Interfaces;

public class Shuffle<T> : IShuffle<T> {
  // Генератор рандомных чисел для перемешивания элементов в колоде.
  private readonly Random _randomGenerator = new();
  private static readonly int MIN_SHUFFLE_COUNT = 2;
  private static readonly int MAX_SHUFFLE_COUNT = 6;

  // Реализует перемешивание колоды карт с помощью perfect shuffle и сдвига влево
  public List<T> ShuffleList(List<T> list) {
    return CutShuffle(PerfectShuffle(list));
  }
  
  // Перемешивает список рандомное число раз, используя алгоритм perfect shuffle.
  private List<T> PerfectShuffle(List<T> list) {
    int shuffleCount = _randomGenerator.Next(MIN_SHUFFLE_COUNT, MAX_SHUFFLE_COUNT);
    int halfLength = list.Count / 2;

    for (int i = 0; i < shuffleCount; i++) {
      var shuffledList = new T[list.Count];

      for (int j = 0; j < halfLength; j++) {
        shuffledList[j * 2] = list[j + halfLength];
        shuffledList[j * 2 + 1] = list[j];
      }

      list = new List<T>(shuffledList);
    }
    return list;
  }
  
  // Циклически сдвигает переданный список влево рандомное число раз.
  private List<T> CutShuffle(List<T> list) {
    int cutCount = _randomGenerator.Next(list.Count / 4, list.Count - 1);
    var cuttedList = list.Skip(cutCount).ToList();
    cuttedList.AddRange(list.Take(cutCount));
    return cuttedList;
  }
}