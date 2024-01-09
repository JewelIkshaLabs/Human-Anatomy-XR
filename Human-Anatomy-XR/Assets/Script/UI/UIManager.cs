using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private CanvasScaler canvasScaler;
    void Start()
    {
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        Resolution[] resolutions = Screen.resolutions;
        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRateRatio);
           
        }

        canvasScaler.referenceResolution = new Vector2(resolutions[0].width, resolutions[0].height );
        
    }
}
