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

    float duration = 0.5f;
    float time = 0;

    public override void Enter()
    {
        print("Entered rotate state");
        initialRotation = parent.gameObject.transform.rotation;
    }

    public override void Exit()
    {
        print("Exited rotate state");
        rotatedAmount = 0;
        time = 0;
    }

    public override State UpdateProcess(float delta)
    {
        return null;
    }

    public override State FixedUpdateProcess(float delta)
    {
        if (time < duration)
        {
            if (isRight)
            {
                parent.transform.rotation = Quaternion.Lerp(initialRotation, initialRotation * Quaternion.AngleAxis(90, Vector3.up), time / duration);
            }
            else
            {
                parent.transform.rotation = Quaternion.Lerp(initialRotation, initialRotation * Quaternion.AngleAxis(-90, Vector3.up), time/duration);
            }

            time += delta;

            return null;
        }
        else
        {
            if (isRight)
            {
                parent.transform.rotation = Quaternion.Lerp(initialRotation, initialRotation * Quaternion.AngleAxis(90, Vector3.up), 1);
            }
            else
            {
                parent.transform.rotation = Quaternion.Lerp(initialRotation, initialRotation * Quaternion.AngleAxis(-90, Vector3.up), 1);
            }
        }

        return idleState;
    }
}
