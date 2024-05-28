using OOP_ICT.Models;
internal class Program {
  
  // Пример использования дилера
  static void Main() {
    var shuffle = new Shuffle<Card>();
    var dealer = new Dealer(shuffle);

    var temp = dealer.GetCards(10);
  }
}