using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveListeners : MonoBehaviour
{
    public void Remove()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
