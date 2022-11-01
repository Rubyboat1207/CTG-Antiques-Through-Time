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
        
        
        dynamic interpreted = ConUtils.InterpretFromString(val, ConVar.Type);
        

        /*float Floatval;
        try {
            if(args.Length > 2 || !float.TryParse(args[1], out Floatval)) {
                List<string> strList = new List<string>(args);
                strList.RemoveAt(0);
                var val = string.Join(" ", strList);
                val = val.Replace("\"", "");
                TypedConVar<string>.setConVarValue(args[0], val);
                ConOut.Singleton.write(args[0] + " was set to " + ((TypedConVar<string>) RTConsole.Singleton.GetConVar(args[0])).value);
            }
            else {
                TypedConVar<float>.setConVarValue(args[0], Floatval);
                ConOut.Singleton.write(args[0] + " was set to " + ((TypedConVar<float>) RTConsole.Singleton.GetConVar(args[0])).value.ToString());
            }
        }catch {
            ConOut.Singleton.write(args[0] + " was invalid");
        }*/
        
    }

    void runConFunc(string input) {
        try {
            if(RTConsole.Singleton.ConFuncs.ContainsKey(input)) {
                RTConsole.Singleton.ConFuncs[input].Invoke(input);
            }else {
                ConOut.Singleton.write("function " + input + " was not found");
            }
        }catch {
            ConOut.Singleton.write("Error occured while running function");
        }
        
    }
}
