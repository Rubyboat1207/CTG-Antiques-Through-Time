using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ATTConsole : MonoBehaviour
{
    static bool registered = false;
    static ATTConsole Instance;
    bool visible = false;
    public bool noclip = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        setVisibility(visible);
        if(!registered)
        {
            TypedConVar<int>.RegisterConVar("pl_model", 0);
            TypedConVar<bool>.RegisterConVar("sv_cheats", false);
            PersistantDataHolder.Instance.ConFuncs.Add("noclip", (name) => {
                if (RTConsole.Singleton)
                    noclip = !noclip;
                PlayerMove.Instance.SimplePlayerMove = ATTConsole.Instance.noclip;
            });
            TypedConVar<int>.RegisterConVar("mapid", SceneManager.GetActiveScene().buildIndex);
            registered = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde)) {
            visible = !visible;
            print(visible);
            setVisibility(visible);
        }
        if(SceneManager.GetActiveScene().buildIndex != RTConsole.Singleton.GetConVar<int>("mapid").Value) {
            SceneManager.LoadScene(RTConsole.Singleton.GetConVar<int>("mapid").Value);
        }
    }


    void setVisibility(bool visibility) {
        GetComponent<Image>().enabled = visibility;
        foreach(Image image in transform.GetComponentsInChildren<Image>()) {
            image.enabled = visibility;
        }
        foreach(TextMeshProUGUI tmpro in transform.GetComponentsInChildren<TextMeshProUGUI>()) {
            tmpro.enabled = visibility;
            tmpro.color = visibility ? new Color(1,1,1,1) : new Color(0,0,0,0);
        }
        foreach (TMP_InputField input in transform.GetComponentsInChildren<TMP_InputField>())
        {
            input.enabled = visibility;
        }
        foreach (RTConsoleInput input in transform.GetComponentsInChildren<RTConsoleInput>())
        {
            input.enabled = visibility;
        }
        transform.position += new Vector3(0, visibility ? -10000 : 10000);
    }
}
