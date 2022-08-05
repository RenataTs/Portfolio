using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBomb : MonoBehaviour
{

    public int powerUpLevelRequirement = 0;
    public bool isActive = false;

    [SerializeField] GameObject ExplosioEffect;
    [SerializeField] GameObject Arrow;

    [SerializeField] float shootIntervalSeconds = 0.5f;
    [SerializeField] float shootTimer = 0f;
    [SerializeField] float delayTimer = 0f;

    [SerializeField] bool autiShoot = false;


    Vector2 direction;

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector2.right).normalized;

        if (autiShoot)
        {
            if (shootTimer >= shootIntervalSeconds)
            {
                GameObject ArrowIns = Instantiate(Arrow, transform.position, Quaternion.identity);;

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

}
