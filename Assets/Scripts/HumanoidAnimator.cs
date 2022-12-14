using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Animator))]
public class HumanoidAnimator : MonoBehaviour
{
    [Header("Debug Info: ")]
    [SerializeField] Vector3 lastPos;
    [SerializeField] float initx;
    [SerializeField] Vector3 curPos;
    [SerializeField] Vector3 positionDelta;
    [SerializeField] bool facingLeft = false;
    [SerializeField] bool useInputForVelocityCalc = false;

    Animator animator;

    private void Start() {
        lastPos = transform.position;
        animator = GetComponent<Animator>();
        initx = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        positionDelta = (lastPos - curPos);

        Vector3 velocity = GetVelocity();

        animator.SetFloat("X_Vel", velocity.x + velocity.z);
        animator.SetFloat("Y_Vel", velocity.y);
        if(velocity.x != 0) {
            facingLeft = velocity.x < 0;
        }
        animator.SetBool("Grounded", GetComponent<CharacterController>().isGrounded);
        transform.localScale = new Vector3(initx * (facingLeft ? -1 : 1), transform.localScale.y, transform.localScale.z);
        //End of frame
        lastPos = curPos;
    }

    public Vector3 GetVelocity() {
        if(useInputForVelocityCalc)
        {
            return -new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        return positionDelta / Time.deltaTime;
    }
}
