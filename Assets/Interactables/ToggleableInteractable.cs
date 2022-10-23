using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleableInteractable : Interactable
{
    [Header("Toggleable Interactable")]
    public bool isInteractable = true;
    
    
    // When using this method, place it at the end of the overriten method
    public override void Interact()
    {
        // Run appropriate methods depending on state
        if(isInteractable) {
           base.Interact();
           OnSuccessfulInteract();
        }else{
            OnFailedInteract();
        }
    }

    public override bool isTargetable() {
        // Only allow targeting if it can be interacted with
        return isInteractable;
    }

    public virtual void OnSuccessfulInteract() {
        //Blank Method, as there shouldnt be a default action
    }

    public virtual void OnFailedInteract() {
        //Blank Method, as there shouldnt be a default action
    }
}
