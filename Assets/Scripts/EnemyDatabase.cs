using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG {
    public class EnemyDatabase : MonoBehaviour {
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static EnemyDatabase Instance { get; private set; }

        private void Awake() {

            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            }
            else {
                Instance = this;
            }

            foreach (Enemy enemy in GetComponents<Enemy>()) {
                Debug.Log(enemy);
                Enemies.Add(enemy);
            }
        }

        public Enemy GetRandomEnemy() {
            return Enemies[Random.Range(0, Enemies.Count)];
        }
    }
}
