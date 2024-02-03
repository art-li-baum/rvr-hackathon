using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = System.Random;

[RequireComponent(typeof(XRGrabInteractable))]
public class AudioPickup : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clipsForItem;
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
            ClipManager.Instance.PlayQuote(clipsForItem[ UnityEngine.Random.Range(0,clipsForItem.Count)]);
    }

}
