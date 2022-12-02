using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadInteractable : Interactable
{
    public int map = 0;
    public override void Interact()
    {
        base.Interact();
        RTConsole.Singleton.GetConVar<int>("mapid").value = map;
    }
}
