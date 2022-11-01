using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ATTConsole : MonoBehaviour
{
    static ATTConsole Instance;
    bool visible = false;
    public bool noclip = false;

    void Awake() {
        DontDestroyOnLoad(transform.root.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        setVisibility(visible);
        TypedConVar<float>.RegisterConVar("pl_model", 0);
        RTConsole.Singleton.ConFuncs.Add("noclip", (name) => {
            noclip = !noclip;
            PlayerMove.Instance.SimplePlayerMove = ATTConsole.Instance.noclip;
        });
        TypedConVar<float>.RegisterConVar("mapid", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde)) {
            visible = !visible;
            print(visible);
            setVisibility(visible);
        }
        if(SceneManager.GetActiveScene().buildIndex != RTConsole.Singleton.GetConVar<float>("mapid").value) {
            SceneManager.LoadScene(RTConsole.Singleton.GetConVar<int>("mapid").value);
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
        foreach(TMP_InputField input in transform.GetComponentsInChildren<TMP_InputField>()) {
            input.enabled = visibility;
        }
    }
}
