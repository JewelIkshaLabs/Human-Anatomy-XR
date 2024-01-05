using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    public GameObject _modelToBeLoaded;
  
    public void LoadModel()
    {
        UnloadModel();
        Instantiate(_modelToBeLoaded);
        ToggleViews.Instance._modelViewAnimator.SetTrigger("CloseDrawer");
        ToggleViews.Instance.animationState = false;
        if(!ToggleViews.Instance.MatchCurrentAnimationState(ToggleViews.Instance._categoryViewAnim ,"Open_Category_Drawer"))
        {
            ToggleViews.Instance._categoryViewAnim.SetTrigger("OpenDrawer");
        }
    }

    private void UnloadModel()
    {
        GameObject[] models = GameObject.FindGameObjectsWithTag("BodyPart");
        foreach(GameObject model in models)
        {
            Destroy(model);
        }
    }
}
