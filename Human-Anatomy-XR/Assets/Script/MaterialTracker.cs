using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class MaterialTracker : MonoBehaviour
{

    public static MaterialTracker Instance;

    [Header("Bone")]
    [Space]
    public List<Material> boneMaterials = new();
    [Header("Muscle")]
    [Space]
    public List<Material> muscleMaterials = new();
    [Header("Ligament")]
    [Space]
    public List<Material> ligamentMaterials = new();
    [Header("Tendon")]
    [Space]
    public List<Material> tendonMaterials = new();
    [Header("Cartilage")]
    [Space]
    public List<Material> cartilageMaterials = new();
    [Header("Joint")]
    [Space]
    public List<Material> jointMaterials = new();
    public Dictionary<List<Material>, string> bodyMats;


    MaterialTracker()
    {
        InitializeVariables();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void InitializeVariables()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        bodyMats = new Dictionary<List<Material>, string>{
            {boneMaterials, "_bone"},
            {muscleMaterials, "_muscle"},
            {ligamentMaterials, "_ligament"},
            {tendonMaterials, "_tendon"},
            {cartilageMaterials, "_cartilage"},
            {jointMaterials, "_joint"}
        };
    }


}
