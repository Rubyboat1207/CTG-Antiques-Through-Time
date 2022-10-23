using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : Interactable
{
    [SerializeField]
    // Sorry collin, but i dont know if I want "cumpletion" to be the variable name
    float completionPercent;
    public void SetPuzzleCompletionPercentage(float percent)
    {
        completionPercent = percent;
    }

    public void addCompletionPercentage(float percent)
    {
        completionPercent += percent;
    }

    void Update()
    {
        base.Update();
        if (completionPercent >= 100)
        {
            Destroy(gameObject);
        }
    }
}
