using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
    [SerializeField] float _speed = 4f;

    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(translation: Vector3.left * _speed * Time.deltaTime);
        if (transform.position.x < -17.5f)
        {
            transform.position = StartPosition;
        }
    }
}
