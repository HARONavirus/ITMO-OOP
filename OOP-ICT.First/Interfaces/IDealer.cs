using OOP_ICT.Models;
namespace OOP_ICT.Interfaces;

public interface IDealer {
  List<Card> GetCards(int takeCount);
}