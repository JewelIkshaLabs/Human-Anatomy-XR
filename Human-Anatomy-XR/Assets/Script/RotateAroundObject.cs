using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    public Transform _kneeTransform;
    [SerializeField] float _rotationSpeed;

    void Update()
    {
        RotateAroundAny(_kneeTransform);
    }

    void RotateAroundAny(Transform target)
    {
        transform.RotateAround(target.position, Vector3.up, _rotationSpeed);
    }
}
