using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class FlagPuzzle : PuzzleInteractable
{
    [SerializeField] int selectedPiece = -1;
    [SerializeField] LayerMask ignoreRaycast;
    [SerializeField] AudioClip clip;

    public new void Start()
    {
        base.Start();
        List<int> scrambled = new List<int>();
        int i = 0;
        foreach(Transform child in transform)
        {
            if(child.name == "Focus")
            {
                continue;
            }
            scrambled.Add(i);
            i++;
        }
        for(i = 0; i < scrambled.Count; i++)
        {
            int r = Random.Range(0, scrambled.Count);
            int o = scrambled[i];
            scrambled[i] = scrambled[r];
            scrambled[r] = o;
        }
        for (i = 0; i < scrambled.Count - 1; i++)
        {
            Vector3 op = transform.GetChild(scrambled[i]).position;
            transform.GetChild(scrambled[i]).position = transform.GetChild(scrambled[i + 1]).position;
            transform.GetChild(scrambled[i + 1]).position = op;
        }
    }

    public override void WhilePuzzleOpen()
    {
        base.WhilePuzzleOpen();
        if(Input.GetMouseButtonDown(0))
        {
            print("shot");
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10000, ignoreRaycast.value))
            {
                print("hit " + hit.collider.name);
                if (hit.transform.IsChildOf(transform))
                {
                    if(selectedPiece == -1)
                    {
                        selectedPiece = hit.transform.GetSiblingIndex();
                    }
                    else
                    {
                        Vector3 op = transform.GetChild(selectedPiece).position;
                        transform.GetChild(selectedPiece).position = hit.transform.position;
                        hit.transform.position = op;
                        GetComponent<AudioSource>().clip = clip;
                        GetComponent<AudioSource>().Play();
                        selectedPiece = -1;
                    }
                    
                }
            }
        }
    }


    bool EvalPieces()
    {
        foreach (FlagPuzzlePiece child in transform.GetComponentsInChildren<FlagPuzzlePiece>())
        {
            if(!child.isInOriginalPlace())
            {
                return false;
            }
        }
        return true;
    }
}
