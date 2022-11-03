using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void LoadScene(int buildIndex) {
        RTConsole.Singleton.GetConVar<int>("mapid").Value = buildIndex;
    }
}
