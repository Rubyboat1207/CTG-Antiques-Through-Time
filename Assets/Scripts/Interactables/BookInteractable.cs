using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Switch to toggleable for when the completion is done
[RequireComponent(typeof(AudioSource))]
public class BookInteractable : Interactable
{
    [Header("Book Interactable")]
    public BookManager.Page[] pages;
    public BookManager activeBM;
    public int page = 0;

    public override void Interact()
    {
        base.Interact();
        if(!activeBM) 
        {
            if (GameObject.Find("MARIO"))
            {
                PlayerMove.Instance.transform.parent.GetComponent<ExampleInputProvider>().paused = true;
            }
            else
            {
                PlayerMove.Instance.canMove = false;
            }
            activeBM = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BaseBook"), GameObject.Find("Canvas").transform).GetComponent<BookManager>();
            activeBM.pages = pages;
            activeBM.page = page;
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/pageFlip"));
        }
    }

    public new void Update() {
        base.Update();
        if(Input.GetKey(KeyCode.Escape)) {
            if(activeBM) {
                page = activeBM.page;
                Destroy(activeBM.gameObject);
                if (GameObject.Find("MARIO"))
                {
                    PlayerMove.Instance.transform.parent.GetComponent<ExampleInputProvider>().paused = false;
                }
                else
                {
                    PlayerMove.Instance.canMove = false;
                }
                activeBM = null;
            }
        }
    }
}
