using UnityEngine;

public class IdleState : State
{
    [SerializeField]
    State moveForwardState = null;

    public new void Enter()
    {
        print("Entered idle state");
        parent.terminal.field.interactable = true;
    }

    public new void Exit()
    {
        print("Entered exit state");
        parent.terminal.field.interactable = false;
    }

    public new State UpdateProcess(float delta)
    {
        print("update");
        parent.terminal.field.ActivateInputField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            parent.terminal.field.text = "";
            return ParseCommand(parent.terminal.field.text);
        }

        return null;
    }

    public new State FixedUpdateProcess(float delta)
    {
        return null;
    }

    public State ParseCommand(string commandString)
    {
        switch (commandString)
        {
            case "move forward":
                print("Move forward command");
                return moveForwardState;
            case "move back":
                print("Move back command");
                break;
            case "rotate right":
                print("rotate right command");
                break;
            case "rotate left":
                print("rotate left command");
                break;
            default:
                print("Command not recognized!");
                break;
        }

        return null;
    }
}
