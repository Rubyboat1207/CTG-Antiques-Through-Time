using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DialPuzzleInteractable : PuzzleInteractable
{
    [Header("Dial Puzzle Interactable")]
    public string acceptableValues = "abcdefghijklmnopqrstuvwxyz123456789";
    int selectedIndex;
    [SerializeField] List<Dial> dials;

    [SerializeField] Light CorrectLight;

    [SerializeField] Color CorrectColor;
    [SerializeField] Color IncorrectColor;

    [SerializeField] Color defaultColor;
    [SerializeField] Color SelectedColor;

    [System.Serializable]
    class Dial
    {
        public TextMeshProUGUI text;
        public int pos;
        public List<char> entries;
        public int entryCount;
        public char correctEntry;
        public char currentEntry;
        public GameObject go;
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        for (int i = 0; i < dials.Count; i++)
        {
            var dbs = dials[i];
            dbs.pos = 0;
            dbs.entries = new List<char>();
            for(int randomEntry = 0; randomEntry < dbs.entryCount; randomEntry++) {
                char characterToInclude = ' ';
                while(dbs.entries.Contains(characterToInclude) || characterToInclude == dbs.correctEntry || !acceptableValues.Contains(characterToInclude)) {
                    characterToInclude = acceptableValues[Random.Range(0, acceptableValues.Length)];
                }
                dbs.entries.Add(characterToInclude); 
            }
            dbs.entries[Random.Range(0, dbs.entryCount)] = dbs.correctEntry;
            dbs.currentEntry = dbs.entries[Random.Range(0, dbs.entryCount)];
            dbs.text.text = dbs.currentEntry.ToString();
        }
    }

    public override void OnClosePuzzle()
    {
        base.OnClosePuzzle();
        if(correctDials())
        {
            isInteractable = false;
            CorrectLight.color = CorrectColor;
        }
        else
        {
            CorrectLight.color = IncorrectColor;
        }
        foreach(Dial dial in dials) {
            dial.text.color = defaultColor;
        }
        OnPuzzleExit.Invoke(correctDials());
    }

    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        int index = 0;
        foreach(Dial dial in dials) {
            dial.text.color = index == selectedIndex ? SelectedColor : defaultColor;
            index++;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex += 1;
        }
        if (selectedIndex < 0)
        {
            selectedIndex = dials.Count - 1;
        }
        if (selectedIndex >= dials.Count)
        {
            selectedIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            IncrementDialPosition(selectedIndex, 1);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncrementDialPosition(selectedIndex, -1);
        }

    }

    void IncrementDialPosition(int dialIndex, int offset)
    {
        var dial = dials[dialIndex];
        dial.pos += offset;
        if(dial.pos >= dial.entryCount) {
            dial.pos = 0;
        }
        else if(dial.pos < 0) {
            dial.pos = dial.entryCount - 1;
        }
        dial.currentEntry = dial.entries[dial.pos];
        dial.text.text = dial.currentEntry + "";
    }



    public bool correctDials()
    {
        foreach(var dial in dials)
        {
            if(dial.currentEntry != dial.correctEntry)
            {
                return false;
            }
        }
        return true;
    }
}
