using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    [SerializeField] float sinCenterY;
    [SerializeField] float amplitude = 2;
    [SerializeField] float frequency = 0.5f;

    [SerializeField] bool inverted = false;

    private void Start()
    {
        sinCenterY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frequency) * amplitude;

        if (inverted)
        {
            sin *= -1;
        }

        pos.y = sinCenterY + sin;

        transform.position = pos;
    }
}
