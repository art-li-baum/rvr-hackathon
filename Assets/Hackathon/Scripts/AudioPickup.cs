using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class AudioPickup : MonoBehaviour
{
    [SerializeField] private AudioClip clipForItem;
    private XRGrabInteractable _xrGrabInteractable;
    private void OnEnable()
    {
        _xrGrabInteractable = GetComponent<XRGrabInteractable>();
        
        _xrGrabInteractable.selectEntered.AddListener(PlayMyClip);

    }

    private void OnDisable()
    {
        _xrGrabInteractable.selectEntered.RemoveListener(PlayMyClip);
    }

    public void PlayMyClip(SelectEnterEventArgs args)
    {
       if(args.interactableObject == (IXRSelectInteractable)_xrGrabInteractable)
            ClipManager.Instance.PlayQuote(clipForItem);
    }

}
