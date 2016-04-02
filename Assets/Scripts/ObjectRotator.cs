using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour
{

    public Transform point;
    public Vector3 axis;
    public float angle;

    void Update()
    {
        transform.RotateAround(point.position, axis, angle * Time.deltaTime);
    }
}