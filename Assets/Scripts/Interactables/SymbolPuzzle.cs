using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolPuzzle : PuzzleInteractable
{
    [SerializeField] float RotationAmmount = 32;
    [SerializeField] Transform SmallDial;
    Quaternion s_g;
    Quaternion b_g;
    [SerializeField] Transform LargeDial;
    [SerializeField] Transform[] StayUpright;


    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDials(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveDials(-1);
        }
    }

    public void MoveDial(Transform dial, int rotations)
    {
        dial.Rotate(new Vector3(0, rotations * RotationAmmount));
    }

    public void MoveDials(int rotations)
    {
        MoveDial(SmallDial, -rotations);
        MoveDial(LargeDial, rotations);
        foreach(var upright in StayUpright)
        {
            for(int i = 0; i < upright.childCount; i++)
            {
                upright.GetChild(i).rotation = Quaternion.Euler(upright.up);
            }
        }
    }
}
