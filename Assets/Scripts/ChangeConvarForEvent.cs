using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeConvarForEvent : MonoBehaviour
{
    public string convar_name;

    public void SetConVar(bool value)
    {
        ConVar.Get<bool>(convar_name).value = value;
    }

    private void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Toggle toggle = GetComponent<Toggle>();
        if (toggle)
        {
            toggle.isOn = ConVar.Get<bool>(convar_name).value;
        }
    }
}
