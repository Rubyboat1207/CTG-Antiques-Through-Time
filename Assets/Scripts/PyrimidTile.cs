using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PyrimidTile : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TMP_InputField input;
    public int value;
    public PyrimidTile[] AddendTiles;
    bool hasBeenValidated;

    private void LateUpdate()
    {
        hasBeenValidated = false;
    }

    public bool isValid()
    {
        print(text.text.Substring(0,3));
        print(value);
        int o;
        if(int.TryParse(text.text.Trim().Substring(0, 3), out o))
        {
            print(o);
            print(value == o);
            return value == o;
        }
        return false;
    }
}
