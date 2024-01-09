using UnityEngine;
using UnityEngine.Events;

public class ToggleViews : MonoBehaviour
{
    public static ToggleViews Instance;
    public Animator _modelViewAnimator;
    public Animator _categoryViewAnim;
    public static event UnityAction<bool> OnModelViewStateChanged;
    public static event UnityAction<bool, string> OnCategoryViewStateChanged;
    public static void RaiseOnModelViewStateChanged(bool state) => OnModelViewStateChanged?.Invoke(state);
    public static void RaiseOnCategoryViewStateChanged(bool state, string id) => OnCategoryViewStateChanged?.Invoke(state, id);
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
        RaiseOnModelViewStateChanged(MatchCurrentAnimationState(_modelViewAnimator, "Close_Model_Drawer"));
    }

    public bool MatchCurrentAnimationState(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
