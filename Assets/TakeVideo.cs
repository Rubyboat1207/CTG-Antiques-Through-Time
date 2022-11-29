using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeVideo : MonoBehaviour
{
    float fullTimer = 0;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 / 60)
        {
            Screenshotter.SS();
            timer = 0;
        }
    }
}
