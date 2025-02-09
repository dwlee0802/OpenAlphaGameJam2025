using UnityEngine;

public class RotateState : State
{
    [SerializeField]
    bool isRight = true;

    [SerializeField]
    State idleState = null;

    Quaternion initialRotation;

    // rotation speed in degree per second
    float rotationSpeed = 180;

    float rotatedAmount = 0;

    public override void Enter()
    {
        print("Entered rotate state");
        initialRotation = parent.gameObject.transform.rotation;
    }

    public override void Exit()
    {
        print("Exited rotate state");
        rotatedAmount = 0;
    }

    public override State UpdateProcess(float delta)
    {
        if(rotatedAmount < 90)
        {
            if (isRight)
            {
                parent.transform.Rotate(new Vector3(0, rotationSpeed * delta, 0));
            }
            else
            {
                parent.transform.Rotate(new Vector3(0, rotationSpeed  * (-delta), 0));
            }
            rotatedAmount += rotationSpeed * delta;
            return null;
        }

        return idleState;
    }

    public override State FixedUpdateProcess(float delta)
    {
        return null;
    }
}
