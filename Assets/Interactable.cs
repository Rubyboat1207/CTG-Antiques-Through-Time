using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // 1. Check a radius, and if the player presses a button, the interaction occurs
    // 2. Get the player, then get the distance, then if the distance is lower than maximum, allow the interaction
    // 3. trigger component, then if the player steps inside the 
    //    trigger you set a flag to be true, and if the flag is true, you can interact

    float interactRadius = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Mathf.Abs((PlayerMove.Instance.transform.position - transform.position).magnitude) < interactRadius)
            {
                Interact();
            }
        }
    }

    public virtual void Interact()
    {
        print("Interacted with: " + gameObject.name); 
    }
}
