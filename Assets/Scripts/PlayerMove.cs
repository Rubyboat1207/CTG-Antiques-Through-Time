using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;
    public bool SimplePlayerMove;

    // [SerializeField] means it shows up in the editor, even if its private
    public float speed = 5;
    public float jumpForce = 5;
    float yVelocity = 0;
    public bool canMove = true;

    CharacterController cc;
    void Start() {
        if(Instance == null)
        {
            Instance = this;
        }
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        // check for grounded status
        if(!cc.isGrounded) {
            // update y velocity by gravity
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }else{
            // usually i'd set it to 0 but there can be issues
            // so i'll set the velocity to -1 as a fail-safe
            yVelocity = -1f;
            
            // Implement Jumping
            if(Input.GetKey(KeyCode.Space)) {
                yVelocity = jumpForce;
            }
        }
        if (canMove)
        {
            // Grab the direct input from the user
            Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), yVelocity, Input.GetAxis("Vertical"));

            // Make moving no longer dependant on the frame rate
            Vector3 frameAdjustedMovement = inputVector * Time.deltaTime;

            // use our speed variable
            Vector3 finalVector = frameAdjustedMovement * speed;

            if(SimplePlayerMove) {
                finalVector.y = ((Input.GetKey(KeyCode.Space) ? 1 : 0) - (Input.GetKey(KeyCode.LeftShift) ? 1 : 0)) * Time.deltaTime * speed;
                transform.Translate(finalVector);
            }else{
                // Move the character by the vector
                cc.Move(finalVector);
            }
            
        }
    }
}
