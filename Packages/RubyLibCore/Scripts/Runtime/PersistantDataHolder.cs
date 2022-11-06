using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PersistantDataHolder : MonoBehaviour
{
    public static PersistantDataHolder Instance;

    public Dictionary<string, ConVar> ConVars = new Dictionary<string, ConVar>();
    public Dictionary<string, Action<string>> ConFuncs = new Dictionary<string, Action<string>>();

    public void Initalize()
    {
        Instance = this;
    }
}
