using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyrimidPuzzle : PuzzleInteractable
{
    [SerializeField] PyrimidTile TopTile;

    public override void ClosePuzzle()
    {
        base.ClosePuzzle();
        if(TopTile.isValid())
        {
            print("valid!");
            OnPuzzleComplete();
            isInteractable = false;
        }
    }
}
