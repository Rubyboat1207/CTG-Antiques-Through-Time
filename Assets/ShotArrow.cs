using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArrow : MonoBehaviour
{
    bool collided = false;
    public void Update()
    {
        if(!collided)
        {
            transform.Translate(Vector3.forward * -5 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target")
        {
            collided = true;
        }
    }
}