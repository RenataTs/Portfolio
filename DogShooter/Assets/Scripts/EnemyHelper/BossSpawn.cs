using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] float spawnTime;
    [SerializeField] GameObject spawn;

    void Start()
    {
        Invoke("SpawnBoss", spawnTime);
    }

    void SpawnBoss()
    {
        Instantiate(spawn, new Vector3(9f, Random.Range(-4f, 3f), 0f), Quaternion.identity);
    }
}
