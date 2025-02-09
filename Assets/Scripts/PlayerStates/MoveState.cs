using UnityEngine;

public class MoveState : State
{
    [SerializeField]
    State idleState = null;

    public override void Enter()
    {
        print("Entered move state");
    }

    public override void Exit()
    {
        print("Exited move state");
    }

    public override State UpdateProcess(float delta)
    {
        return idleState;
    }

    public override State FixedUpdateProcess(float delta)
    {
        return null;
    }
}
