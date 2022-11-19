using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CrossbowInteractable : PuzzleInteractable
{
    [SerializeField] int hits = 0;
    [SerializeField] float speed;
    [SerializeField] float localOffset;
    public override void WhilePuzzleOpen()
    {
        Vector3 moveVector = new Vector3(0,Input.GetAxis("Horizontal")) * Time.deltaTime * speed;
        transform.Rotate(moveVector);
        var _oo = Offset;
        Offset = localOffset * transform.forward;
        Offset.y = _oo.y;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var arrow = Instantiate(
                Resources.Load<GameObject>("Prefabs/ShotArrow"),
                LookPos.position,
                transform.rotation
            ).GetComponent<ShotArrow>();
        }
        CinemachineVirtualCamera cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        ReorientCamera(cam);
        base.WhilePuzzleOpen();
    }
}
