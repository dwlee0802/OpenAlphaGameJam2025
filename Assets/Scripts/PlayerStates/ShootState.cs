using UnityEngine;

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
            Bullet newBullet = Instantiate(parent.bulletPrefab);
            newBullet.transform.SetParent(parent.transform);
            newBullet.transform.rotation = transform.rotation;
            newBullet.transform.position = parent.transform.position;

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
