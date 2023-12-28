using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleView : MonoBehaviour
{
    public Color defaultColor;
    public Color updatedColor;
    // Start is called before the first frame update
    void Start()
    {
        
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
            ChangeColour(obj.enabled);
       }
       
    }

    public void ChangeColour(bool state)
    {
        Button button = GetComponent<Button>();
        ColorBlock colors = button.colors;
        Debug.Log(state);
        if (state)
        {
            colors.normalColor = new Color(0, 0, 0); 
        }
        else
        {
            colors.normalColor = new Color(10, 255, 5); 
        }
        button.colors = colors;
    }
}
