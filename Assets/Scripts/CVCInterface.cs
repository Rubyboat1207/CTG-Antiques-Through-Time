using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CVCInterface : MonoBehaviour
{
    public void SetFollow(Transform follow)
    {
        GetComponent<CinemachineVirtualCamera>().Follow = follow;
    }
}
