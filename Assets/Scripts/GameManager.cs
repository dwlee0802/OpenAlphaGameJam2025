using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Unit unit = null;

    [SerializeField]
    Board board = null;

    [SerializeField]
    Terminal terminal = null;

    private void Start()
    {
        board.InitializeGrid();

        // place unit
        unit.transform.position = Vector3.zero;

        // inject reference
        terminal.boundUnit = unit;
        unit.terminal = terminal;
    }
}