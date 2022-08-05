using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalGun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public bool isActive = false;

    [SerializeField] bool autoShoot = false;
    [SerializeField] Bullet bullet;

    [SerializeField] float shootIntervalSeconds = 0.5f;
    [SerializeField] float shootSelaySeconds = 0.0f;
    [SerializeField] float shootTimer = 0f;
    [SerializeField] float delayTimer = 0f;

    Vector2 direction;

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot)
        {
            if (shootTimer >= shootIntervalSeconds)
            {
                Shoot();
                shootTimer = 0;
            }
            else
            {
                shootTimer += Time.deltaTime;
            }
        }
        else
        {
            delayTimer += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}
