using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetHandlers : MonoBehaviour
{
    public static TargetHandlers instance;
    [System.Serializable]
    class Target
    {
        public Transform transform;
        public bool hasBeenHit;
    }
    [SerializeField] List<Target> targets;
    public UnityEvent Hit = new UnityEvent();
    public AudioSource blip;

    private void Start()
    {
        instance = this;
    }

    public void hitTarget(GameObject potentalTarget)
    {
        bool changed = false;
        foreach(Target target in targets)
        {
            if(target.transform == potentalTarget.transform)
            {
                if (target.hasBeenHit)
                    return;
                changed = true;
                target.hasBeenHit = true;
            }
        }
        if(changed)
        {
            print("changed because of " + potentalTarget.name);
            blip.Play();
            blip.pitch += 0.1f;
            Evaluate();
        }
    }

    public void Evaluate()
    {
        foreach (Target target in targets)
        {
            if(!target.hasBeenHit)
            {
                print("nothit");
                return;
            }
        }
        print("should have been hit");
        Hit.Invoke();
    }
}
