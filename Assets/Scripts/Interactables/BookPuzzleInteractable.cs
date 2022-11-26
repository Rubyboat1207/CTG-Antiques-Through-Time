using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzleInteractable : PuzzleInteractable
{
    public int progress;
    public List<SelectableBook> Books = new List<SelectableBook>();
    public Light CorrectLight;
    [SerializeField] Color CorrectColor;
    [SerializeField] Color IncorrectColor;

    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        if(allBooksCorrect())
        {
            OnPuzzleComplete();
        }
    }

    public override void ClosePuzzle()
    {
        base.ClosePuzzle();
        if(allBooksCorrect())
        {
            isInteractable = false;
        }
        else
        {
            ClearPuzzle();
        }

        if(allBooksCorrect())
        {
            isInteractable = false;
            CorrectLight.color = CorrectColor;
        }
        else
        {
            CorrectLight.color = IncorrectColor;
        }
    }

    public override void OnPuzzleComplete()
    {
        base.OnPuzzleComplete();
        ClosePuzzle();
    }

    public void ClearPuzzle()
    {
        foreach (SelectableBook book in Books)
        {
            book.ResetVariables();
        }
        progress = 0;
    }

    public bool allBooksCorrect()
    {
        foreach (SelectableBook book in Books)
        {
            if (!book.Correct)
            {
                return false;
            }
        }
        return true;
    }
}
