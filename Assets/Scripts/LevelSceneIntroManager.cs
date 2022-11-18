using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSceneIntroManager : MonoBehaviour
{
    public int ChapterIndex;
    public string ChapterName;
    public string ChapterNumberPrefix = "Chapter";

    [Header("Text Objects")]
    public TextMeshProUGUI chapter;
    public TextMeshProUGUI title;
    public TextMeshProUGUI character;
    // Start is called before the first frame update
    void Start()
    {
        chapter.text = ChapterNumberPrefix + " " + ChapterIndex;
        title.text = ChapterName;
        
        GetComponent<Animation>().Play("ChapterIntroAnimation");
    }

    private void Update()
    {
        if(!GetComponent<Animation>().isPlaying)
        {
            return;
        }
        int charid;
        try
        {
            charid = RTConsole.Singleton.GetConVar<int>("pl_model").value;
        }catch
        {
            return;
        }
        if (charid == 0)
        {
            character.text = "Cassie";
        }
        if (charid == 1)
        {
            character.text = "Alfred";
        }
        if (charid == 2)
        {
            character.text = "Brady";
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            GetComponent<Animation>().Play("EndAnimEarly");
        }
    }
}
