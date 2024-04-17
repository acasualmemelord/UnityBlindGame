using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour {
    public GameObject[] walls;
    public GameObject[] doors;
    public GameObject enemy;

    public void UpdateRoom(bool[] status) {
        for (int i = 0; i < status.Length; i ++) {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    public void SpawnEnemies() {
        for (int i = 1; i < 5; i ++) {
            var rand = Random.Range(0, 2);
            if (rand > 0) {
                Debug.Log(rand);
                var _ = Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }
    }
}
