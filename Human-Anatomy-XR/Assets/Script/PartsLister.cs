using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class PartsLister : MonoBehaviour
{
    public List<GameObject> _parts = new List<GameObject>();
    public Dictionary<string, string> _categoriesMap = new Dictionary<string, string>();
    public List<string> _categories = new List<string>();
    public List<GameObject> _categoryGameObjects;
    public List<Sprite> buttonImages;
    public GameObject _categoryPrefab;
    public GameObject _buttonPrefab;
    public Transform _contentTransform;

    void Start()
    {
        ChildLister(_parts);
        PopulateCategoriesFromParts(_parts, transform, _contentTransform);
        CreateGameObjects(_parts);
    }

    void ChildLister(List<GameObject> parts)
    {
        foreach(Transform child in transform)
        {
            parts.Add(child.gameObject);
        }
    }

    public void LoadCase()
    {
        ChildLister(_parts);
        PopulateCategoriesFromParts(_parts, transform, _contentTransform);
        CreateGameObjects(_parts);
    }

    void PopulateCategoriesFromParts(List<GameObject> parts, Transform parent, Transform contentTransform)
    {
        HashSet<string> categories = new HashSet<string>();

        foreach (GameObject part in parts)
        {
            string[] partNameSplit = part.name.Split('_');
            if (partNameSplit.Length > 1)
            {
                string category = partNameSplit[partNameSplit.Length - 1]; // Get the last part of the name
                categories.Add(category);
            }
        }

        foreach (string category in categories)
        {
            GameObject cat = Instantiate(_categoryPrefab);
            GameObject button = Instantiate(_buttonPrefab);
            cat.name = category;
            cat.transform.parent = parent;
            button.name = category;
            foreach (Sprite buttonImage in buttonImages)
            {
                Debug.Log("Catergory : " +  category);
                Debug.Log("ButtonImageName : " + buttonImage.name);
                if (buttonImage.name == category)
                {
                    button.GetComponent<Image>().sprite = buttonImage;
                    break;
                }
            }
            button.transform.parent = contentTransform;
            _categoryGameObjects.Add(cat);
        }
    }

    // Sorts the gameobjects into particular categories

    void CreateGameObjects(List<GameObject> parts)
    {
        foreach (GameObject part in parts)
        {
            string[] partNameSplit = part.name.Split('_');
            if (partNameSplit.Length > 1)
            {
                string category = partNameSplit[partNameSplit.Length - 1]; // Get the last part of the name
                GameObject categoryGameObject = _categoryGameObjects.FirstOrDefault(cat => cat.name == category);
                if (categoryGameObject != null)
                {
                    part.transform.parent = categoryGameObject.transform;
                }
            }
        }
    }

    void ConsoleOutput()
    {
        foreach(KeyValuePair<string,string> kvp in _categoriesMap)
        {
            Debug.Log(kvp);
        }
    }
}
