using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class RTConsoleInput : MonoBehaviour
{
    public TextMeshProUGUI placeholder;
    public TextMeshProUGUI visText;
    public string text = "";
    public Vector2Int SelectedArea = new Vector2Int(0,0);
    public UnityEvent<string> Submitted = new UnityEvent<string>();
    public int entryCount;
    public List<string> prevEntries = new List<string>();
    bool skipframe = false;
    public bool arrows = true;
    public List<char> allowedChars = new List<char>(" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=<>?,./[]{}\\".ToCharArray());

    private void Start()
    {
        prevEntries.Add("");
    }

    private void OnEnable()
    {
        skipframe = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(skipframe)
        {
            skipframe = false;
            return;
        }
        string inputString = SanitizeInput(Input.inputString.Replace(Environment.NewLine, ""));

        if (SelectedArea.x != SelectedArea.y)
        {
            if (inputString.Length > 0)
            {
                string p1 = (text.Substring(0, Mathf.Min(SelectedArea.x, SelectedArea.y)) + inputString);
                text = p1 + text.Substring(0, Mathf.Max(SelectedArea.x, SelectedArea.y));
                SelectedArea.x = p1.Length;
                SelectedArea.y = SelectedArea.x;
                return;
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SelectedArea.x = SelectedArea.y;
            }
            //TODO Implement
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                SelectedArea.x = text.Length;
                SelectedArea.y = 0;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                string copytext = text.Substring(Mathf.Min(SelectedArea.x, SelectedArea.y), Mathf.Max(SelectedArea.x, SelectedArea.y));
                GUIUtility.systemCopyBuffer = copytext;
            }

        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (text.Length > 0)
            {
                DeleteSelection();
            }
        }
        text = text.Insert(SelectedArea.x, inputString);
        SelectedArea.x += inputString.Length;
        SelectedArea.y += inputString.Length;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectedArea.y--;
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                SelectedArea.x--;
                SelectedArea.x = SelectedArea.y;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectedArea.y++;
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                SelectedArea.x++;
                SelectedArea.y = SelectedArea.x;
            }
        }

        //Should Occur at the end
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Submitted.Invoke(text);
            prevEntries.Add(text);
            entryCount = prevEntries.Count;
        }
        if(arrows)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                entryCount--;
                entryCount = Mathf.Clamp(entryCount, 0, prevEntries.Count - 1);
                text = prevEntries[entryCount];
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                entryCount++;
                entryCount = Mathf.Clamp(entryCount, 0, prevEntries.Count - 1);
                text = prevEntries[entryCount];
            }
        }
        
        SelectedArea.x = Mathf.Clamp(SelectedArea.x, 0, text.Length);
        SelectedArea.y = Mathf.Clamp(SelectedArea.y, 0, text.Length);
        placeholder.enabled = text.Length == 0;
        
        visText.text = text.Insert(Mathf.Min(SelectedArea.x, SelectedArea.y), "|").Insert(Mathf.Min(SelectedArea.x, SelectedArea.y) + 1, "<mark=#75ff7599>").Insert(Mathf.Max(SelectedArea.x, SelectedArea.y) + "<<mark=numbers>> ".Length, "</mark>");
    }

    public void DeleteSelection()
    {
        int dist = Mathf.Abs(SelectedArea.y - SelectedArea.x);
        if (dist == 0)
        {
            text = text.Remove(SelectedArea.x - 1, 1);
            SelectedArea.x--;
            SelectedArea.y--;
        }
        else
        {
            text = text.Remove(Mathf.Min(SelectedArea.x, SelectedArea.y), dist);
            SelectedArea.x = Mathf.Min(SelectedArea.x, SelectedArea.y);
            SelectedArea.y = SelectedArea.x;
        }
    }

    string SanitizeInput(string input)
    {
        char[] chars = input.ToCharArray();
        string output = "";
        foreach(char ch in chars)
        {
            if(ch == ' ')
            {
                output += " ";
            }

            if(allowedChars.Contains(ch))
                output += ch;
        }
        return output;
    }
}
