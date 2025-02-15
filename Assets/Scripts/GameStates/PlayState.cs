using UnityEngine;

public class PlayState : GameState
{
    [SerializeField]
    GameState postGameState = null;

    public override void Enter()
    {
        print("Entered play GameState");
        parent.InGameUI.transform.gameObject.SetActive(true);
        parent.preGameUI.transform.gameObject.SetActive(false);
        parent.networkUI.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        print("Exited play GameState");
        parent.InGameUI.transform.gameObject.SetActive(false);
    }

    public override GameState UpdateProcess(float delta)
    {
        int playersAliveCount = GameManager.units.Count;

        foreach(Unit unit in GameManager.units)
        {
            if (unit.isDead)
            {
                playersAliveCount -= 1;
            }
        }

        if (playersAliveCount == 1)
        {
            print("Only one player alive. End game");
            return postGameState;
        }

        return null;
    }
}
