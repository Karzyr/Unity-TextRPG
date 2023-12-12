using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG {
    public class Encounter : MonoBehaviour {

        [SerializeField] Player player;
        [SerializeField] Button[] dynamicControls;

        public delegate void OnEnemyDieHandler();
        public static OnEnemyDieHandler OnEnemyDie;
        public Enemy Enemy { get; set; }

        private void Start() {
            OnEnemyDie += Loot;
        }
            
        public void ResetDynamicControls() {
            foreach(Button button in dynamicControls) {
                button.interactable = false;
            }
        }

        public void StartCombat() { 
            dynamicControls[0].interactable = true;
            dynamicControls[1].interactable = true;

            Enemy = player.Room.Enemy;
            UIController.OnEnemyStatChange(Enemy);
        }

        public void ChestFound() {
            dynamicControls[3].interactable = true;
        }

        public void OpenChest() {
            Chest chest = player.Room.Chest;
            if (chest.Trap) {
                player.TakeDamage(5);
                UIController.OnPlayerStatChange();
                Journal.Instance.Log("It was a trap, you took 5 damage!");
            } else if (chest.Heal) {
                player.TakeDamage(-7);
                UIController.OnPlayerStatChange();
                Journal.Instance.Log("It contained a healing potion! You gained 7 energy!");
            } else if (chest.Enemy) {
                player.Room.Enemy = chest.Enemy;
                player.Room.Chest = null;
                Journal.Instance.Log("There was a monster inside the chest!");
                player.Investigate();
            } else {
                player.Gold += chest.Gold;
                player.AddItem(chest.Item);
                UIController.OnPlayerStatChange();
                UIController.OnPlayerInventoryChange();
                Journal.Instance.Log(string.Format("Searching the chest, you looted a {0} as well as {1} gold from it.", chest.Item, chest.Gold));
            }
            player.Room.Chest = null;
            player.Room.Empty = true;
            dynamicControls[3].interactable = false;
        }

        public void ExitFound() {
            dynamicControls[2].interactable = true;            
        }

        public void Attack() {
            int playerDamageAmount = (int)((player.Attack - Enemy.Defence) * Random.value);
            int enemyDamageAmount = Enemy.Attack - player.Defence + Random.Range(1,5);

            if (playerDamageAmount <= 0) Journal.Instance.Log("You failed your attack.");
            else Enemy.TakeDamage(playerDamageAmount);

            if (enemyDamageAmount <= 0) Journal.Instance.Log(Enemy.Description + " failed his attack.");
            else player.TakeDamage(enemyDamageAmount);
        }

        public void Flee() {
            int enemyDamageAmount = (int)((Enemy.Attack - player.Defence + Random.Range(1, 5)) * Random.Range(2,2.5f));
            if (enemyDamageAmount <= 0) Journal.Instance.Log(Enemy.Description + " failed his attack as you fleed.");
            else player.TakeDamage(enemyDamageAmount);
            player.Room.Enemy = null;
            player.Room.Empty = true;
            UIController.OnEnemyStatChange(null);
            Journal.Instance.Log("You managed to escape the " + Enemy.Description);
            player.Investigate();
        }

        public void Loot() {
            player.AddItem(Enemy.Inventory[0]);
            player.Gold += Enemy.Gold;
            Journal.Instance.Log(string.Format("You've slained a {0}. Searching the carcass, you looted a {1} as well as {2} gold from it.", Enemy.Description, Enemy.Inventory[0], Enemy.Gold));
            player.Room.Enemy = null;
            player.Room.Empty = true;
            UIController.OnEnemyStatChange(null);
            player.Investigate();
        }

        public void ExitFloor() {
            player.world.GenerateFloor();
            player.Floor++;
            Journal.Instance.Log("You found an exit to another floor. Floor: " + player.Floor);
            dynamicControls[2].interactable = false;
        }
    }
}
