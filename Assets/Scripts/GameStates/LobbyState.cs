using UnityEngine;

public class LobbyState : GameState
{
    [SerializeField]
    GameState waitState = null;


    public override void Enter()
    {
        print("Entered network GameState");
        parent.InGameUI.transform.gameObject.SetActive(false);
        parent.preGameUI.transform.gameObject.SetActive(false);
        parent.networkUI.gameObject.SetActive(true);
        parent.postGameUI.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        print("Exited network GameState");
        parent.networkUI.gameObject.SetActive(false);
        GameManager.instance.tempCamera.gameObject.SetActive(false);
        GameManager.instance.tempCamera.GetComponent<AudioListener>().enabled = false;
    }

    public override GameState UpdateProcess(float delta)
    {
        if (parent.networkUI.networkEstablished)
        {
            return waitState;
        }

        return null;
    }
}
