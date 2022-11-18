using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBack : MonoBehaviour
{
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
        }
    }
}
