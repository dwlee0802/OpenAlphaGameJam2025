using UnityEngine;
using TMPro;

public class WaitState : GameState
{
    [SerializeField]
    GameState playState = null;

    float timeTilStart = 3.0f;

    public override void Enter()
    {
        print("Entered Wait GameState");
        parent.InGameUI.transform.gameObject.SetActive(false);
        parent.preGameUI.transform.gameObject.SetActive(true);
        parent.networkUI.transform.gameObject.SetActive(false);
        parent.preGameUI.statusLabel.text = "Waiting for other player to join...";
    }

    public override void Exit()
    {
        print("Exited Wait GameState");
        parent.preGameUI.transform.gameObject.SetActive(false);
    }

    public override GameState UpdateProcess(float delta)
    {
        // check if player count is 2
        // if so, start in 3 seconds
        if (GameManager.PlayerCount() >= 2)
        {
            timeTilStart -= Time.deltaTime;
            parent.preGameUI.statusLabel.text = "Starting in " + (int)(timeTilStart + 1);

            if(timeTilStart < 0)
            {
                return playState;
            }
        }

        return null;
    }
}
