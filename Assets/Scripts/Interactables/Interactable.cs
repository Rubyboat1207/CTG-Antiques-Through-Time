using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // 1. Check a radius, and if the player presses a button, the interaction occurs
    // 2. Get the player, then get the distance, then if the distance is lower than maximum, allow the interaction
    // 3. trigger component, then if the player steps inside the 
    //    trigger you set a flag to be true, and if the flag is true, you can interact
    [Header("Base Interactable")]
    [SerializeField] Vector3 ParticleOffset;

    public float extraInteractDistance = 0;

    GameObject InteractEffectInstance;

    // Update is called once per frame
    protected void Update()
    {
        // Check to see if this interactable is selected
        if (Interacter.selectedInteract == gameObject)
        {
            
            // Preform Interact Check
            if(Input.GetKeyDown(KeyCode.E)) {
                Interact();
            }

            // Spawn Effect Particle
            if(InteractEffectInstance == null) {
                CreateParticle();
            }
        }
        //Check to see if an interactable is left over
        else if(InteractEffectInstance) {
            Destroy(InteractEffectInstance);
        }
    }

    protected void Start() {
        Interacter.interactables.Add(this);
    }

    public virtual void Interact()
    {
        print("Interacted with: " + gameObject.name); 
    }

    //This was going to be used to make it not targetable, but then i just decided to add isTargetable()
    //Could be used to add multiple particles maybe
    public virtual void CreateParticle() {
        //Instanciate Object
        InteractEffectInstance = GameObject.Instantiate(
            // Find Particle Effect
            Resources.Load<GameObject>("Particles/Interact"),
            // Set parent
            transform
        );
        //Move the particle
        InteractEffectInstance.transform.localPosition = Vector3.zero + ParticleOffset;
    }

    public virtual bool isTargetable() {
        return true;
    }

    void OnDestroy() {
        Interacter.interactables.Remove(this);
    }
}
