using UnityEngine;

public class MoveState : State
{
    [SerializeField]
    State idleState = null;

    public new void Enter()
    {
        print("Entered move state");
    }

    public new void Exit()
    {
        print("Exited move state");
    }

    public new State UpdateProcess(float delta)
    {
        return null;
    }

    public new State FixedUpdateProcess(float delta)
    {
        return null;
    }
}
