using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Player : Character
    {
        public int Floor { get; set; }
        public Room Room { get; set; }

        [SerializeField]
        Encounter encounter;
        public World world;
        // Use this for initialization
        void Start()
        {
            Floor = 0;
            Energy = 30;
            Attack = 10;
            Defence = 5;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2,2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty = true;
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
        }

        public void Move(int direction)
        {
            if (this.Room.Enemy)
            {
                return;
            }
            if (direction == 0 && RoomIndex.y > 0)
            {
                Journal.Instance.Log("You move north.");
                RoomIndex -= Vector2.up;
            }
            if (direction == 1 && RoomIndex.x < world.Dungeon.GetLength(0)-1)
            {
                Journal.Instance.Log("You move east.");
                RoomIndex += Vector2.right;
            }
            if (direction == 2 && RoomIndex.y < world.Dungeon.GetLength(1)-1)
            {
                Journal.Instance.Log("You move south.");
                RoomIndex -= Vector2.down;
            }
            if (direction == 3 && RoomIndex.x > 0)
            {
                Journal.Instance.Log("You move west.");
                RoomIndex += Vector2.left;
            }
            if (this.Room.RoomIndex != RoomIndex)
                Investigate();
        }

        public void Investigate()
        {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];

            Debug.Log(RoomIndex);
            encounter.ResetDynamicControls();
            if (this.Room.Empty)
            {
                Journal.Instance.Log("You find yourself in an empty room.");
            }
            else if (this.Room.Chest != null)
            {
                encounter.StartChest();
                Journal.Instance.Log("You've found a chest! What would you like to do?");
            }
            else if (this.Room.Enemy != null)
            {
                Journal.Instance.Log("You are jumped by a " + Room.Enemy.Description + "! What would you like to do?");
                encounter.StartCombat();
            }
            else if (this.Room.Exit)
            {
                encounter.StartExit();
                Journal.Instance.Log("You've found the exit to the next floor. Would you like to continue?");
            }
        }

        public void AddItem(string item)
        {
            Journal.Instance.Log("You were given item: " + item);
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange();
        }

        public void AddItem(int item)
        {
            Inventory.Add(ItemDatabase.Instance.Items[item]);
            UIController.OnPlayerInventoryChange();
        }

        public override void TakeDamage(int amount)
        {
            Debug.Log("Player TakeDamage.");
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange();
        }
        public override void Die()
        {
            Debug.Log("Player died. Game over!");
            base.Die();
        }
    }
}
