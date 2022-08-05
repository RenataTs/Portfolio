using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] bool canBeDestroyed = false;
    [SerializeField] bool  boss = false;

    [SerializeField] int score;
    [SerializeField] int enemyHelth;

    [SerializeField] GameObject ExplosioEffect;
    [SerializeField] GameObject speedBonus;
    [SerializeField] GameObject gunBonus;
    [SerializeField] GameObject spiralGunBonus;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject helth;
    [SerializeField] GameObject portal;
    [SerializeField] HelthBarBehavior helthBar;

    private int maxHitPoints;


    public void Start()
    {
        maxHitPoints = enemyHelth;
        helthBar.SetHealth(enemyHelth, maxHitPoints);
    }

    void Update()
    {
        if (transform.position.x < 17.0f && !canBeDestroyed)
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            SpiralGun[] spiralGuns = transform.GetComponentsInChildren<SpiralGun>();


            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }

            foreach (SpiralGun spiralGun in spiralGuns)
            {
                spiralGun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                Destroy(bullet.gameObject);
                --enemyHelth;

                helthBar.SetHealth(enemyHelth, maxHitPoints);
                if (enemyHelth == 0)
                {
                    if (Random.Range(0, 10) == 0)
                    {
                        Instantiate(speedBonus, transform.position, Quaternion.identity);
                    }
                    else if (Random.Range(0, 10) == 1)
                    {
                        Instantiate(gunBonus, transform.position, Quaternion.identity);
                    }
                    else if (Random.Range(0, 10) == 2)
                    {
                        Instantiate(shield, transform.position, Quaternion.identity);
                    }
                    else if (Random.Range(0, 10) == 3)
                    {
                        Instantiate(helth, transform.position, Quaternion.identity);
                    }
                    else if (Random.Range(0, 10) == 4)
                    {
                        Instantiate(spiralGunBonus, transform.position, Quaternion.identity);
                    }

                    PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
                    GameObject ExplosionEffectIns = Instantiate(ExplosioEffect, transform.position, Quaternion.identity);

                    Destroy(gameObject);

                    if (boss)
                    {
                        Instantiate(portal, transform.position, Quaternion.identity);
                        Shake shake = Camera.main.GetComponent<Shake>();
                        shake.start = true;
                    }

                    Destroy(ExplosionEffectIns, 1);
                }
            }
        }
    }
}
