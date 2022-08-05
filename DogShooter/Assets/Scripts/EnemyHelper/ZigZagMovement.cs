using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    [SerializeField] float _frequency = 1.0f;
    [SerializeField] float _amplitude = 5.0f;
    [SerializeField] float _cycleSpeed = 1.0f;
    [SerializeField] bool boss = false;

    private Vector3 pos;
    private Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        axis = transform.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos += Vector3.down * Time.deltaTime * _cycleSpeed;
        transform.position = pos + axis * Mathf.Sin(Time.time * _frequency) * _amplitude;

        if (boss)
        {
            if (pos.x > 8f)
            {
                pos.x = 8f;
            }
        }
    }
}
