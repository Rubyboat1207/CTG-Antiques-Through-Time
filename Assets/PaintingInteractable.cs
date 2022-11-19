using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingInteractable : PuzzleInteractable
{
    [SerializeField] Vector3 origin = Vector3.zero;
    [SerializeField] Vector3 scale = Vector3.one;
    [SerializeField] float speed;
    [SerializeField] Vector3 zoomSpeed;
    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        LookPos.localPosition += moveVector;
        Vector3 lp = LookPos.localPosition;
        lp.x = Mathf.Clamp(lp.x, origin.x - (scale.x / 2), origin.x + (scale.x / 2));
        lp.y = Mathf.Clamp(lp.y, origin.y - (scale.y / 2), origin.y + (scale.y / 2));

        LookPos.localPosition = lp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + origin, scale);
    }
}
