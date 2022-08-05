using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInScene : MonoBehaviour
{
    [SerializeField] float _speed = 0.9f;
    [SerializeField] float height = 2f;
    [SerializeField] float startX = 0.5f;

    void Update()
    {
        var pos = transform.position;
        var newX = startX + height * Mathf.Sin(Time.time * _speed);
        transform.position = new Vector3(newX, pos.y, pos.z);
    }
}

