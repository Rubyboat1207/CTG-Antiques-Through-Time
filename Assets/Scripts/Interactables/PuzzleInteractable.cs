using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Switch to toggleable for when the completion is done
public class PuzzleInteractable : ToggleableInteractable
{
    public GameObject puzzle;
    public Animation anim;

    public override void Interact()
    {
        base.Interact();
        GameObject.Instantiate(puzzle, GameObject.Find("Canvas").transform).GetComponent<PuzzleHandler>().interactable = this;
    }

    public virtual void OnPuzzleComplete() {
        isInteractable = false;
        anim.Play();
    }
}
