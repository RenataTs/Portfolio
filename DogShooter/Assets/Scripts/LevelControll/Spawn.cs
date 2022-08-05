using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] float rate = 5f;
    [SerializeField] int waves = 1;
    [SerializeField] GameObject[] enemies;

    private int counter = 55;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
    }

    void SpawnEnemy()
    {
        if (--counter == 0) CancelInvoke("SpawnEnemy");

        for (int i = 0; i < waves; i++)
        {
            Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(9f, Random.Range(-4f, 3f), 0f), Quaternion.identity);
        }
    }
}
