using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleViews : MonoBehaviour
{
    public static ToggleViews Instance;
    [SerializeField] Animator _modelViewAnimator;
    public bool animationState;
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void ToggleModelView()
    {
        string triggerParams = animationState ? "CloseDrawer" : "OpenDrawer"; 
        _modelViewAnimator.SetTrigger(triggerParams);
        animationState = !animationState;
    }
}
