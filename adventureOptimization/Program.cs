using adventureOptimization;
using OfficeOpenXml;
using System;
using System.Drawing.Text;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace adventureOptimization
{
    internal partial class Program
    {
        public static List<CarryingCapacity> carryingCapacities = new List<CarryingCapacity>();
        static void Main(string[] args)
        {
            List<Player> playerList = new List<Player>();

            capacityData();

            randomItems(playerList);
            

        }
        public static void playerData(List<Player> playerList)
        {
            string PlayerFile = @"C:\Users\willi\OneDrive\Skrivebord\Players.csv";

            using (var reader = new StreamReader(PlayerFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Player playerValue = new Player()
                    {
                        Name = values[0],
                        Strength = Convert.ToInt32(values[1]),
                        CarryingCapacity = new CarryingCapacity()
                        {
                            StreghtScore = Convert.ToInt32(values[1]),
                            Capacity = carryingCapacities.Find(i => i.StreghtScore == Convert.ToInt32(values[1]))!.Capacity
                        }
                    };

                    playerList.Add(playerValue);
                }
            }

        }
        public static void capacityData()
        {

            string CapacityFile = @"C:\Users\willi\OneDrive\Skrivebord\Carrying_capasity.csv";

            using (var reader = new StreamReader(CapacityFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    CarryingCapacity capacityValue = new CarryingCapacity()
                    {
                        StreghtScore = Convert.ToInt32(values[0]),
                        Capacity = Convert.ToInt32(values[1]),
                    };

                    carryingCapacities.Add(capacityValue);
                }
            }
        }
        public static void goodsData(List<Goods> goods, List<Goods> goodsInterative)
        {
            string GoodsFile = @"C:\Users\willi\OneDrive\Skrivebord\Goods.csv";

            using (var reader = new StreamReader(GoodsFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Goods goodsValue = new Goods()
                    {
                        Name = values[0],
                        Cost = Convert.ToDouble(values[1]),
                        Weight = Convert.ToDouble(values[2]),
                    };

                    goods.Add(goodsValue);
                    goodsInterative.Add(goodsValue);
                }
            }
        }
        public static void randomItems(List<Player> playerList)
        {
            List<Goods> goods = new List<Goods>();
            List<Goods> goodsInterative = new List<Goods>();
            List<Player> inventories = new List<Player>();

            for (int i = 0; i < 100; i++)
            {
                goods.Clear();
                playerList.Clear();

                goodsData(goods, goodsInterative);
                playerData(playerList);

               
                while (playerList[3].CarryingCapacity.Capacity >= 0 && goodsInterative.Count != 0)
                {
                    int index = RND.Range(0, goods.Count);

                    var randomLoot = goods[index];
                    if (playerList[3].CarryingCapacity.Capacity > randomLoot.Weight)
                    {
                        goods.Remove(randomLoot);
                        goodsInterative.Remove(randomLoot);
                        playerList[3].Inventory.Add(randomLoot);
                        playerList[3].CarryingCapacity.Capacity -= randomLoot.Weight;
                    }
                    else
                    {
                        playerList[3].LeftLoot.Add(randomLoot);
                        goodsInterative.Remove(randomLoot);
                    }
                }
                
                inventories.Add(playerList[3]);

            }
            FindBestPlayers(inventories);

        }
        public static void FindBestPlayers(List<Player> inventories)
        {

            foreach (Player player in inventories)
            {
                for (int i = 0; i < player.Inventory.Count - 1; i++)
                {
                    player.Loss += player.Inventory[i].Cost;
                }
            }

            List<Player> sorted = inventories.OrderByDescending( x => x.Loss).ToList();
            List<Player> bestInventories = new List<Player>();

            for (int i = 0;i < 2; i++)
            {
                bestInventories.Add(sorted[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                Player player = new Player(bestInventories[0].Name, bestInventories[0].Strength, bestInventories[0].Loss, bestInventories[0].CarryingCapacity, bestInventories[0].Inventory, bestInventories[0].LeftLoot);
                Console.WriteLine(player.Loss);
            }

            //geneticItem(bestInventories);

        }

        //public static void geneticItem(List<Player> bestInventories) 
        //{

        //    List<Goods> goods = new List<Goods>();
        //    List<Goods> goodsInterative = new List<Goods>();
        //    List<Player> best = new List<Player>();

        //    var bestPlayer = new Player()
        //    {
        //        Name = bestInventories[0].Name,
        //        Loss = bestInventories[0].Loss,
        //        Strength = bestInventories[0].Strength,
        //        Inventory = bestInventories[0].Inventory,
        //        CarryingCapacity = new CarryingCapacity()
        //        {
        //            Capacity = bestInventories[0].CarryingCapacity.Capacity,
        //            StreghtScore = bestInventories[0].CarryingCapacity.StreghtScore
        //        }
        //    };
        //    best.Add(bestPlayer);
        //    for (int i = 0; i < 100; i++)
        //    {
                
        //        foreach (var player in bestInventories)
        //        {
        //            goods.Clear();
        //            goodsData(goods, goodsInterative);
 
        //            int numOfdeletes = RND.Range(0, player.Inventory.Count);
        //            for (int j = 0; j < numOfdeletes; j++)
        //            {
        //                int removeitem = RND.Range(0, player.Inventory.Count);
        //                var randomLoot = player.Inventory[removeitem];
        //                player.Inventory.Remove(randomLoot);
        //                player.CarryingCapacity.Capacity += randomLoot.Weight;
        //                player.Loss -= randomLoot.Cost;
        //            }  
        //            while (player.CarryingCapacity.Capacity >= 0 && goodsInterative.Count != 0)
        //            {
        //                int index = RND.Range(0, goods.Count);
        //                var randomLoot = goods[index];
        //                if (player.CarryingCapacity.Capacity > randomLoot.Weight && !player.Inventory.Contains(randomLoot))
        //                {
        //                    goods.Remove(randomLoot);
        //                    goodsInterative.Remove(randomLoot);
        //                    player.Inventory.Add(randomLoot);
        //                    player.CarryingCapacity.Capacity -= randomLoot.Weight;
        //                    player.Loss += randomLoot.Cost;

        //                }
        //                else
        //                {
        //                    goodsInterative.Remove(randomLoot);
        //                }
        //            }

        //            if (bestPlayer.Loss < player.Loss)
        //            {
        //                best.Add(player);
        //                bestPlayer.Loss = player.Loss;
        //                bestPlayer.Inventory = player.Inventory;
        //            }
        //            else
        //            {
        //                best.Add(bestPlayer);
        //            }
        //        }

        //    }
        //    foreach (var player in best)
        //    {
        //        Console.WriteLine(player.Loss);
        //    }
        //}

    }
}