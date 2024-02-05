using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    public GameObject _selectedGameObject;
    private PartsLister _partsLister;
    private VoiceInput voiceInput;
    [SerializeField] Button unIsolateButton;
    private GameObject highlightGameobject;

    void Start()
    {
        voiceInput = FindFirstObjectByType<VoiceInput>();
    }
    void Update()
    {
        Highlight();
        Select();
        HighlightFromSpeech();
    }

    void HighlightFromSpeech()
    {
        if (highlightGameobject != null || highlight != null)
        {
            try
            {
                highlightGameobject.GetComponent<Outline>().enabled = false;
            }
            catch {}
            highlightGameobject = null;
        }
        highlightGameobject = GameObject.Find(VoiceInput.highlightString);
        if(highlightGameobject != null && highlightGameobject.CompareTag("Selectable"))
        {
            if (highlightGameobject.GetComponent<Outline>() != null)
            {
                highlightGameobject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Outline outline = highlightGameobject.AddComponent<Outline>();
                outline.enabled = true;
                highlightGameobject.GetComponent<Outline>().OutlineColor = Color.yellow;
                highlightGameobject.GetComponent<Outline>().OutlineWidth = 10.0f;
            }
        }
        else
        {
            highlightGameobject = null;
        }
    }

    void Highlight()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }
    }

    void Select()
    {

        // Selection

        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                Debug.Log(highlight.gameObject.name);
                _selectedGameObject = highlight.gameObject;
                if(!voiceInput.audioSource.isPlaying) StartCoroutine(voiceInput.PostRequest("",$"Tell me about {_selectedGameObject.name} in 10 words"));
                if(!unIsolateButton.IsInteractable()) IsolatePart(_selectedGameObject);
                unIsolateButton.interactable = true;
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
            }
            else if (selection)
            {
                selection.gameObject.GetComponent<Outline>().enabled = false;
                selection = null;
            }
        }
    }

    void IsolatePart(GameObject selectedGameobject)
    {
        _partsLister = FindFirstObjectByType<PartsLister>();
        List<GameObject> parts = _partsLister._parts;
        foreach(GameObject part in parts)
        {
            if(part == selectedGameobject) continue;
            part.GetComponent<Renderer>().enabled = false;
            part.GetComponent<Collider>().enabled = false;
        }
        ToggleViews.RaiseOnCategoryViewStateChanged(false, null);
    }

    public void UnIsolatePart()
    {
        List<GameObject> parts = _partsLister._parts;
        foreach(GameObject part in parts)
        {
            part.GetComponent<Renderer>().enabled = true;
            part.GetComponent<Collider>().enabled = true;
        }
        ToggleViews.RaiseOnCategoryViewStateChanged(true, null); 
    }

}
