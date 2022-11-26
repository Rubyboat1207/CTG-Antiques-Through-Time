using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AlwaysLookAt : MonoBehaviour
{
    public Transform target;
    public Vector3 Axis;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            transform.rotation = Quaternion.Euler(Axis);
        }
        transform.LookAt(target, Vector3.up);
        transform.Rotate(new Vector3(0,180));
    }
}
