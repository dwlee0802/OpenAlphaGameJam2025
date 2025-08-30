using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;
using TMPro;

public class GameManager : NetworkBehaviour
{
    GameStateMachine stateMachine;

    public static List<Unit> units = new List<Unit>();

    [SerializeField]
    Board board = null;

    [SerializeField]
    Terminal terminal = null;

    [SerializeField]
    Bullet bulletPrefab = null;

    [SerializeField]
    AmmoUI ammoUI = null;

    public static Board boardRef;

    public static GameManager instance = null;

    public InGameUI InGameUI;

    public PreGameUI preGameUI;

    public NetworkUI networkUI;

    public PostGameUI postGameUI;

    public Transform tempCamera;

    public static bool hostReady = false;
    public static bool clientReady = false;

    public ReadyTracker tracker;


    private void Start()
    {
        board.InitializeGrid();

        // inject ref for board to Unit class
        GameManager.boardRef = board;

        GameManager.instance = this;

        stateMachine = GetComponentInChildren<GameStateMachine>();
        stateMachine.Initialize(this);
    }

    public static void AddUnit(Unit unit)
    {
        unit.bulletPrefab = instance.bulletPrefab;

        if (unit.IsOwner)
        {
            unit.terminal = instance.terminal;

            // unit ref for ammo count ui
            instance.ammoUI.boundUnit = unit;

            // inject ref to unit for bullet prefab
        }

        if (!unit.IsOwner)
        {
            // deactivate camera if not player's
            unit.transform.GetChild(3).GetComponent<Camera>().enabled = false;
            unit.transform.GetComponentInChildren<AudioListener>().enabled = false;
        }
        else
        {
            unit.transform.GetChild(3).GetComponent<Camera>().enabled = true;
            unit.transform.GetComponentInChildren<AudioListener>().enabled = true;
        }
        
        if (units.Count == 0)
        {
            unit.transform.position = Board.hostSpawn;
        }
        else
        {
            unit.transform.position = Board.clientSpawn;
            unit.transform.Rotate(Vector3.up, 180);
        }


        units.Add(unit);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateProcess(Time.deltaTime);
    }

    public static int PlayerCount()
    {
        if (units == null)
        {
            return 0;
        }

        return units.Count;
    }


    public bool EveryoneReady()
    {
        foreach(Unit unit in GameManager.units)
        {
            if (!unit.isReady.Value)
            {
                return false;
            }
        }

        return true;
    }
}