using UnityEngine;
using UnityEngine.UI;

public class ToggleViews : MonoBehaviour
{
    public static ToggleViews Instance;
    public Animator _modelViewAnimator;
    public Animator _categoryViewAnim;
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
        string triggerParam = !MatchCurrentAnimationState(_modelViewAnimator, "Close_Model_Drawer") ? "CloseDrawer" : "OpenDrawer"; 
        _modelViewAnimator.SetTrigger(triggerParam);
    }

    public bool MatchCurrentAnimationState(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
