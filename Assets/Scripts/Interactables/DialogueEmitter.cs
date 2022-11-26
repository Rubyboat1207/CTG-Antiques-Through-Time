using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueEmitter : Interactable
{
    [Header("Dialouge Emmiter")]
    bool interacted = false;
    public int progress;
    public UnityEvent onDialogueBegin = new UnityEvent();
    public UnityEvent onDialogueComplete = new UnityEvent();
    public int lastGoto = -1;
    [System.Serializable]
    public class DialogeController : DialogueManager.Dialogue
    {
        public bool continueToNext = true;
    }
    [SerializeField] DialogeController[] dialogue;


    public new void Start()
    {
        base.Start();
        onDialogueComplete.AddListener(() =>
        {
            if (progress == dialogue.Length && lastGoto != -1)
            {
                progress = lastGoto;
            }
        });
    }

    public override void Interact()
    {
        base.Interact();
        interacted = true;
        DialogueManager.RenderDialogue(dialogue[progress]);
        onDialogueBegin.Invoke();
    }

    public new void Update()
    {
        base.Update();
        progress = Mathf.Clamp(progress, 0, dialogue.Length - 1);
        if(!DialogueManager.showing && interacted)
        {
            if(progress < dialogue.Length)
            {
                
                if (dialogue[progress].continueToNext && (progress != dialogue.Length - 1))
                {
                    if(progress + 1 <= dialogue.Length)
                    {
                        progress++;
                    }
                    DialogueManager.RenderDialogue(dialogue[progress]);
                }
                else
                {
                    progress++;
                    interacted = false;
                    onDialogueComplete.Invoke();
                }
            }
            else
            {
                interacted = false;
                onDialogueComplete.Invoke();
                
            }
        }
    }

    public override bool isTargetable()
    {
        return !interacted;
    }
}
