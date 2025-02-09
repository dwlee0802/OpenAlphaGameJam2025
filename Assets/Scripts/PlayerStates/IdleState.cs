using UnityEngine;

public class IdleState : State
{
    [SerializeField]
    State moveForwardState = null;

    public override void Enter()
    {
        print("Entered idle state");
        parent.terminal.field.interactable = true;
    }

    public override void Exit()
    {
        print("Exited idle state");
        parent.terminal.field.interactable = false;
    }

    public override State UpdateProcess(float delta)
    {
        parent.terminal.field.ActivateInputField();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputString = parent.terminal.field.text;
            parent.terminal.field.text = "";
            return ParseCommand(inputString);
        }

        return null;
    }

    public override State FixedUpdateProcess(float delta)
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
                print("Command " + commandString + " not recognized!");
                break;
        }

        return null;
    }
}
