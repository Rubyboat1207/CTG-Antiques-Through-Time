using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// TODO: Switch to toggleable for when the completion is done
public class PuzzleInteractable : ToggleableInteractable
{
    [Header("Base Puzzle Interactable")]
    public Transform LookPos;
    Vector3 OldOffset;
    public Vector3 Offset;
    public bool focused;

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
            OnClosePuzzle();
        }
        
        //TODO: Disable Player Move
    }

    public virtual void OnClosePuzzle()
    {
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = PlayerMove.Instance.transform;
        cam.Follow = PlayerMove.Instance.transform;
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = OldOffset;
        PlayerMove.Instance.canMove = true; //This can cause issues if multiple components are controlling movement
                                            // I'll be back here in a week when an unfindable bug appears due to this change
    }

    public virtual void OnOpenPuzzle()
    {
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = LookPos;
        cam.Follow = LookPos;
        OldOffset = cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Offset;
        PlayerMove.Instance.canMove = false;
    }

    public virtual void OnPuzzleComplete() {
        
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
            isInteractable = true;
            focused = false;
            OnClosePuzzle();
        }
    }

    public bool isComplete()
    {
        return false;
    }
}
