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
        if(selectedInteract) {
            if(distance == Vector3.Distance(selectedInteract.transform.position, transform.position)) {
                return;
            }
        }
        distance = float.MaxValue;
        foreach(Interactable it in interactables) {
            float dist = Vector3.Distance(it.transform.position, transform.position);
            if (dist < 5 && dist < distance) {
                selectedInteract = it.gameObject;
                distance = dist;
            }
        }
        if(distance == float.MaxValue) {
            selectedInteract = null;
        }
    }
}
