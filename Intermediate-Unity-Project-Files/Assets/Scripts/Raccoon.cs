using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        public Enemy Enemy { get; set; }
        public Player Player { get; set; }
        public Walrus Walrus { get; set; }
        // Use this for initialization
        void Start()
        {
            Energy = 10;
            MaxEnergy = 10;
            Attack = 8;
            Defence = 3;
            Gold = 20;
            Description = "Raccoon";
            Inventory.Add("Bandit Mask");
        }
    }
}
