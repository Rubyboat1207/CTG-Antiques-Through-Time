using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LibSM64;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager Singleton;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI emitterText;
    [SerializeField] Image image;

    public static bool showing;

    [System.Serializable]
    public class Dialogue
    {
        public string emitterName;
        [TextArea(5, 10)]
        public string text;
        public Sprite image;
    }

    public void Start()
    {
        Singleton = this;
    }

    void Update()
    {
        if(showing)
        {
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
            {
                showing = false;
                GetComponent<Animation>().Play("Hide");
                if(GameObject.Find("MARIO"))
                {
                    PlayerMove.Instance.transform.parent.GetComponent<ExampleInputProvider>().paused = false;
                }
                else
                {
                    PlayerMove.Instance.canMove = true;
                }
            }
        }
    }

    public static void RenderDialogue(Dialogue dialogue)
    {
        Singleton.RenderDialogue(dialogue, true);
    }

    public void RenderDialogue(Dialogue dialogue, bool animate)
    {
        text.text = dialogue.text;
        emitterText.text = dialogue.emitterName;
        image.sprite = dialogue.image;
        if(animate)
        {
            GetComponent<Animation>().Play("Rise");
        }
        if(PlayerMove.Instance)
        {
            PlayerMove.Instance.canMove = false;
            PlayerMove.Instance.transform.parent.GetComponent< ExampleInputProvider >().paused = true;
        }
        showing = true;
    }
}
