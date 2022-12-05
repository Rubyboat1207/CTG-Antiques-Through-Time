using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateTrigger : MonoBehaviour
{
    public Vector3 eulerAngles = Vector3.zero;
    private void OnTriggerStay(Collider other)
    {
        PlayerMove.Instance.transform.rotation = Quaternion.Euler(eulerAngles);
        Destroy(this);
    }
}
