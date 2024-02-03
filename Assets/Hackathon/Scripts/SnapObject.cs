using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapObject : MonoBehaviour
{
    public static Dictionary<GameObject, SnapObject> snappedObjects = new Dictionary<GameObject, SnapObject>();
    [Tooltip("Make sure u add a trigger collider so this script actually works")]
    public Vector3 snapPos;

    
    public static void RemoveObject(GameObject obj)
    {
        print(obj.name);
        //Removes the gameobject from the snap list
        if (!snappedObjects.ContainsKey(obj)) { return; }
        foreach (var snappedObject in snappedObjects)
        {
            if(snappedObject.Key != obj) { continue; }
            obj.GetComponent<Rigidbody>().isKinematic = false;
            snappedObjects.Remove(obj);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) { return; }
        //Adds teh gameobject to the snap list
        if(!snappedObjects.ContainsKey(other.gameObject))
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>().isSelected) { return; }
            other.transform.position = transform.position + snapPos;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            snappedObjects.Add(other.gameObject, this);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + snapPos, .1f);
    }
}
