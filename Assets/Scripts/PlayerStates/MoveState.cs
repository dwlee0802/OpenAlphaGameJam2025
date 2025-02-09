using UnityEngine;

public class MoveState : State
{
    [SerializeField]
    private State idleState = null;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    float moveSpeed = 10f;
    private float sinTime = 0f;

    public override void Enter()
    {
        print("Entered move state");

        Vector3 moveDirection = parent.transform.rotation * Vector3.right;
        startPosition = parent.transform.position;
        targetPosition = startPosition + moveDirection;

        sinTime = 0f;
    }

    public override State UpdateProcess(float delta)
    {
        if (Vector3.Distance(parent.transform.position, targetPosition) > 0.01f)
        {
            sinTime += delta * moveSpeed;
            sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);

            float t = Evaluate(sinTime);
            parent.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            return null;
        }

        parent.transform.position = targetPosition;
        print("Movement complete, switching to idle state.");
        return idleState;
    }

    public override void Exit()
    {
        print("Exited move state");
    }

    private float Evaluate(float x)
    {
        return 0.5f * Mathf.Sin(x - Mathf.PI / 2f) + 0.5f;
    }
}