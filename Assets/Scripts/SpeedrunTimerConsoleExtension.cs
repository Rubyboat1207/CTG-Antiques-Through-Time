using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunTimerConsoleExtension : MonoBehaviour
{
    string speedrunDir;
    bool hasWrittentime = false;
    // Start is called before the first frame update
    void Start()
    {
        speedrunDir = Application.persistentDataPath + "/times";
        if (!RTConsole.Singleton.IsRegistered<float>("speedrunTimer"))
        {
            Startup();
        }
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if (!RTConsole.Singleton.IsRegistered<float>("speedrunTimer"))
        {
            Startup();
        }
        if(!ConVar.Get<bool>("speedrunTimer").value)
        {
            return;
        }

        int mapid = ConVar.Get<int>("mapid").value;
        if (mapid > 1 && mapid < 9)
        {
            ConVar.Get<float>("sp_time").value += Time.deltaTime;
        }
        else if (mapid == 9)
        {
            if (hasWrittentime)
            {
                return;
            }
            if (!File.Exists(speedrunDir))
            {
                File.Create(speedrunDir);
            }
            float time;
            if (!float.TryParse(File.ReadAllText(speedrunDir), out time))
            {
                File.WriteAllText(speedrunDir, ConVar.Get<float>("sp_time").value.ToString());
                hasWrittentime = true;
            }
            else
            {
                if (time > ConVar.Get<float>("sp_time").value)
                {
                    File.WriteAllText(speedrunDir, ConVar.Get<float>("sp_time").value.ToString());
                }
            }
        }
        else
        {
            ConVar.Get<float>("sp_time").value = 0;
        }
    }

    void OnSceneChanged(Scene current, Scene next)
    {

    }

    void Startup()
    {
        TypedConVar<bool>.RegisterConVar("speedrunTimer", false);
        TypedConVar<float>.RegisterConVar("sp_time", 0.0f);
        //TypedConVar<bool>.RegisterConVar("IndividualLevelTimer", false);
    }

    string toMinSec(float time)
    {
        return Mathf.Floor(time / 60) + ":" + (time % 60);
    }
}
