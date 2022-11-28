using UnityEngine;
using LibSM64;

public class ExampleInputProvider : SM64InputProvider
{
    public GameObject cameraObject = null;
    public bool paused = false;
    public override Vector3 GetCameraLookDirection()
    {
        return cameraObject.transform.forward;
    }

    public override Vector2 GetJoystickAxes()
    {
        if (paused)
        {
            return Vector2.zero;
        }
        return new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );
    }

    public override bool GetButtonHeld( Button button )
    {
        if(paused)
        {
            return false;
        }
        switch( button )
        {
            case SM64InputProvider.Button.Jump:  return Input.GetKey(KeyCode.Space);
            case SM64InputProvider.Button.Kick:  return Input.GetKey(KeyCode.Z);
            case SM64InputProvider.Button.Stomp: return Input.GetKey(KeyCode.Return);
        }
        return false;
    }
}