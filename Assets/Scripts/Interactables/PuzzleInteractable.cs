using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// TODO: Switch to toggleable for when the completion is done
public class PuzzleInteractable : ToggleableInteractable
{
    public Transform LookPos;
    Vector3 OldOffset;
    public Vector3 Offset;
    public bool focused;

    public override void Interact()
    {
        base.Interact();
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
    }

    public virtual void OnOpenPuzzle()
    {
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = LookPos;
        cam.Follow = LookPos;
        OldOffset = cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Offset;
    }

    public virtual void OnPuzzleComplete() {
        
    }

    public bool isComplete()
    {
        return false;
    }
}
