using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportBack : MonoBehaviour
{
    public UnityEvent onTeleport = new UnityEvent(); 
    [SerializeField] Transform telepoint;
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerMove>())
        {
            CharacterController cc;
            if(cc = other.GetComponent<CharacterController>())
            {
                cc.enabled = false;
            }
            Rigidbody rb;
            if (rb = other.GetComponent<Rigidbody>())
            {
                rb.isKinematic = true;
            }
            other.transform.position = telepoint.position;
            if (cc)
            {
                cc.enabled = true;
            }
            if (rb)
            {
                rb.isKinematic = false;
            }
            onTeleport.Invoke();
        }
    }
}
