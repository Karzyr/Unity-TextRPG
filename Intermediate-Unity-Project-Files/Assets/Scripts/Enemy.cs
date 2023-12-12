using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public interface IBaddie
    {
        void Cry();
        string Description { get; set; }
    }
    public class Enemy : Character, IBaddie
    {
        public string Description { get; set; }
        public bool dead { get; set; }

        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);

            if (dead) return;

            UIController.OnEnemyUpdate(this);
            Debug.Log("This also happens, but only on enemy! not other characters.");
        }

        public void Cry()
        {

        }

        public override void Die()
        {
            dead = true;
            Energy = MaxEnergy;
            Debug.Log("Character died, was enemy.");
            Encounter.OnEnemyDie();
        }
    }
}
