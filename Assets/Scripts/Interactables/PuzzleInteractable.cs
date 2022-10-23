using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Switch to toggleable for when the completion is done
public class PuzzleInteractable : Interactable
{
    public GameObject puzzle;

    public override void Interact()
    {
        base.Interact();
        GameObject.Instantiate(puzzle);
    }
}
