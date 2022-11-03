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

    public Dictionary<string, ConVar> ConVars = new Dictionary<string, ConVar>();
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

    public TypedConVar<T> GetConVar<T>(string name)
    {
        if(ConVars.ContainsKey(name)) {
            if(ConVars[name].Type == typeof(T)) {
                return (TypedConVar<T>) ConVars[name];
            }else{
                throw new ArgumentException($"ConVar \"{name}\" was not of type specified \"{typeof(T).Name}\"");
            }
        }else{
            throw new ArgumentException($"ConVar \"{name}\" is not registered");
        }
    }

    public ConVar GetConVar(string name)
    {
        if(ConVars.ContainsKey(name)) {
            return ConVars[name];
        }else{
            throw new ArgumentException($"ConVar \"{name}\" is not registered");
        }
    }
}
public class ConVar {
    protected readonly Type type;

    protected ConVar(Type type) {
        this.type = type;
    }

    public Type Type {
        get {
            return type;
        }
    }
}

public class TypedConVar<T> : ConVar
{
    public string name;

    public T value;
    public T Value { get { return value; }
        set
        {
            this.value = value;
            ConOut.Singleton.write(name + " was set to: " + this.value);
        }
    }

    public TypedConVar(string name, T value) : base(typeof(T))
    {
        this.value = value;
        this.name = name;
    }

    public static void RegisterConVar(string name, T defaultValue)
    {
        if(RTConsole.Singleton.ConVars.ContainsKey(name)) {
            throw new ArgumentException(name + " is already a ConVar");
        }else{
            RTConsole.Singleton.ConVars.Add(name, new TypedConVar<T>(name, defaultValue));
            RTConsole.Singleton.ConFuncs.Add(name, (name) => {
                ConOut.Singleton.write(name + ": " + (RTConsole.Singleton.GetConVar<T>(name)).Value.ToString());
            });
        }
    }

    public static void setConVarValue(string name, T value) {
        if(RTConsole.Singleton.ConVars.ContainsKey(name)) {
            RTConsole.Singleton.GetConVar<T>(name).Value = value;
        }else{
            throw new ArgumentException(name + " was not found");
        }
        
    }
}