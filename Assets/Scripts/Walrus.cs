using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG {
    public class Walrus : Enemy {
        // Start is called before the first frame update
        void Start() {
            Description = "Walrus";
            Energy = 15;
            MaxEnergy = 15;
            Attack = 3;
            Defence = 5;
            Gold = 30;
            Inventory.Add("Tooth");
        }
    }
}
