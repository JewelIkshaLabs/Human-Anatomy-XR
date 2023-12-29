using System.Collections;
using System.Collections.Generic;
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
       SkinnedMeshRenderer[] objects = category.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
       foreach (SkinnedMeshRenderer obj in objects)
       {
            obj.enabled = !obj.enabled;
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                meshCollider.enabled = obj.enabled;
            }
            ChangeColour(obj.enabled);
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
