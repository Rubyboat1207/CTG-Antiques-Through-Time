using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshotter : MonoBehaviour
{
    static string ss_path;

    // Start is called before the first frame update
    void Start()
    {
        ss_path = Application.persistentDataPath + "/screenshots";
        if (!Directory.Exists(ss_path))
        {
            Directory.CreateDirectory(ss_path);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
            ScreenCapture.CaptureScreenshot(ss_path + "/" + Directory.GetFiles(ss_path).Length + ".png");
    }

    public static void SS()
    {
        ScreenCapture.CaptureScreenshot(ss_path + "/" + Directory.GetFiles(ss_path).Length + ".png");
    }
}
