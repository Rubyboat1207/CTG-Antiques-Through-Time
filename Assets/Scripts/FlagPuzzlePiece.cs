using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPuzzlePiece : MonoBehaviour
{
    Vector3 originalPos;

    public void Awake()
    {
        originalPos = transform.position;
    }

    public bool isInOriginalPlace()
    {
        return (originalPos - transform.position).magnitude < 0.2;
    }
}
