using OOP_ICT.Models;
namespace OOP_ICT.Interfaces;

public interface IChecker {
  CardsCombination? Check(List<Card> cards, Dictionary<CardRank, int> cardsCount);
}