using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField]
    GameState startingState;
    GameState currentState;

    public void ChangeState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Initialize(GameManager parent)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<GameState>().parent = parent;
        }
        ChangeState(startingState);
    }

    public void UpdateProcess(float delta)
    {
        GameState newState = currentState.UpdateProcess(delta);
        if (newState != null)
        {
            ChangeState(newState);
        }
    }
}
