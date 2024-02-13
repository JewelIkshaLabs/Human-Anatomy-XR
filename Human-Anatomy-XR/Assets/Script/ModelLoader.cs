using UnityEngine;
using UnityEngine.UI;

public class ModelLoader : MonoBehaviour
{
    public GameObject _modelToBeLoaded;

    public void LoadModel()
    {
        UnloadModel();
        Instantiate(_modelToBeLoaded);
        GameObject.Find("UnIsolatePartsButton").GetComponent<Button>().interactable = false;
        // ToggleViews.RaiseOnModelViewStateChanged(false);
        // ToggleViews.RaiseOnCategoryViewStateChanged(!ToggleViews.Instance.MatchCurrentAnimationState(ToggleViews.Instance._categoryViewAnim ,"Open_Category_Drawer"), "NoToggle");
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
