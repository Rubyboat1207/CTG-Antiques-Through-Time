using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class AutoPopulateTMP : MonoBehaviour
{
    public string content = "";

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PopulateSpitefont.populateFont(content);
    }
}
