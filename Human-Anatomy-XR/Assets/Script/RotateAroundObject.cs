using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RotateAroundObject : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] Vector3 _orbitCenter;
    [SerializeField] float _orbitSpeed;
    [SerializeField] Scrollbar _scrollbar_Horizontal;
    [SerializeField] Scrollbar _scrollbar_Vertical;

    void Update()
    {
        DoOrbit(ScrollValue()); 
    }
    
    Vector2 ScrollValue()
    {
        float horizontalScroll = _scrollbar_Horizontal.value - 0.5f;
        float verticalScroll = _scrollbar_Vertical.value - 0.5f;
        Vector2 scrollValues = new Vector2(horizontalScroll,verticalScroll);
        return scrollValues;
    }

    void DoOrbit(Vector2 delta)
    {
        transform.Rotate(Vector3.down, delta.x, Space.World);
        transform.Rotate(Vector3.left, delta.y, Space.World);
    }
}
