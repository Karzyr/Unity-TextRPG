using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG {
    public class Enemy : Character {

        [SerializeField] Player player;

        public string Description { get; set; }
        public override void TakeDamage(int amount) {
            base.TakeDamage(amount);
            Journal.Instance.Log("<color=#2900a3>" + Description + " took <b>" + amount + "</b> damage from you.</color>");
            UIController.OnEnemyStatChange(this);
        }

        public override void Die() {
            Debug.Log("Enemy died.");           
            Encounter.OnEnemyDie();
            Energy = MaxEnergy;
        }
    }
}
