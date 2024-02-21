using UnityEngine;
using DG.Tweening;

public class ConstrainToRoom : MonoBehaviour
{
    Collider _collider;
    void Start()
    {
        TryGetComponent(out _collider);
    }

    void Update()
    {
        Constrain();
    }

    void Constrain()
    {
        if(!enabled) return;
        var roomBounds = RoomBounds.Instance;
        if(!roomBounds) return;

        var boxCollider = roomBounds.boxCollider;
        // if(_collider)   
        // {
        //     var boundsCenter = _collider.bounds.center;
		// 	transform.position += boxCollider.ClosestPoint(boundsCenter) - boundsCenter;
        // }
        // else
        // {
        //     transform.position = boxCollider.ClosestPoint(transform.position);
        // }
        transform.position = boxCollider.ClosestPoint(transform.position);
    }
}
