using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachineInteractable : Interactable
{
    public override void Interact()
    {
        base.Interact();
        RTConsole.Singleton.GetConVar<int>("mapid").value = 8;
    }
}
