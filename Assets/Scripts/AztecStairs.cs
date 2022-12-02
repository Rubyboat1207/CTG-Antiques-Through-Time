using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AztecStairs : MonoBehaviour
{
    public UnityEvent OnComplete = new UnityEvent();
    public bool a = false;
    public void TryMakeVisibleA()
    {
        a = true;
    }

    public bool b = false;
    public void TryMakeVisibleB()
    {
        b = true;
    }

    private void Update()
    {
        GetComponent<Collider>().enabled = a && b;
        GetComponent<Renderer>().enabled = a && b;
        if(a && b)
        {
            OnComplete.Invoke();
        }
    }
}
