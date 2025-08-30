using UnityEngine;
using Unity.Netcode;

public class GameState: NetworkBehaviour
{
    public GameManager parent;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual GameState UpdateProcess(float delta)
    {
        return null;
    }

    public virtual GameState FixedUpdateProcess(float delta)
    {
        return null;
    }
}
