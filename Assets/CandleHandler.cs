using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleHandler : MonoBehaviour
{
    [SerializeField] Color correctColor;
    public void SetCorrect()
    {
        GetComponentInChildren<Light>().color = correctColor;
    }
}
