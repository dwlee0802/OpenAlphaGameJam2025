using UnityEngine;

public class PostGameState : GameState
{
    [SerializeField]
    GameState waitState = null;

    public override void Enter()
    {
        print("Entered post GameState");
        GameManager.instance.InGameUI.gameObject.SetActive(false);
        GameManager.instance.preGameUI.gameObject.SetActive(false);
        GameManager.instance.postGameUI.gameObject.SetActive(true);
        GameManager.instance.networkUI.gameObject.SetActive(false);

        foreach(Unit unit in GameManager.units)
        {
            if (unit.IsOwner)
            {
                GameManager.instance.postGameUI.winLabel.gameObject.SetActive(!unit.isDead);
                GameManager.instance.postGameUI.loseLabel.gameObject.SetActive(unit.isDead);
            }
        }
    }

    public override void Exit()
    {
        GameManager.instance.postGameUI.gameObject.SetActive(false);
        print("Exited post GameState");
    }

    public override GameState UpdateProcess(float delta)
    {
        return null;
    }
}
