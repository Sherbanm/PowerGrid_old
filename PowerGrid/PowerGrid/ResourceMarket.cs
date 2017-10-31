using PowerGrid.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerGrid
{
    class ResourceMarket
    {
        public const int COAL = 0;
        public const int GAS = 1;
        public const int OIL = 2;
        public const int NUCLEAR = 3;

        public int[,] RefillData { get; set; }

        public int[,] Slots { get; set; }

        public int[,] Market { get; set; }

        public ResourceMarket(Location location, int numberOfPlayers)
        {
            RefillData = GetRefill(location, numberOfPlayers);
            Slots = GetSlots(location);

            int[,] startFill = GetStartFill(location);

            Market = (int[,])Slots.Clone();
            for (int i = 0; i < Market.GetLength(0); i++)
            {
                for (int j = 0; j < Market.GetLength(1); j++)
                {
                    if (i < startFill[0, j] || i > startFill[1, j])
                    {
                        Market[i, j] = 0; 
                    }
                }
            }
        }

        public void Refill(Steps step)
        {
            int[] thisRefill = RefillData.Cast<int>().Skip((int)step * RefillData.GetLength(1)).Take(RefillData.GetLength(1)).ToArray();

            int numberOfSlots = Market.GetLength(0);
            int numberOfResouces = Market.GetLength(1);
            for (int i = 0; i < numberOfResouces; i++)
            {
                for (int j = 0; j < numberOfSlots; j++)
                {
                    if (thisRefill[i] == 0)
                    {
                        // we're done
                        break;
                    }
                    if (Market[j, i] == Slots[j, i])
                    {
                        //cant add here
                        continue;
                    }
                    else
                    {
                        int availableSpaces = Slots[j, i] - Market[j, i];
                        int neededSpaces = thisRefill[i];

                        if (neededSpaces <= availableSpaces)
                        {
                            Market[j, i] += neededSpaces;
                            thisRefill[i] -= neededSpaces;
                        }
                        else
                        {
                            Market[j, i] += availableSpaces;
                            thisRefill[i] -= availableSpaces;
                        }
                    }
                }
            }
        }

        public static int[,] GetSlots(Location location)
        {
            switch (location)
            {
                case Location.EU:
                case Location.NA:
                default:
                    return new int[,] { { 4, 3, 2, 1 }, { 4, 3, 2, 1 }, { 4, 3, 2, 1 }, { 3, 3, 2, 1 }, { 3, 3, 2, 1 }, { 3, 3, 2, 1 }, { 2, 3, 2, 2 }, { 2, 3, 2, 2 }, { 2, 0, 4, 2 } };
            }
        }

        public static int[,] GetStartFill(Location location)
        {
            switch (location)
            {
                case Location.EU:
                    return new int[,] { { 1, 2, 2, 7 }, { 8, 7, 8, 8 } };
                case Location.NA:
                    return new int[,] { { 1, 2, 3, 8 }, { 8, 7, 8, 8 } };
                default:
                    return new[,] { { 0 } };
            }
        }

        public void BuyResources(Player player, int desiredType, int desiredQuantity)
        {
            int cost = 0;
            for (int slot = 0; slot < Market.GetLength(0); slot++)
            {
                if (Market[slot, desiredType] > 0)
                {
                    while (desiredQuantity > 0 && Market[slot, desiredType] != 0 )
                    {
                        Market[slot, desiredType]--;
                        desiredQuantity--;
                        cost += (slot + 1);
                    }
                }
            }
            if (desiredQuantity != 0)
            {
                // not enough resources
                
            }
            if (cost > player.Money)
            {
                // not enough money
            }
            player.Money -= cost;
            player.Resources[desiredType] += desiredQuantity;
        }

        public static int[,] GetRefill(Location location, int numberOfPlayers)
        {
            switch (location)
            {
                case Location.EU:
                    switch (numberOfPlayers)
                    {
                        case 2:
                        case 3:
                            return new[,] { { 2, 2, 2, 1 }, { 6, 3, 2, 1 }, { 2, 5, 3, 2 } };
                        case 4:
                            return new[,] { { 3, 3, 3, 1 }, { 7, 4, 3, 2 }, { 4, 5, 4, 2 } };
                        case 5:
                            return new[,] { { 3, 3, 4, 2 }, { 8, 5, 3, 3 }, { 4, 7, 5, 3 } };
                        case 6:
                            return new[,] { { 5, 4, 4, 2 }, { 10, 6, 5, 3 }, { 5, 8, 6, 4 } };
                        default:
                            return new[,] { { 0 } };
                    }
                case Location.NA:
                    switch (numberOfPlayers)
                    {
                        case 2:
                        case 3:
                            return new[,] { { 2, 2, 3, 2 }, { 5, 2, 1, 1 }, { 2, 4, 3, 2 } };
                        case 4:
                            return new[,] { { 3, 3, 3, 2 }, { 6, 3, 2, 1 }, { 3, 5, 4, 2 } };
                        case 5:
                            return new[,] { { 4, 3, 4, 3 }, { 7, 3, 2, 2 }, { 4, 6, 5, 3 } };
                        case 6:
                            return new[,] { { 5, 4, 5, 3 }, { 8, 4, 3, 2 }, { 5, 7, 6, 4 } };
                        default:
                            return new[,] { { 0 } };
                    }
                default:
                    return new[,] { { 0 } };
            }
        }
    }
}
