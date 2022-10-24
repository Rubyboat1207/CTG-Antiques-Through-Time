using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAnimator : Interactable
{
    [Header("Animate On Interact")]
    public InteractAnimatable[] animations;

    [System.Serializable]
    public struct InteractAnimatable {
        public Animation ObjectToAnimate;
        public string[] animations;
    }
    int index = 0;

    public override void Interact()
    {
        index += 1;
        if(index >= animations.Length) {
            index = 0;
        }
        foreach(InteractAnimatable ia in animations) {
            ia.ObjectToAnimate.Play(ia.animations[index]);
        }
        
        base.Interact();

    }

    public override bool isTargetable()
    {
        foreach(InteractAnimatable ia in animations) {
            if(ia.ObjectToAnimate.isPlaying) {
                return false;
            }
        }
        return true;
    }
}
