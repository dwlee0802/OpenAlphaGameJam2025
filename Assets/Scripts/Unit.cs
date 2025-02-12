using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    // reference to the terminal this unit is bound to
    public Terminal terminal;

    StateMachine stateMachine;

    int directionIndex = 0;
    Vector3[] directions;

    public Bullet bulletPrefab = null;

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

    public void ReceiveHit()
    {
        print(name + " received hit!");
    }
}