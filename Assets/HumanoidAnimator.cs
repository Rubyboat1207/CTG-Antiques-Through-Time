using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animator))]
public class HumanoidAnimator : MonoBehaviour
{
    [Header("Debug Info: ")]
    [SerializeField] Vector3 lastPos;
    [SerializeField] Vector3 curPos;
    [SerializeField] Vector3 positionDelta;
    Animator animator;

    private void Start() {
        lastPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        positionDelta = lastPos - curPos;

        Vector3 velocity = GetVelocity();

        animator.SetFloat("X_Vel", velocity.x);
        //End of frame
        lastPos = curPos;
    }

    Vector3 GetVelocity() {
        return positionDelta / Time.deltaTime;
    }
}
