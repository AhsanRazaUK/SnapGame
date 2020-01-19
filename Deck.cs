using System.Collections.Generic;

namespace SnapGame
{
    public class Deck
    {
        public List<Card> CreateNewDeck()
        {
            var deck = new List<Card>();

            var values = new List<string>
        {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "Jack",
            "Queen",
            "King",
            "Ace"
        };

            var suits = new List<string>
        {
            "Spades",
            "Diamonds",
            "Hearts",
            "Clubs"
        };

            foreach (string value in values)
            {
                foreach (string suit in suits)
                {
                    deck.Add(
                        new Card
                        {
                            Identifier= $"{value}{suit}",
                            Value = value,
                            Suit = suit,
                            FullName = $"{value} of {suit}"
                        }
                       );
                }
            }

            return deck;
        }
    }
}
