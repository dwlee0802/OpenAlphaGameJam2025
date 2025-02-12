using UnityEngine;

public class EquipState : State
{
    [SerializeField]
    private State idleState = null;

    Gun gunObject = null;

    public override void Enter()
    {
        print("Entered equip state");
        GameObject obj = CheckIfOnGun();
        if (obj != null)
        {
            print("found gun!");
            gunObject = obj.GetComponent<Gun>();
        }
        else
        {
            print("gun not found!");
        }
    }

    public override State UpdateProcess(float delta)
    {
        if(gunObject != null)
        {
            gunObject.Pickup();
            parent.ammoCount += 1;
            print("Increased " + parent.name + " ammo count by one");
        }

        return idleState;
    }

    public override void Exit()
    {
        print("Exited equip state");
    }

    GameObject CheckIfOnGun()
    {
        Collider[] colliders = Physics.OverlapSphere(parent.transform.position, 0.2f);
        foreach(var item in colliders)
        {
            if (item.gameObject.CompareTag("Gun"))
            {
                return item.gameObject;
            }
        }

        return null;
    }
}