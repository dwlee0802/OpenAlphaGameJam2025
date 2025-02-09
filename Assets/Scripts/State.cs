using UnityEngine;

public class State: MonoBehaviour
{
    public Unit parent;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual State UpdateProcess(float delta)
    {
        return null;
    }

    public virtual State FixedUpdateProcess(float delta)
    {
        return null;
    }
}
