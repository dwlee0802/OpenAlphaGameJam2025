using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Unit unit = null;

    [SerializeField]
    Board board = null;

    [SerializeField]
    Terminal terminal = null;

    private void Awake()
    {
        unit.terminal = terminal;
    }

    private void Start()
    {
        board.InitializeGrid();

        // place unit
        unit.transform.position = Vector3.zero;
    }
}