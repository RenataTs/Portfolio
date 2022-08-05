using UnityEngine;

public class Orbit2D : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] float velocidad = 0f;
    [SerializeField] Vector2 Velocity;

    void FixedUpdate()
    {
        transform.RotateAround(ship.transform.localPosition, -Vector3.back, Time.deltaTime * velocidad);
    }
}
