using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void DoOrbit(Vector2 delta)
    {
		Vector3 focusPosition = _orbitCenter;

		var camPosition = transform.position;

		var focusToCam = camPosition - focusPosition;
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
