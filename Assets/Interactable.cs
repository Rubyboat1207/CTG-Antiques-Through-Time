using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Check a radius, and if the player presses a button, the interaction occurs
    // Get the player, then get the distance, then if the distance is lower than maximum, allow the interaction
    // trigger component, then if the player steps inside the 
    //    trigger you set a flag to be true, and if the flag is true, you can interact

    float interactRadius = 5;
    public int Solution = 1;

    // Update is called once per frame
    void Update()
    {
        if(Solution == 1)
        {
            // Solution 1
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] objects = Physics.OverlapSphere(transform.position, interactRadius);

                foreach (Collider obj in objects)
                {
                    if (obj.GetComponent<PlayerMove>() != null)
                    {
                        //Do interaction
                        Interact();
                        return;
                    }
                }
            }
        }
        if(Solution == 2)
        {
            // Solution 2
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Mathf.Abs((PlayerMove.Instance.transform.position - transform.position).magnitude) < interactRadius)
                {
                    Interact();
                }
            }
        }
    }

    public virtual void Interact()
    {
        print("Interacted with: " + gameObject.name); 
    }
}
