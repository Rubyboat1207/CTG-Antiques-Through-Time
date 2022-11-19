using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.Rotate(new Vector3(0, 25 * Time.deltaTime * Input.GetAxis("Mouse X")));
        }
    }
}
