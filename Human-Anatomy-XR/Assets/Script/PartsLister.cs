using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public class PartsLister : MonoBehaviour
{
    public List<GameObject> _parts = new List<GameObject>();
    public Dictionary<string, string> _categoriesMap = new Dictionary<string, string>();
    Dictionary<string, List<string>> _jsonData = new Dictionary<string, List<string>>()
    {
        { "bone", new List<string> { "Femur_bone", "Fibula_bone", "Patella_bone", "Tibia_bone" } },
        { "cartilage", new List<string> { "Femur_cartilage", "Patella_cartilage", "Tibia_cartilage" } },
        { "muscle", new List<string> { "hamstring_lat_muscle", "hamstring_med_muscle", "quad_muscle" } },
        { "ligament", new List<string> { "lateral_collateral_ligament", "medial_collateral_ligament" } },
        { "tendon", new List<string> { "patellar_tendon", "quadriceps_tendon" } }
    };
    public List<string> _categories = new List<string>();
    public List<GameObject> _categoryGameObjects;
    public GameObject _categoryPrefab;
    public GameObject _buttonPrefab;
    public Transform _contentTransform;

    void Start()
    {
        ChildLister(_parts);
        PopulateCategoriesFromJson(_jsonData, transform, _contentTransform);
        // ConsoleOutput();
        CreateGameObjects(_jsonData);
    }

    void ChildLister(List<GameObject> parts)
    {
        foreach(Transform child in transform)
        {
            parts.Add(child.gameObject);
        }
    }

    void PopulateCategoriesFromJson(Dictionary<string, List<string>> jsonData, Transform parent, Transform contentTransform)
    {
        foreach(KeyValuePair<string, List<string>> kvp in jsonData)
        {
            _categories.Add(kvp.Key);
            GameObject cat = Instantiate(_categoryPrefab);
            GameObject button = Instantiate(_buttonPrefab);
            cat.name = kvp.Key;
            cat.transform.parent = parent;
            button.name = kvp.Key;
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = kvp.Key;
            button.transform.parent = contentTransform;
            _categoryGameObjects.Add(cat);
        }
    }

    // Sorts the gameobjects into particular categories

    void CreateGameObjects(Dictionary<string, List<string>> jsonData)
    {
        foreach(GameObject part in _parts)
        {
            string searchValue = part.name;
            string foundKey = jsonData.FirstOrDefault(x => x.Value.Contains(searchValue)).Key;
            foreach(GameObject categoryGameObject in _categoryGameObjects)
            {
                if(categoryGameObject.name == foundKey)
                {
                    part.transform.parent = categoryGameObject.transform;
                    break;
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
