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
    // [SerializeField] InputActionReference orbitActionReference;
    // InputAction _orbitAction;

    // void OnEnable()
    // {
    //     _orbitAction = orbitActionReference.action.Clone();
    //     _orbitAction.Enable();
    // }

    // void OnDisable()
    // {
    //     _orbitAction.Dispose();
    //     _orbitAction = null;
    // }

    void Update()
    {
        DoOrbit1(ScrollValue()); 
    }

    void RotateAroundAny(Vector3 target)
    {
        transform.RotateAround(target, Vector3.up, _rotationSpeed*Time.deltaTime);
    }
    
    Vector2 ScrollValue()
    {
        float horizontalScroll = _scrollbar_Horizontal.value - 0.5f;
        float verticalScroll = _scrollbar_Vertical.value - 0.5f;
        Vector2 scrollValues = new Vector2(horizontalScroll,verticalScroll);
        return scrollValues;
    }

    void DoOrbit1(Vector2 delta)
    {
        transform.Rotate(Vector3.down, delta.x);
        transform.Rotate(Vector3.left, delta.y);
    }

    void DoOrbit(Vector2 delta)
    {
        _orbitCenter = transform.position;
		Vector3 focusPosition = _orbitCenter;

		var camPosition = transform.position;

		var focusToCam = focusPosition;
		float focusDistance = focusToCam.magnitude;

		float distanceH = new Vector2(focusToCam.x, focusToCam.z).magnitude;
		float distanceV = focusToCam.y;

		float angleH = Mathf.Atan2(focusToCam.z, focusToCam.x);
		float angleV = Mathf.Asin(distanceV / Mathf.Sqrt(distanceH * distanceH + distanceV * distanceV));

		var scaledDelta = delta * _orbitSpeed;
		angleH -= scaledDelta.x;
		angleV = Mathf.Clamp(angleV - scaledDelta.y, -1.4f, 1.4f);

		float y = focusDistance * Mathf.Sin(angleV);
		float c = focusDistance * Mathf.Sin(Mathf.PI * 0.5f - angleV);
		float z = c * Mathf.Sin(angleH);
		float x = c * Mathf.Sin(Mathf.PI * 0.5f - angleH);

		focusToCam = new Vector3(x, y, z);

		transform.position = focusPosition + focusToCam;

		// Make the camera look at the focus position
		transform.LookAt(focusPosition);
    }
}
