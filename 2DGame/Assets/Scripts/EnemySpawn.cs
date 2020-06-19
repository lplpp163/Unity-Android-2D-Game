using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemy;
    public float spawnTime = 10f;
    float time = 0f;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= spawnTime)
        {
            time = 0f;

            Instantiate(Enemy[Random.Range(0, Enemy.Length)], transform.position, Quaternion.identity);
        }
    }
}
