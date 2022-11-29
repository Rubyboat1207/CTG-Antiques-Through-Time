using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AztecStairs : MonoBehaviour
{
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

    }
}
