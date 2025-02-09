using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    // reference to the terminal this unit is bound to
    public Terminal terminal;

    StateMachine stateMachine;

    int directionIndex = 0;
    Vector3[] directions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directions = new [] {
        new Vector3(1,0,0),
        new Vector3(0,0,-1),
        new Vector3(-1,0,0),
        new Vector3(0,0,1)
        };

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "StateMachine")
            {
                stateMachine = transform.GetChild(i).GetComponent<StateMachine>();
            }
        }

        stateMachine.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateProcess(Time.deltaTime);
    }

    public void ParseCommand(string commandString)
    {
        switch (commandString)
        {
            case "move forward":
                print("Move forward command");
                transform.position += directions[directionIndex];
                break;
            case "move back":
                print("Move back command");
                transform.position += directions[directionIndex];
                break;
            case "rotate right":
                print("rotate right command");
                transform.Rotate(0, 90, 0);
                directionIndex += 1;
                if (directionIndex >= 4)
                {
                    directionIndex = 0;
                }
                break;
            case "rotate left":
                print("rotate left command");
                transform.Rotate(0, -90, 0);
                directionIndex -= 1;
                if (directionIndex <= -1)
                {
                    directionIndex = 3;
                }
                break;
            default:
                print("Command not recognized!");
                break;
        }
    }
}