using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralGun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;
    public float shootIntervalSeconds = 0.5f;
    public float shootSelaySeconds = 0.0f;
    public float shootTimer = 5f;
    public float delayTimer = 0f;
    public bool isActive = false;
    public bool autoShoot = false;

    public Bullet bullet;
    public Vector2 direction;

    private void Update()
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
                SpiralShoot();
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

    public void SpiralShoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}
