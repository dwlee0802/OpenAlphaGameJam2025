using UnityEngine;

public class PostGameState : GameState
{
    [SerializeField]
    GameState waitState = null;

    bool restartPressed = false;

    private void Start()
    {
        GameManager.instance.postGameUI.restartButton.onClick.AddListener(OnRestartButtonPressed);

    }

    public override void Enter()
    {
        restartPressed = false;

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

        foreach (Unit unit in GameManager.units)
        {
            unit.UnreadyServerRpc();
        }
    }

    public override void Exit()
    {
        GameManager.instance.postGameUI.gameObject.SetActive(false);
        print("Exited post GameState");
    }

    public override GameState UpdateProcess(float delta)
    {
        if (restartPressed)
        {
            return waitState;
        }

        return null;
    }

    void OnRestartButtonPressed()
    {
        restartPressed = true;

        foreach(Unit unit in GameManager.units)
        {
            if (unit.IsOwner)
            {
                if (unit.IsHost)
                {
                    unit.ReadyServerRpc();
                }
                else
                {
                    unit.ReadyServerRpc();
                }
            }
        }
    }
}
