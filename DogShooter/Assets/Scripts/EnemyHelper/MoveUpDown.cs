using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float _speed = 0.9f;
    [SerializeField] float height = 2f;
    [SerializeField] float startY = 0.5f;

    void Update()
    {
        var pos = transform.position;
        var newY = startY + height * Mathf.Sin(Time.time * _speed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}

