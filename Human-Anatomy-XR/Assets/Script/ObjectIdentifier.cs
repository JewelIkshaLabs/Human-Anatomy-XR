using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P = UnityEngine.Physics;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] Camera _maincamera;
    void Awake()
    {
        _maincamera = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Identify();
        }
    }

    void Identify()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _maincamera.ScreenPointToRay(mousePosition);
        if(P.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
