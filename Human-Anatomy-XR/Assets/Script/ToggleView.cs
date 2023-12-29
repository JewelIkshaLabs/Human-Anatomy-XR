using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleView : MonoBehaviour
{
    public Color defaultColor;
    public Color updatedColor;
    Image buttonImage;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
       GameObject category = GameObject.Find(this.name);
       if(category.transform.GetComponentInChildren<SkinnedMeshRenderer>())
       {
            SkinnedMeshRenderer[] objects = category.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
            ToggleMesh(objects);
       }
       else
       {
            MeshRenderer[] objects = category.transform.GetComponentsInChildren<MeshRenderer>();
            ToggleMesh(objects);
       }
       
    }

    void ToggleMesh(Renderer[] renderers)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = !renderer.enabled;

            MeshCollider meshCollider = renderer.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = renderer.enabled;
            }

            ChangeColour(renderer.enabled);
        }
    }

    public void ChangeColour(bool state)
    {
        if(state)
        {
            Color toggleColor = defaultColor;
            buttonImage.color = toggleColor;
        }
        else
        {
            Color toggleColor = updatedColor;
            buttonImage.color = toggleColor;
        }
    }
}
