using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] bool boss = false;

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= moveSpeed * Time.deltaTime;
        transform.position = pos;

        if (boss)
        {
            if (pos.x < 5f)
            {
                moveSpeed = 0;
            }
        }

        if (pos.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
