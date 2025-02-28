using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoomBehavior : MonoBehaviour {
    public GameObject[] walls;
    public GameObject[] doors;
    public GameObject enemy;
    public GameObject player;
    public NavMeshSurface navSurface;
    public bool[] status = new bool[4];

    public void getSurface() {
        navSurface = transform.GetComponentInChildren<NavMeshSurface>();
    }

    public void UpdateRoom(bool[] status) {
        this.status = status;
        for (int i = 0; i < status.Length; i ++) {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    public void SpawnEnemies(bool endRoom) {
        int enemyID = 0;
        for (int i = 1; i < (endRoom ? transform.GetChild(0).childCount : 5); i ++) {
            var rand = Random.Range(0, 2);
            var pos = transform.GetChild(0).GetChild(i);
            if (rand > 0 || endRoom) {
                var newEnemy = Instantiate(enemy, pos.position, Quaternion.identity);
                newEnemy.transform.GetComponentInChildren<EnemySystem>().player = player;
                newEnemy.name = "Enemy " + enemyID++ + " of " + transform.name;
            }
        }
    }
}
