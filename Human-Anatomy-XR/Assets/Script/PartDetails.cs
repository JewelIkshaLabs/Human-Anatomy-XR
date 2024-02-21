using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XR;
using DG.Tweening;
using UnityEngine.UI;

public class PartDetails : MonoBehaviour
{
    public static PartDetails Instance;
    public GameObject loadingIcon;
    public TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI partName;
    [SerializeField] GameObject model;
    [SerializeField] GameObject optionsPanel;
    Transform startTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed;
    private VoiceInput voiceInput;
    private OutlineSelection outlineSelection;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        voiceInput = FindFirstObjectByType<VoiceInput>();
        outlineSelection = FindFirstObjectByType<OutlineSelection>();
        startTransform = model.transform;
        var lookAtConstraint = GetComponent<LookAtConstraint>();
        Transform cameraTransform = FindFirstObjectByType<TrackedPoseDriver>().transform;
        ConstraintSource constraintSource = new()
        {
            sourceTransform = cameraTransform,
            weight = 1
        };
        lookAtConstraint.SetSource(0,constraintSource);
        partName.text = model.name;
    }

    void Update()
    {
        Vector3 playerForward = Vector3.forward.normalized;
        transform.position = Vector3.Lerp(transform.position, startTransform.position + 
        offset + playerForward, followSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    public void UpdateName(GameObject part)
    {
        optionsPanel.SetActive(true);
        string anatomicalName = part.name.Split('_')[0];
        partName.text = anatomicalName;
        // transform.parent = part.transform;
        startTransform = part.transform;
    }
    public void ResetModel()
    {
        partName.text = model.name;
        startTransform = model.transform;
        OutlineSelection._selectedGameObject = null;
    }

    public void ExplainPart()
    {
        if(OutlineSelection._selectedGameObject != null)
        {
            Debug.Log(OutlineSelection._selectedGameObject.name + " explained!");
            if(!voiceInput.audioSource.isPlaying) StartCoroutine(voiceInput.PostRequest("",$"Tell me about {OutlineSelection._selectedGameObject.name} in 10 words"));
        }
        else if(!voiceInput.audioSource.isPlaying) StartCoroutine(voiceInput.PostRequest("",$"Tell me about {model.name} in 10 words"));
    }

    public void ShowAllParts()
    {
        outlineSelection.UnIsolatePart();
        ResetModel();
    }

}
