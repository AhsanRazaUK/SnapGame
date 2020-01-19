using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace SnapGame
{
    class Program
    {

        private static readonly int startIndex = 1;
        private static readonly int numberOfPlayers = 2;

        private static readonly Dictionary<int, List<Card>> ShuffledPacks =
            new Dictionary<int, List<Card>>();

        private static List<Card> CardsOnTheTable = new List<Card>();
        static void Main(string[] args)
        {
            var Packs =
            new Dictionary<int, List<Card>>();

            for (int i = startIndex; i <= numberOfPlayers; i++)
            {

                var deck = new Deck();
                Packs.Add(i, deck.CreateNewDeck());

                Console.WriteLine($"Player {i} Deck ** CREATED **");
            }

            // DisplayPacks(Packs);
            ShuffleDecksInThePack(Packs);

            Console.WriteLine("\n'd' to display packs and start the game\n's' to start game\n'q' to quit\n");

            switch (Console.ReadLine().ToLower())
            {
                case "d":
                    DisplayPacks(ShuffledPacks);
                    StartGame();
                    PickWinner();
                    break;
                case "s":
                    StartGame();
                    PickWinner();
                    break;
                case "q":
                    Console.WriteLine("** Quit **");
                    return;
            }


            Console.ReadLine();
        }

        private static void PickWinner()
        {
            var random = new Random();

            var randomWinner = random.NextDouble() < 0.5 ? 1 : 2;

            // Add cards on the table to the winner's deck

            ShuffledPacks[randomWinner].AddRange(CardsOnTheTable);

            Console.WriteLine($"******** Player {randomWinner} Won ********");

            DisplayPacks(ShuffledPacks);
        }

        private static void StartGame()
        {
            for (int i = 0; i < 52; i++)
            {
                Console.WriteLine($"\nPress any key to get card from both players");

                Console.ReadLine();

                // If it is snap returns true
                if (CompareCardsInPacks(i) == true)
                    return;
            }
        }
        private static bool CompareCardsInPacks(int index)
        {
            bool isSnap = false;

            //Compare cards of both players for same index 

            for (int i = startIndex; i < ShuffledPacks.Count; i++)
            {
                var cardsTemp = new List<Card>();

                for (int j = 0; j < numberOfPlayers; j++)
                {
                    var packIndex = i + j;
                    var card = ShuffledPacks[packIndex][index];
                    cardsTemp.Add(card);

                    if (cardsTemp.Count > 1 && IsSnap(cardsTemp))
                    {
                        Console.WriteLine($"{JsonSerializer.Serialize(cardsTemp.Select(i => i.FullName))}");

                        Console.WriteLine("************ SNAP ************");

                        isSnap = true;
                        return isSnap;
                    }

                    CardsOnTheTable.Add(card);
                }

                Console.WriteLine($"{JsonSerializer.Serialize(cardsTemp.Select(i => i.FullName))}");

                // Uncomment to view cards on the table
                //   Console.WriteLine($"{JsonSerializer.Serialize(CardsOnTheTable)}");
            }

            return isSnap;
        }

        private static bool IsSnap(List<Card> cardsTemp)
        {

            return (cardsTemp[0].Value == cardsTemp[1].Value)
                || (cardsTemp[0].Suit == cardsTemp[1].Suit);
        }

        private static void DisplayPacks(Dictionary<int, List<Card>> packs)
        {
            foreach (var pack in packs)
            {
                Console.WriteLine($"Player {pack.Key}");
                Console.WriteLine($"{JsonSerializer.Serialize(pack.Value.Select(p => p.FullName))}\n");
            }
        }

        // Shuffling
        private static void ShuffleDecksInThePack(Dictionary<int, List<Card>> packs)
        {
            foreach (var pack in packs)
            {
                ShuffledPacks.Add(pack.Key, ShuffledDeck(pack.Value));

                Console.WriteLine($"Player {pack.Key} Deck ** SHUFFLED **");
            }
        }

        private static List<Card> ShuffledDeck(List<Card> source)
        {
            var random = new Random();

            return source.Distinct().OrderBy(item => random.Next()).ToList();
        }
    }
}
