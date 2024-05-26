using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public class Room {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public Vector2 size;
    public int startPos = 0;
    public GameObject room;
    public GameObject endRoom;
    public Vector2 offset;
    List<Room> floor;
    public GameObject player;

    readonly List<BoxCollider> floors = new();
    readonly List<RoomBehavior> behaviors = new();
    public GenerateNavLinks generateNavLinks;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MazeGenerator();
    }

    void MazeGenerator() {
        floor = new List<Room>();
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                Room room = new();
                floor.Add(room);
            }
        }

        int currentRoom = startPos;
        Stack<int> path = new();
        int k = 0;

        while(k < 1000) {
            k++;
            floor[currentRoom].visited = true;

            if(currentRoom == floor.Count - 1) {
                break;
            }

            List<int> neighbors = Neighbors(currentRoom);
            if(neighbors.Count == 0) {
                if(path.Count == 0) break;
                else {
                    currentRoom = path.Pop();
                }
            } else {
                path.Push(currentRoom);
                int newRoom = neighbors[Random.Range(0, neighbors.Count)];
                if(newRoom > currentRoom) {
                    //south or east
                    if(newRoom - 1 == currentRoom) {
                        floor[currentRoom].status[3] = true;
                        currentRoom = newRoom;
                        floor[currentRoom].status[2] = true;
                    } else {
                        floor[currentRoom].status[1] = true;
                        currentRoom = newRoom;
                        floor[currentRoom].status[0] = true;
                    }
                } else {
                    //north or west
                    if (newRoom + 1 == currentRoom) {
                        floor[currentRoom].status[2] = true;
                        currentRoom = newRoom;
                        floor[currentRoom].status[3] = true;
                    }
                    else {
                        floor[currentRoom].status[0] = true;
                        currentRoom = newRoom;
                        floor[currentRoom].status[1] = true;
                    }
                }
            }
        }

        GenerateDungeon();
    }

    List<int> Neighbors(int cell) {
        List<int> neighbors = new();

        //north
        if(cell - size.x >= 0 && !floor[Mathf.FloorToInt(cell - size.x)].visited) {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //south
        if (cell + size.x < floor.Count && !floor[Mathf.FloorToInt(cell + size.x)].visited) {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //west
        if (cell % size.x != 0 && !floor[Mathf.FloorToInt(cell - 1)].visited) {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        //east
        if ((cell + 1) % size.x >= 0 && !floor[Mathf.FloorToInt(cell + 1)].visited) {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        return neighbors;
    }

    void GenerateDungeon() {
        for(int i = 0; i < size.x; i ++) {
            for(int j = 0; j < size.y; j ++) {
                Room currentRoom = floor[Mathf.FloorToInt(i + j * size.x)];
                if (currentRoom.visited) {
                    RoomBehavior newRoom;
                    if (i == size.x - 1 && j == size.y - 1) {
                        newRoom = Instantiate(endRoom, new Vector3(i * offset.x, 2.5f, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                        newRoom.UpdateRoom(currentRoom.status);
                        newRoom.GetComponent<RoomBehavior>().player = player;
                        newRoom.name += + i + "-" + j;
                        newRoom.SpawnEnemies(true);
                    }
                    else {
                        newRoom = Instantiate(room, new Vector3(i * offset.x, 2.5f, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                        newRoom.UpdateRoom(currentRoom.status);
                        newRoom.GetComponent<RoomBehavior>().player = player;
                        newRoom.name += + i + "-" + j;
                        if (i != 0 || j != 0) newRoom.SpawnEnemies(false);
                    }

                    floors.Add(newRoom.transform.GetChild(0).GetChild(0).GetComponent<BoxCollider>());
                    behaviors.Add(newRoom);
                }
            }
        }
        generateNavLinks.floors = floors;
        generateNavLinks.DoGenerateLinks();
        foreach (RoomBehavior rb in behaviors) {
            rb.getSurface();
            rb.navSurface.BuildNavMesh();
        }
    }
}
