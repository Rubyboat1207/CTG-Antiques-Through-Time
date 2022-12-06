using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SceneLoadWhenVideoDone : MonoBehaviour
{
    [SerializeField] int scene = 1;

    // Update is called once per frame
    void Update()
    {
        VideoPlayer player = GetComponent<VideoPlayer>();
        if (!player.isPlaying && player.frame > 0)
        {
            RTConsole.Singleton.GetConVar<int>("mapid").value = scene;
        }
    }
}
