using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialTracker))]
public class PartLabeller : MonoBehaviour
{
    public GameObject _model;
    bool labelledBreak = false;
    public void Nomenclate(GameObject model)
    {
        foreach(Transform child in model.transform)
        {   
            labelledBreak = false;
            if(child.name == "Armature") continue;
            Material childMat = child.GetComponent<Renderer>().sharedMaterial;
            SearchFromMap(childMat, child);
        }
    }

    void SearchFromMap(Material childMat, Transform child)
    {
        foreach(KeyValuePair<List<Material>,string> kvp in MaterialTracker.Instance.bodyMats)
        {
            foreach(Material mat in kvp.Key)
            {
                if(ReferenceEquals(mat, childMat))
                {
                    child.transform.gameObject.name += kvp.Value;
                    labelledBreak = true;
                    break;
                }
            }

            if(labelledBreak) break;
        }
    }
}
