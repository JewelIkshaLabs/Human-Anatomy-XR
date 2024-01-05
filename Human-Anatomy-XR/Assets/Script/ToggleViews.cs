using UnityEngine;

public class ToggleViews : MonoBehaviour
{
    public static ToggleViews Instance;
    public Animator _modelViewAnimator;
    public Animator _categoryViewAnim;
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

    public bool MatchCurrentAnimationState(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
