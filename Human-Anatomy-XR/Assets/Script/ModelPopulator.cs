using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModelPopulator : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] modelPaths;
    public GameObject model_button_prefab;
    public Transform modelContent;
    public List<GameObject> modelPrefabs;
    public List<Sprite> modelSprites;
    public Button unIsolateButton;
    void Start()
    {
        modelPaths = Directory.GetFiles("Assets/Resources/", "*.prefab");
        foreach(string path in modelPaths)
        {
            string fileName = Path.ChangeExtension(path.Split("/")[path.Split("/").Length - 1], null);
            GameObject prefab = Resources.Load(fileName) as GameObject;
            GameObject model_button = Instantiate(model_button_prefab, modelContent);
            model_button.name = prefab.name;
            model_button.GetComponent<ModelLoader>()._modelToBeLoaded = prefab;
            PlaceSpriteOverModelButton(model_button);
            modelPrefabs.Add(prefab);
            unIsolateButton.interactable = false;
        }
    }

    void PlaceSpriteOverModelButton(GameObject model_button)
    {
        foreach(var modelSprite in modelSprites)
        {
            if(modelSprite.name == model_button.name)
            {
                model_button.GetComponent<Image>().sprite = modelSprite;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
