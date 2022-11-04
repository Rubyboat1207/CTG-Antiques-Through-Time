using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzleInteractable : PuzzleInteractable
{
    public int progress;
    public List<SelectableBook> Books = new List<SelectableBook>();

    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        if(allBooksCorrect())
        {
            OnPuzzleComplete();
        }
    }

    public override void OnClosePuzzle()
    {
        base.OnClosePuzzle();
        if(allBooksCorrect())
        {
            isInteractable = false;
        }
        else
        {
            ClearPuzzle();
        }
    }

    public override void OnPuzzleComplete()
    {
        base.OnPuzzleComplete();
        OnClosePuzzle();
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
