using System;
using UnityEngine;

public class RoomBounds : MonoBehaviour
{
    public static RoomBounds Instance {get; set;}
    public BoxCollider boxCollider {get; set;}
    public static event Action InstanceChanged;

    void OnEnable()
    {
        Instance = this;
        boxCollider = GetComponent<BoxCollider>();
        InstanceChanged?.Invoke();
    }

    void OnDisable()
    {
        if(!ReferenceEquals(Instance, this)) return;
        Instance = null;
        InstanceChanged?.Invoke();
    }
}
