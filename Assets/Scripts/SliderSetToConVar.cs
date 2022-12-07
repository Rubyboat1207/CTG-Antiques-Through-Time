using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSetToConVar : MonoBehaviour
{
    [SerializeField] string conVar;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = RTConsole.Singleton.GetConVar<float>(conVar).value;
    }
}
