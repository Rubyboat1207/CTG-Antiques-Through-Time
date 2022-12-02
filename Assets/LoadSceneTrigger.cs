using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    public int mapid = 7;
    private void OnTriggerStay(Collider other)
    {
        RTConsole.Singleton.GetConVar<int>("mapid").value = mapid;
    }
}
