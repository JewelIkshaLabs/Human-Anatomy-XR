using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PartHider : MonoBehaviour
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
       try
       {
            Renderer[] objects = category.transform.GetComponentsInChildren<Renderer>();
            ToggleMesh(objects);
       }
       catch 
       {
            Debug.LogError("Renderer Not Found!");
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

            ChangeColor(renderer.enabled);
        }
    }

    public void ChangeColor(bool state)
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
