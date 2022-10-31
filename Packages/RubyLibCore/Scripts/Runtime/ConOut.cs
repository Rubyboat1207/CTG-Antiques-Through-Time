using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConOut : MonoBehaviour
{
    static ConOut singleton;
    public static ConOut Singleton {
        get {
            return singleton;
        }
        set {
            if(singleton) {
                Destroy(value);
                return;
            }
            singleton = value;
        }
    }

    private void Start() {
        Singleton = this;
    }
    [SerializeField] TextMeshProUGUI conOut;

    public void write(string output) {
        conOut.text = string.Concat(string.Concat(conOut.text, output), "\n");
    }
}
