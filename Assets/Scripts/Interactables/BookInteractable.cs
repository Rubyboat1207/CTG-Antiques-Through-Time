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
            PlayerMove.Instance.canMove = false;
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
                PlayerMove.Instance.canMove = true;
                activeBM = null;
            }
        }
    }
}
