using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class LibraryDoor : MonoBehaviour
{
    [SerializeField] string OpenAnimationName;
    Dictionary<GameObject, bool> openCommands = new Dictionary<GameObject, bool>();
    public void SetOpenStatus(bool status, GameObject go)
    {
        if(openCommands.ContainsKey(go))
        {
            openCommands[go] = status;
        }
        if(canOpen())
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        GetComponent<Animation>().Play(OpenAnimationName);
    }

    public bool canOpen()
    {
        foreach(var open in openCommands)
        {
            if(!open.Value)
            {
                return false;
            }
        }
        return true;
    }
}
