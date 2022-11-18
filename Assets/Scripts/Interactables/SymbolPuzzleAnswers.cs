using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SymbolPuzzleAnswers : PuzzleInteractable
{
    [SerializeField] Sprite[] Symbols;
    int[] CorrectSymbolIndex;
    int selectedIndex = 0;
    [SerializeField] TMP_InputField[] Inputs;
    [SerializeField] List<int> CorrectInputs;
    [SerializeField] Image[] Images;

    public override void OnClosePuzzle()
    {
        base.OnClosePuzzle();
        if(allSymbolsCorrect())
        {
            OnPuzzleExit.Invoke(true, gameObject);
        }
    }

    public new void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex--;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex++;
        }
        if(selectedIndex < 0)
        {
            selectedIndex = Inputs.Length - 1;
        }
        if(selectedIndex > Inputs.Length - 1)
        {
            selectedIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Inputs[selectedIndex].text = (int.Parse(Inputs[selectedIndex].text) - 1).ToString();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Inputs[selectedIndex].text = (int.Parse(Inputs[selectedIndex].text) + 1).ToString();
        }
        for(int i = 0; i < Inputs.Length; i++)
        {
            ColorBlock block = Inputs[i].colors;
            block.normalColor = i == selectedIndex ? Color.grey : Color.white;
            Inputs[i].colors = block;
        }
    }

    bool allSymbolsCorrect()
    {
        int i = 0;
        foreach (var input in Inputs)
        {
            if (input.text != CorrectSymbolIndex[i].ToString())
            {
                return false;
            }
            i++;
        }
        return true;
    }

    public new void Start()
    {
        base.Start();
        CorrectInputs = new List<int>(new int[Images.Length]);
        for (int inp = 0; inp < CorrectInputs.Count; ++inp)
        {
            CorrectInputs[inp] = -1;
        }
        int i = 0;
        foreach (Image image in Images)
        {
            CorrectInputs[i] = Random.Range(0, Symbols.Length - 1);
            while(!CorrectInputs.Contains(i))
            {
                CorrectInputs[i] = Random.Range(0, Symbols.Length - 1);
            }
            image.sprite = Symbols[CorrectInputs[i]];
            i++;
        }
    }
}
