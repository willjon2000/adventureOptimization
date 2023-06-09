﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static adventureOptimization.Program;

namespace adventureOptimization
{
    internal class Player
    {
        public Player()
        {

        }

        public Player(string name, int strength, double loss, CarryingCapacity carryingCapacity, List<Goods> inventory, List<Goods> leftLoot, int percentageChanceOfMutation)
        {
            this.Name = name;
            this.Strength = strength;
            this.Loss = loss;
            this.CarryingCapacity = carryingCapacity;
            this.Inventory = new List<Goods>();
            foreach (var item in inventory)
            {
                this.Inventory.Add(item);

                if (RND.Range(1, 100) <= percentageChanceOfMutation)
                {
                    mutation(item);
                }
            }
            this.LeftLoot = new List<Goods>();
            foreach (var item in leftLoot)
            {
                this.LeftLoot.Add(item);
            }



        }

        public string Name { get; set; }
        public int Strength { get; set; }
        public double Loss { get; set; }



        public CarryingCapacity CarryingCapacity { get; set; }
        public List<Goods> Inventory { get; set; } = new List<Goods>();
        public List<Goods> LeftLoot { get; set; } = new List<Goods>();

        public void mutation(Goods item)
        {

            int count = 0;
            Inventory.Remove(item);
            LeftLoot.Add(item);
            CarryingCapacity.Capacity += item.Weight;
            Loss -= item.Cost;

            while (CarryingCapacity.Capacity >= 0 && count == 124)
            {
                int index = RND.Range(0, LeftLoot.Count);
                var randomLoot = LeftLoot[index];
                if (CarryingCapacity.Capacity > randomLoot.Weight && !Inventory.Contains(randomLoot))
                {
                    LeftLoot.Remove(randomLoot);
                    Inventory.Add(randomLoot);
                    CarryingCapacity.Capacity -= randomLoot.Weight;
                    Loss += randomLoot.Cost;

                }
                count++;
            }
        }
    }
}
