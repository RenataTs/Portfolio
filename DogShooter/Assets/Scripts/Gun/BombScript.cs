using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject ship;
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;

    public Vector2 Velocity;

    public bool isEnemy = false;

    public GameObject ExplosioEffect;
    public float force;

    public float GrenadeRadius;

    void Start()
    {
        StartCoroutine(explodeTimer());
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += Velocity * Time.deltaTime;
        transform.position = pos;
    }

    void Update()
    {
        Velocity = direction * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                Explode();
            }
        }
    }

    public void Explode()
    {
        GameObject ExplosionEff = Instantiate(ExplosioEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(ExplosionEff, 1);
        
        Collider2D[] touchedObjects = Physics2D.OverlapCircleAll(transform.position, GrenadeRadius);

        foreach (Collider2D touchedObject in touchedObjects)
        {
            Rigidbody2D rigidbody = touchedObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.AddForce(direction * force);
            }

            Ship target = touchedObject.gameObject.GetComponent<Ship>();
            if (target != null)
            {
                target.Hit(ExplosionEff);
            }
        }
    }

    IEnumerator explodeTimer()
    {
        yield return new WaitForSeconds(3);
        Explode();
    }
}
