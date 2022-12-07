using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConInp : MonoBehaviour
{
    [SerializeField] Button EnterButton;
    [SerializeField] TMP_InputField Input;

    private void Start() {
        EnterButton.onClick.AddListener(() => {
            SendCommand(Input.text);
        });
    }

    public void SendCommand(string input) {
        if(input.Contains(';')) {
            var commands = input.Split(';');
            foreach(string command in commands) {
                if(command.Split(' ').Length > 1) {
                    setConVar(command.Trim());
                }else{
                    runConFunc(command);
                }
                
            }
        }else{
            if(input.Split(' ').Length > 1) {
                setConVar(input.Trim());
            }else{
                runConFunc(input.Trim());
            }
        }
    }

    public void ClearInput() {
        Input.text = "";
    }

    void setConVar(string input) {
        var args = input.Split(' ');
        ConVar ConVar = RTConsole.Singleton.GetConVar(args[0]);
        List<string> strList = new List<string>(args);
        strList.RemoveAt(0);
        var val = string.Join(" ", strList);

        ConUtils.InterpretFromString(val, ConVar.Type, ConVar);
    }

    void runConFunc(string input) {
        try {
            if(PersistantDataHolder.Instance.ConFuncs.ContainsKey(input.Split(' ')[0])) {
                PersistantDataHolder.Instance.ConFuncs[input].Invoke(input);
            }else {
                ConOut.Singleton.write("function " + input + " was not found");
            }
        }catch {
            ConOut.Singleton.write("Error occured while running function");
        }
        
    }
}
