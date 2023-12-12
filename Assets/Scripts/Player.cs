using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG {
    public class Player : Character {
        public int Floor { get; set; }
        public Room Room { get; set; }
        [SerializeField] Encounter encounter;
        public WorldGenerator world;

        // Start is called before the first frame update
        void Start() {
            Floor = 0;
            Energy = 30;
            Attack = 10;
            Defence = 5;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2, 2);
            Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y].Empty = true;
            encounter.ResetDynamicControls();
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
        }

        public void Move(int direction) {
            if (this.Room.Enemy) {
                return;
            }

            if(direction == 0 && RoomIndex.y > 0) {
                RoomIndex -= Vector2.up;
                Journal.Instance.Log("You moved North.");    
            }

            if (direction == 1 && RoomIndex.x < (world.Dungeon.GetLength(0) - 1)) {
                RoomIndex += Vector2.right;
                Journal.Instance.Log("You moved East.");
            }

            if (direction == 2 && RoomIndex.y < (world.Dungeon.GetLength(1) - 1)) {
                RoomIndex -= Vector2.down;
                Journal.Instance.Log("You moved South.");
            }

            if (direction == 3 && RoomIndex.x > 0) {
                RoomIndex += Vector2.left;
                Journal.Instance.Log("You moved West.");
            }

            if (this.Room.RoomIndex != RoomIndex) Investigate();
        }      

        public void Investigate() {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            encounter.ResetDynamicControls();
            if (this.Room.Empty) {
                Journal.Instance.Log("You find yourself in an empty room.");
            } else if (this.Room.Chest != null) {
                Journal.Instance.Log("You found a chest. What would you like to do?");
                encounter.ChestFound();
            } else if (this.Room.Enemy != null) {
                Journal.Instance.Log("You are jumped by a " + Room.Enemy.Description + "! What would you like to do?");
                encounter.StartCombat();
            } else if (this.Room.Exit) {
                Journal.Instance.Log("You have found the exit to the next floor! Would you like to continue?");
                encounter.ExitFound();
            }
        }

        public void AddItem(string item) {
            Journal.Instance.Log("You were given item: " + item);
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange();
        }

        public override void TakeDamage(int amount) {
            Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            UIController.OnPlayerStatChange();
            if(Room.Enemy) Journal.Instance.Log("<color=#59ffa1>Player took <b>" + amount + "</b> damage from " + Room.Enemy.Description + "</color>");
            base.TakeDamage(amount);
        }

        public override void Die() {
            Debug.Log("Game over.");
            base.Die();
        }
    }
}