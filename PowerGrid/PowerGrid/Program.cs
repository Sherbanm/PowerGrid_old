using PowerGrid.Auctioning;
using PowerGrid.GameState;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerGrid
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.Write("Enter number of players: ");
            var numberOfPlayers = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Location (0)NA or (1)EU: ");
            var location = Convert.ToInt32(Console.ReadLine());
            */

            var numberOfPlayers = 6;
            var location = Location.NA;

            GameObject game = new GameObject(numberOfPlayers, (Location)location);

            while (true)
            {
                // 1. Determine player order
                game.UpdatePlayerOrder();

                // 2. Auction Power Plants
                AuctionPowerPlants(game);

                // 3. Buy Resources
                BuyResources(game);

                // 4. Build Generators
                BuildGenerators(game);

                // 5. Bureaucracy
                game.PerformBureaucracy();
            }
        }

        public static void AuctionPowerPlants(GameObject game)
        {
            int startingBid = 5;
            var rng = new Random();

            foreach (var player in game.Players)
            {
                if (player.CanBuy)
                {
                    Console.WriteLine("You can bid for a power station. Your options are 1-4:");
                    foreach (var station in game.AuctionHouse.CurrentMarket)
                    {
                        ColoredConsoleWrite(GetConsoleColor(station.Type), GetShortName(station.Type));
                        Console.WriteLine($" Val: {station.Value}, Res: {station.Resource} Pow: {station.Power}");
                    }

                    var newAuction = new Auction(new Bid(player, startingBid), game.AuctionHouse.CurrentMarket.ElementAt(rng.Next(4)));

                    var eligiblePlayers = new LinkedList<Player>();
                    foreach (var item in game.Players)
                    {
                        if (item.CanBuy)
                        {
                            eligiblePlayers.AddLast(item);
                        }
                    }

                    // skip first player cause hes bidding
                    var currentPlayer = eligiblePlayers.First.Next;
                    while (eligiblePlayers.Count != 1)
                    {
                        var nextPlayer = currentPlayer.Next;
                        eligiblePlayers.Remove(currentPlayer);
                        currentPlayer = nextPlayer;
                    }

                    // maybe make this more efficient.
                    newAuction.ResolveAuction();
                    game.AuctionHouse.CurrentMarket.Remove(newAuction.PowerStation);
                    game.AuctionHouse.DrawPowerPlant();
                }
            }
            game.Players.ForEach(p => p.CanBuy = true);
        }

        public static void BuyResources(GameObject game)
        {
            var rng = new Random();
            game.Players.Reverse();

            foreach (var player in game.Players)
            {
                if (player.CanBuy)
                {
                    var Market = game.ResourceMarket.Market;
                    Console.WriteLine("You can buy resource:");
                    for (int i = 0; i < Market.GetLength(1); i++)
                    {
                        for (int j = 0; j < Market.GetLength(0); j++)
                        {
                            for(int k = 0; k < 4; k++)
                            {
                                if (Market[j, i] <= k)
                                {
                                    ColoredConsoleWrite(GetConsoleColor((ResourceType)i), " ");
                                }
                                else
                                {
                                    ColoredConsoleWrite(GetConsoleColor((ResourceType)i), "X");
                                }
                            }
                            ColoredConsoleWrite(GetConsoleColor((ResourceType)i), "|");
                        }
                        Console.WriteLine();
                    }
                    int desiredType = rng.Next(4);
                    int desiredQunatity = rng.Next(3)+ 1;
                    game.ResourceMarket.BuyResources(player, desiredType, desiredQunatity);
                }
            }
            game.Players.ForEach(p => p.CanBuy = true);
        }

        public static void BuildGenerators(GameObject game)
        {
            var rng = new Random();

            foreach (var player in game.Players)
            {
                if (player.CanBuy)
                {
                    Console.WriteLine("Skip for now.");
                }
            }
        }


        public void InteractiveAuction(GameObject game)
        {
            foreach (var player in game.Players)
            {
                if (player.CanBuy)
                {
                    Console.WriteLine("You can bid for a power station. Your options are 1-4:");
                    foreach (var station in game.AuctionHouse.CurrentMarket)
                    {
                        ColoredConsoleWrite(GetConsoleColor(station.Type), GetShortName(station.Type));
                        Console.WriteLine($" Val: {station.Value}, Res: {station.Resource} Pow: {station.Power}");
                    }

                    string option = Console.ReadLine();
                    try
                    {
                        var stationPick = Convert.ToInt32(option);
                        Console.WriteLine("Whats the starting bid");
                        var startingBid = Convert.ToInt32(Console.ReadLine());


                        var newAuction = new Auction(new Bid(player, startingBid), game.AuctionHouse.CurrentMarket.ElementAt(stationPick));

                        var eligiblePlayers = new LinkedList<Player>();
                        foreach (var item in game.Players)
                        {
                            if (item.CanBuy)
                            {
                                eligiblePlayers.AddLast(item);
                            }
                        }

                        var currentPlayer = eligiblePlayers.First;
                        while (eligiblePlayers.Count != 1)
                        {
                            Console.WriteLine($"Its {currentPlayer.Value.Name} turn. you can bid or pass");

                            option = Console.ReadLine();
                            if (option != "x")
                            {
                                int bidValue = Convert.ToInt32(option);
                                if (bidValue > newAuction.CurrentBid.Value)
                                {
                                    newAuction.Bid(new Bid(currentPlayer.Value, bidValue));
                                }
                                else
                                {
                                    Console.Write("No a valid bid. You forfeit and pass.");
                                    eligiblePlayers.Remove(currentPlayer);
                                }

                            }
                            else
                            {
                                Console.WriteLine("You pass.");
                                eligiblePlayers.Remove(currentPlayer);
                            }

                            currentPlayer = currentPlayer.Next ?? eligiblePlayers.First;
                        }

                    }
                    // skipped
                    catch (Exception e)
                    {
                        player.CanBuy = false;
                    }
                }
            }
        }

        public static void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }

        public static ConsoleColor GetConsoleColor(ResourceType type)
        {
            if (type == ResourceType.Coal)
            {
                return ConsoleColor.DarkYellow ;
            }
            if (type == ResourceType.Gas)
            {
                return ConsoleColor.Cyan;
            }
            if (type == ResourceType.Green)
            {
                return ConsoleColor.Green;
            }
            if (type == ResourceType.Mixed)
            {
                return ConsoleColor.Gray;
            }
            if (type == ResourceType.Nuclear)
            {
                return ConsoleColor.Red;
            }
            if (type == ResourceType.Oil)
            {
                return ConsoleColor.Gray;
            }
            return ConsoleColor.White;
        }

        public static string GetShortName(ResourceType type)
        {
            if (type == ResourceType.Coal)
            {
                return "COA";
            }
            if (type == ResourceType.Gas)
            {
                return "GAS";
            }
            if (type == ResourceType.Green)
            {
                return "GRN";
            }
            if (type == ResourceType.Mixed)
            {
                return "MIX";
            }
            if (type == ResourceType.Nuclear)
            {
                return "NUC";
            }
            if (type == ResourceType.Oil)
            {
                return "OIL";
            }
            return null;
        }
    }
}
