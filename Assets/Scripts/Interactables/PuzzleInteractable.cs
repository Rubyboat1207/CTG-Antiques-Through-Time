using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[RequireComponent(typeof(AudioSource))]
// TODO: Switch to toggleable for when the completion is done
public class PuzzleInteractable : ToggleableInteractable
{
    [Header("Base Puzzle Interactable")]
    public Transform LookPos;
    Vector3 OldOffset;
    public Vector3 Offset;
    public bool focused;
    public bool haveNotes = true;
    [SerializeField] bool hadFollow = true;
    Vector3 prevCamPos;
    public UnityEvent<bool, GameObject> OnPuzzleExit = new UnityEvent<bool, GameObject>();
    public UnityEvent OnPuzzleCorrect = new UnityEvent();
    public UnityEvent OnPuzzleOpen = new UnityEvent();

    public override void Interact()
    {
        base.Interact();
        isInteractable = focused;
        focused = !focused;
        if (focused)
        {
            OnOpenPuzzle();
        }
        else
        {
            ClosePuzzle();
        }
        if(haveNotes)
        {
            GameObject.Find("Notebook").GetComponent<Animation>().Play("SlideIn");
        }
        //TODO: Disable Player Move
    }

    public virtual void ClosePuzzle()
    {
        PlayerMove.Instance.GetComponent<Renderer>().enabled = true;
        isInteractable = true;
        focused = false;
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = PlayerMove.Instance.transform;
        if(hadFollow)
        {
            cam.Follow = PlayerMove.Instance.transform;
        }
        else
        {
            cam.Follow = null;
            cam.transform.position = prevCamPos;
        }
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = OldOffset;
        PlayerMove.Instance.canMove = true; //This can cause issues if multiple components are controlling movement
                                            // I'll be back here in a week when an unfindable bug appears due to this change
        if (haveNotes)
        {
            GameObject.Find("Notebook").GetComponent<Animation>().Play("SlideOut");
        }
    }

    public virtual void OnOpenPuzzle()
    {
        PlayerMove.Instance.GetComponent<Renderer>().enabled = false;
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        OldOffset = cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        if(cam.Follow == null)
        {
            hadFollow = false;
        }else if(!cam.Follow.GetComponent<PlayerMove>())
        {
            hadFollow = false;
        }else
        {
            hadFollow = true;
        }
        if(!hadFollow)
        {
            prevCamPos = cam.transform.position;
        }
        ReorientCamera(cam);
        OnPuzzleOpen.Invoke();
    }

    public void ReorientCamera(CinemachineVirtualCamera cam)
    {
        cam.LookAt = LookPos;
        cam.Follow = LookPos;
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Offset;
        PlayerMove.Instance.canMove = false;
    }

    public virtual void OnPuzzleComplete() {
        OnPuzzleExit.Invoke(true, gameObject);
        OnPuzzleCorrect.Invoke();
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/p_correct");
        GetComponent<AudioSource>().Play();
    }

    public new void Update()
    {
        base.Update();
        if(focused)
        {
            WhilePuzzleOpen();
        }
    }

    public virtual void WhilePuzzleOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
        }
    }

    public bool isComplete()
    {
        return false;
    }
}
