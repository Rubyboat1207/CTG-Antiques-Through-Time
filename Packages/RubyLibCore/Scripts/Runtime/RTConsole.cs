using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

[RequireComponent(typeof(ConOut))]
[RequireComponent(typeof(ConInp))]
public class RTConsole : MonoBehaviour
{
    KeyCode consoleAppear = KeyCode.Tilde;
    bool visible = false;

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
    private void Awake()
    {
        Singleton = this;
        if(PersistantDataHolder.Instance == null)
        {
            var pdh = new GameObject();
            pdh.AddComponent<PersistantDataHolder>().Initalize();
            pdh.name = "PersistantDataHolder";
            DontDestroyOnLoad(pdh);
        }
    }
    
    public TypedConVar<T> GetConVar<T>(string name)
    {
        if(PersistantDataHolder.Instance.ConVars.ContainsKey(name)) {
            if(PersistantDataHolder.Instance.ConVars[name].Type == typeof(T)) {
                return (TypedConVar<T>)PersistantDataHolder.Instance.ConVars[name];
            }else{
                throw new ArgumentException($"ConVar \"{name}\" was not of type specified \"{typeof(T).Name}\"");
            }
        }else{
            throw new ArgumentException($"ConVar \"{name}\" is not registered");
        }
    }



    public bool IsRegistered<T>(string name)
    {
        if (PersistantDataHolder.Instance.ConVars.ContainsKey(name))
        {
            if (PersistantDataHolder.Instance.ConVars[name].Type == typeof(T))
            {
                return true;
            }
        }
        return false;
    }
    public bool IsRegistered(string name)
    {
        if (PersistantDataHolder.Instance.ConVars.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    public ConVar GetConVar(string name)
    {
        if(PersistantDataHolder.Instance.ConVars.ContainsKey(name)) {
            return PersistantDataHolder.Instance.ConVars[name];
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

    public static TypedConVar<T> Get<T>(string name)
    {
        return RTConsole.Singleton.GetConVar<T>(name);
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
        if(PersistantDataHolder.Instance.ConVars.ContainsKey(name)) {
            throw new ArgumentException(name + " is already a ConVar");
        }else{
            PersistantDataHolder.Instance.ConVars.Add(name, new TypedConVar<T>(name, defaultValue));
            PersistantDataHolder.Instance.ConFuncs.Add(name, (name) => {
                ConOut.Singleton.write(name + ": " + (RTConsole.Singleton.GetConVar<T>(name)).Value.ToString());
            });
        }
    }

    public static void setConVarValue(string name, T value) {
        if(PersistantDataHolder.Instance.ConVars.ContainsKey(name)) {
            RTConsole.Singleton.GetConVar<T>(name).Value = value;
        }else{
            throw new ArgumentException(name + " was not found");
        }
        
    }

}