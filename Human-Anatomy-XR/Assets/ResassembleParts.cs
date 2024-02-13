using UnityEngine;
using DG.Tweening;

public class ResassembleParts : MonoBehaviour
{
    public void Reassemble()
    {
        PartsLister partsLister = FindFirstObjectByType<PartsLister>();
        if(partsLister != null)
        {
            var parts = partsLister._parts;
            var partsPos = partsLister._partsPosition; 
            var partsRot = partsLister._partsRotation;
            for(int i = 0; i < parts.Count; i++)
            {
                parts[i].transform.DOMove(partsPos[i],3,false);
                parts[i].transform.rotation = partsRot[i];
            }
        }
    }
}
