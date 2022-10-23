using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    public static float distance = float.MaxValue;
    public static GameObject selectedInteract;
    public static float InteractDistance = 5;

    public static List<Interactable> interactables = new List<Interactable>();

    private void Update() {
        // If there is a selected interactable
        if(selectedInteract) {
            // Check if this interactable is within range
            if(distance == Vector3.Distance(selectedInteract.transform.position, transform.position)) {
                return;
            }
            // Check if this interactable is valid
            if(!selectedInteract.GetComponent<Interactable>().isTargetable()) {
                selectedInteract = null;
            }
        }
        distance = float.MaxValue;

        // -- Loop through all registerd interactables
        foreach(Interactable it in interactables) {
            // Skip if not targetable
            if(!it.isTargetable()) {
                continue;
            }
            // store the distance to the target
            float dist = Vector3.Distance(it.transform.position, transform.position);
            if (
                dist < InteractDistance // Check if the distance is within range
                && 
                dist < distance // Check if its the closest
            ) {
                // update the values
                selectedInteract = it.gameObject;
                distance = dist;
            }
        }

        // Failsafe so if there are no interactables, its garunteed to be null
        if(distance == float.MaxValue) {
            selectedInteract = null;
        }
    }
}
