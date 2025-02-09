using UnityEngine;
using Microsoft.Win32;

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    State startingState;
    State currentState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Initialize(Unit parent)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<State>().parent = parent;
        }
        ChangeState(startingState);
    }

    public void UpdateProcess(float delta)
    {
        State newState = currentState.UpdateProcess(delta);
        if (newState != null)
        {
            ChangeState(newState);
        }
    }
}
