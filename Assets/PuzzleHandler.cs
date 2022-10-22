using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : Interactable
{
    [SerializeField]
    float cumpleation;
    public void SetPuzzleCompletionPercentage(float percent)
    {
        cumpleation = percent;
    }

    public void addCompletionPercentage(float percent)
    {
        cumpleation += percent;
    }

    void Update()
    {
        if (cumpleation >= 100)
        {
            Destroy(gameObject);
        }
    }
}
