    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG {
    public class UIController : MonoBehaviour {
        [SerializeField] Text playerStatsText, enemyStatsText, playerInventoryText;
        [SerializeField] Player player;

        public delegate void OnPlayerUpdateHandler();
        public static OnPlayerUpdateHandler OnPlayerStatChange;
        public static OnPlayerUpdateHandler OnPlayerInventoryChange;

        public delegate void OnEnemyUpdateHandler(Enemy enemy);
        public static OnEnemyUpdateHandler OnEnemyStatChange;

        // Start is called before the first frame update
        void Start() {
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;

            OnEnemyStatChange += UpdateEnemyStats;
        }

        public void UpdatePlayerStats() {
            playerStatsText.text = string.Format("Player: {0} Energy, {1} Attack, {2} Defence, {3} Gold", player.Energy, player.Attack, player.Defence, player.Gold);
        }

        public void UpdatePlayerInventory() {
            playerInventoryText.text = "Inventory: ";
            foreach (string item in player.Inventory) {
                playerInventoryText.text += item + " | ";
            }
            
        }

        public void UpdateEnemyStats(Enemy enemy) {
            if (enemy && player.Room.Empty == false) enemyStatsText.text = string.Format("{0}: {1} Energy, {2} Attack, {3} Defence", enemy.Description, enemy.Energy, enemy.Attack, enemy.Defence);
            else enemyStatsText.text = "";
        }
    }
}
