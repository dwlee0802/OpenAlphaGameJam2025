using UnityEngine;

public class IdleState : State
{
    [SerializeField]
    State moveForwardState = null;
    [SerializeField]
    State rotateRight = null;
    [SerializeField]
    State rotateLeft = null;
    [SerializeField]
    State shootState = null;
    [SerializeField]
    State equipState = null;

    public override void Enter()
    {
        print("Entered idle state");
        if (parent == null)
        {
            return;
        }
        if (!parent.IsHost)
        {
            return;
        }
        if (parent.terminal == null)
        {
            return;
        }
        parent.terminal.field.interactable = true;
    }

    public override void Exit()
    {
        print("Exited idle state");
        if (parent == null)
        {
            return;
        }
        if (!parent.IsHost)
        {
            return;
        }
        if (parent.terminal == null)
        {
            return;
        }
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
                return rotateRight;
            case "rotate left":
                print("rotate left command");
                return rotateLeft;
            case "shoot":
                print("Shoot command");
                return shootState;
            case "pick up gun":
                print("Pick up gun command");
                return equipState;
            default:
                print("Command " + commandString + " not recognized!");
                break;
        }

        return null;
    }
}
