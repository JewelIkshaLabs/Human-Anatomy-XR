using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[RequireComponent(typeof(MaterialTracker))]
public class PartLabeller : MonoBehaviour
{
    [Tooltip("Model to be named correctly")]
    public GameObject _model;
    bool labelledBreak = false;

    /// <summary>
    ///     <p>
    ///         Every gameobject inside of the model needs to be named according to some naming convention, since we want 
    ///         our models to be consistant with how they look, we can use their materials to name them correctly.
    ///     </p>
    ///     <br></br><br></br>
    ///     <p>
    ///         Warning!
    ///             The model should be applied with the materials available in the MaterialTracker lists. If you want to
    ///             add more materials for an object type you can append the material list of that category.
    ///     </p>
    /// </summary>

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
