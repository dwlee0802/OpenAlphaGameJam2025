using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    static List<Unit> units = new List<Unit>();

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

    [SerializeField]
    public Transform InGameUI;


    private void Start()
    {
        board.InitializeGrid();

        // inject ref for board to Unit class
        GameManager.boardRef = board;

        GameManager.instance = this;
    }

    public static void AddUnit(Unit unit)
    {
        unit.bulletPrefab = instance.bulletPrefab;

        if (unit.IsOwner)
        {
            unit.terminal = instance.terminal;

            // unit ref for ammo count ui
            instance.ammoUI.boundUnit = unit;

            print("Spawn position: " + unit.transform.position);

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

        unit.transform.position = Vector3.zero + units.Count * Vector3.right * 5;

        units.Add(unit);
    }
}