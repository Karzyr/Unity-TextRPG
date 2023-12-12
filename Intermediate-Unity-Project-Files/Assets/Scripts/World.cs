using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class World : MonoBehaviour
    {
        public Room[,] Dungeon { get; set; }
        public Vector2 Grid;
        private void Awake()
        {
            Dungeon = new Room[(int)Grid.x, (int)Grid.y];
            StartCoroutine(GenerateFloor());
        }

        public IEnumerator GenerateFloor()
        {
            for(int x = 0; x < Grid.x; x++)
            {
                for(int y = 0; y < Grid.y; y++)
                {
                    Dungeon[x, y] = new Room
                    {
                        RoomIndex = new Vector2(x, y)
                    };
                }
            }
            Debug.Log("Finding possible exit location. " + Time.time);
            yield return new WaitForSeconds(2);

            Vector2 exitLocation = new Vector2((int)Random.Range(0, Grid.x), (int)Random.Range(0, Grid.y));
            Dungeon[(int)exitLocation.x, (int)exitLocation.y].Exit = true;
            Dungeon[(int)exitLocation.x, (int)exitLocation.y].Empty = false;
            Debug.Log("Exit is at: " + exitLocation + " " + Time.time);
        }
    }
}
