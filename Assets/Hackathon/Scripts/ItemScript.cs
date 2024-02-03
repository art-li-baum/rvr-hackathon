using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.State;

public class ItemScript : MonoBehaviour
{
    [SerializeField]private AudioClip clipForItem;

    [SerializeField]private Mesh meshForItem;
    [SerializeField]private Material materialForItem;

    [SerializeField] private GameObject interactableObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(interactableObject, transform);
        GetComponentInChildren<MeshFilter>().mesh = meshForItem;
        GetComponentInChildren<MeshRenderer>().sharedMaterial = materialForItem;

        XRGrabInteractable _XRGrabInteractable = gameObject.GetComponentInChildren<XRGrabInteractable>();        
        

        _XRGrabInteractable.selectEntered.AddListener(delegate { AudioManager.instance.PlayTriggerAudio(clipForItem); });
    }

}
