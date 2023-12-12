using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Chest
    {
        public string Item { get; set; }
        public int Gold { get; set; }
        public bool Trap { get; set; }
        public bool Heal { get; set; }
        public Enemy Enemy { get; set; }

        public Chest()
        {
            Enemy = Random.Range(0, 5) == 2 ? EnemyDatabase.Instance.GetRandomEnemy() : null;
            Trap = Random.Range(0, 7) == 2;

            if (Random.Range(0,7) == 2)
            {
                Trap = true;
            }
            else if (Random.Range(0,5) == 2)
            {
                Heal = true;
            }
            else if (Random.Range(0,5) == 2)
            {
                Enemy = EnemyDatabase.Instance.GetRandomEnemy();
            }
            else
            {
                int itemToAdd = Random.Range(0, ItemDatabase.Instance.Items.Count);
                Item = ItemDatabase.Instance.Items[itemToAdd];
                Gold = Random.Range(20, 200);
            }
        }
    }
}
