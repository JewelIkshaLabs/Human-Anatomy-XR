using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLister : MonoBehaviour
{
    public List<GameObject> _parts;

    void Start()
    {
        ChildLister(_parts);
    }

    void ChildLister(List<GameObject> parts)
    {
        foreach(Transform child in transform)
        {
            parts.Add(child.gameObject);
        }
    }
}
