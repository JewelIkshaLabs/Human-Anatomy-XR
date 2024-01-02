using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    public GameObject _modelToBeLoaded;
    [SerializeField] Animator _modelViewAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadModel()
    {
        UnloadModel();
        _modelViewAnimator.SetTrigger("CloseDrawer");
        ToggleViews.Instance.animationState = false;
        GameObject model = Instantiate(_modelToBeLoaded);
        Camera.main.GetComponent<RotateAroundObject>()._kneeTransform = model.transform;
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
