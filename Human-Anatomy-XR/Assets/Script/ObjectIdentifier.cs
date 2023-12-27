using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P = UnityEngine.Physics;

public class ObjectIdentifier : MonoBehaviour
{
    private Camera _maincamera;
    void Awake()
    {
        _maincamera = Camera.main;
    }

    // GameObject Identify()
    // {
    //     Vector3 mousePosition = Input.mousePosition;
    //     Ray ray = _maincamera.ScreenPointToRay(mousePosition);
    //     if(P.Raycast(ray, out RaycastHit hit))
    //     {
            
    //     }
    // }
}
