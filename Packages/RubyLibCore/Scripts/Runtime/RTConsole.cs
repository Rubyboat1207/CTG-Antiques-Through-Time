using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[RequireComponent(typeof(ConOut))]
[RequireComponent(typeof(ConInp))]
public class RTConsole : MonoBehaviour
{
    KeyCode consoleAppear = KeyCode.Tilde;
    bool visible = false;

    public Dictionary<string, TypedConVar<string>> StringConVars = new Dictionary<string, TypedConVar<string>>();
    public Dictionary<string, TypedConVar<float>> FloatConVars = new Dictionary<string, TypedConVar<float>>();

    public Dictionary<string, Action<string>> ConFuncs = new Dictionary<string, Action<string>>();

    static RTConsole singleton;
    public static RTConsole Singleton {
        get {
            return singleton;
        }

        set {
            if(singleton) {
                Destroy(value.gameObject);
            }else{
                singleton = value;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(consoleAppear))
        {
            visible = !visible;
            foreach(Transform child in transform)
            {
                if (child.GetComponent<Renderer>())
                    continue;
                child.GetComponent<Renderer>().enabled = visible;
            }
        }
    }
    bool noclip = false;
    private void Awake()
    {
        Singleton = this;
    }

    public ConVar GetConVar(string name)
    {
        if(StringConVars.ContainsKey(name)) {
            return StringConVars[name];
        }else if(FloatConVars.ContainsKey(name)){
            return FloatConVars[name];
        }else{
            throw new ArgumentException("Name for conVar was not registered in either StringConVars or FloatConVars.");
        }
    }
}
public interface ConVar {

}

public class TypedConVar<T> : ConVar
{
    public string name;
    public T value;

    public TypedConVar(string name, T value)
    {
        this.value = value;
        this.name = name;
    }

    public static void RegisterConVar(string name, T defaultValue)
    {
        if(typeof(T) == typeof(string)) {
            if(RTConsole.Singleton.StringConVars.ContainsKey(name)) {
                throw new ArgumentException(name + " is already a ConVar");
            }else{
                RTConsole.Singleton.StringConVars.Add(name, new TypedConVar<string>(name, Convert.ToString(defaultValue)));
                RTConsole.Singleton.ConFuncs.Add(name, (name) => {ConOut.Singleton.write(((TypedConVar<string>) RTConsole.Singleton.GetConVar(name)).value);});
            }
        }else if(typeof(T) == typeof(float)) {
            if(RTConsole.Singleton.FloatConVars.ContainsKey(name)) {
                throw new ArgumentException(name + " is already a ConVar");
            }else{
                RTConsole.Singleton.FloatConVars.Add(name, new TypedConVar<float>(name, (float) Convert.ChangeType(defaultValue, typeof(float))));
                RTConsole.Singleton.ConFuncs.Add(name, (name) => {ConOut.Singleton.write(((TypedConVar<float>) RTConsole.Singleton.GetConVar(name)).value.ToString());});
            }
        }else {
            throw new ArgumentException("ConVar type to set was not float or string.");
        }

        
    }

    public static void setConVarValue(string name, T value) {
        if(typeof(T) == typeof(string)) {
            if(RTConsole.Singleton.StringConVars.ContainsKey(name)) {
                RTConsole.Singleton.StringConVars[name].value = Convert.ToString(value);
            }else{
                throw new ArgumentException(name + " was not found");
            }
        }else if(typeof(T) == typeof(float)) {
            if(RTConsole.Singleton.FloatConVars.ContainsKey(name)) {
                RTConsole.Singleton.FloatConVars[name].value = (float) Convert.ChangeType(value, typeof(float));
            }else{
                throw new ArgumentException(name + " was not found");
            }
        }else {
            throw new ArgumentException("ConVar type to set was not float or string.");
        }
    }
}