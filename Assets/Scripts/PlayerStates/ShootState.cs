using UnityEngine;
using Unity.Netcode;

public class ShootState : State
{
    [SerializeField]
    State idleState = null;

    public override void Enter()
    {
        print("Entered shoot state");
    }

    public override void Exit()
    {
        print("Exited shoot state");
    }

    public override State UpdateProcess(float delta)
    {
        if (parent.ammoCount > 0)
        {
            if (parent.IsHost)
            {
                Bullet newBullet = parent.SpawnBullet();

                NetworkObject obj = newBullet.GetComponent<NetworkObject>();
                obj.Spawn();
                //obj.transform.SetParent(parent.transform);
            }
            else
            {
                parent.RequestSpawnServerRpc();
            }

            parent.ammoCount -= 1;

            print("Shot bullet!");
        }
        else
        {
            print("No ammo!");
        }

        return idleState;
    }

    public override State FixedUpdateProcess(float delta)
    {
        return null;
    }

}
