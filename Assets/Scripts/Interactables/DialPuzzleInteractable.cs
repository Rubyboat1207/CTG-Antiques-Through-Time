using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialPuzzleInteractable : PuzzleInteractable
{
    [SerializeField] List<List<string>> DialPositions;
    [SerializeField] List<string> DialValues;
    [SerializeField] DialButtons[] dials;
    [System.Serializable]
    struct DialButtons
    {
        public Button up;
        public Button down;
    }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        int index = 0;
        foreach(DialButtons dbs in dials)
        {
            dbs.down.onClick.AddListener(() => IncrementDialPosition(index, -1));
            dbs.up.onClick.AddListener(() => IncrementDialPosition(index, -1));
            index++;
        }
    }

    void IncrementDialPosition(int dial, int offset)
    {
        DialValues[dial] = DialPositions[dial][
            DialPositions[dial].IndexOf(
                DialValues[dial]
            )
            + offset
        ];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
