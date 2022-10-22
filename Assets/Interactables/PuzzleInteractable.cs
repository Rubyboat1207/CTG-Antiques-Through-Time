using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteractable : Interactable
{
    public GameObject puzzle;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        GameObject.Instantiate(puzzle);
    }
}
