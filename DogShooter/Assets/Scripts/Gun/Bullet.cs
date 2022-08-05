using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;

    public Vector2 Velocity;

    public bool isEnemy = false;

    void Start()
    {
        Destroy(gameObject, 5); 
    }

    void Update()
    {
        Velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += Velocity * Time.deltaTime;
        transform.position = pos;
    }
}
