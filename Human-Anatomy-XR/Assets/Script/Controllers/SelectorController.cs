using UnityEngine;

public class SelectorController : MonoBehaviour
{
    void OnEnable()
    {
        // Subscription of Events
        ToggleViews.OnModelViewStateChanged += ModelSelectorState;
        ToggleViews.OnCategoryViewStateChanged += CategorySelectorState;
    }

    void OnDisable()
    {
        // Unsubscription of Events
        ToggleViews.OnModelViewStateChanged -= ModelSelectorState;
        ToggleViews.OnCategoryViewStateChanged -= CategorySelectorState;
    }

    void ModelSelectorState(bool state)
    {
        if(state) ToggleViews.Instance._modelViewAnimator.SetTrigger("OpenDrawer"); 
        else ToggleViews.Instance._modelViewAnimator.SetTrigger("CloseDrawer");
    }

    void CategorySelectorState(bool state, string id)
    {
        if(state) ToggleViews.Instance._categoryViewAnim.SetTrigger("OpenDrawer");
        else if(!state && id=="NoToggle") {} 
        else ToggleViews.Instance._categoryViewAnim.SetTrigger("CloseDrawer");
    }
}
