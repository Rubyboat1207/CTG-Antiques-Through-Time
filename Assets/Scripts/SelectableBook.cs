using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBook : MonoBehaviour
{
    public BookPuzzleInteractable puzzle;

    public Vector3 SelectedAngle;
    public Vector3 DefaultAngle;

    bool selected = false;
    public bool Selected { 
        get { return selected; } 
        set {
            transform.localRotation = Quaternion.Euler(value ? SelectedAngle : DefaultAngle);
            selected = value;
        }
    }
    public bool Correct = false;
    public int orderToBeSelected = 0;

    public void OnMouseDown()
    {
        if(puzzle.focused)
        {
            Selected = true;
            if (puzzle.progress == orderToBeSelected)
            {
                Correct = !Correct;
                puzzle.progress += 1;
            }
            if(!Selected && Correct && puzzle.progress != puzzle.progress - 1)
            {
                puzzle.ClearPuzzle();
            }
        }
    }

    public void Start()
    {
        DefaultAngle = transform.localRotation.eulerAngles;
    }

    public void ResetVariables()
    {
        Selected = false;
        Correct = false;
    }
}
