using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class Unit : NetworkBehaviour
{
    // reference to the terminal this unit is bound to
    public Terminal terminal;

    StateMachine stateMachine;

    int directionIndex = 0;
    Vector3[] directions;

    public Bullet bulletPrefab = null;

    public int ammoCount = 1;

    public Transform laser;

    private void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        directions = new[] {
        new Vector3(1,0,0),
        new Vector3(0,0,-1),
        new Vector3(-1,0,0),
        new Vector3(0,0,1)
        };

        GetComponent<Rigidbody>().isKinematic = true;

        stateMachine = GetComponentInChildren<StateMachine>();

        stateMachine.Initialize(this);

        laser = transform.GetChild(2);
    }

    public override void OnNetworkSpawn()
    {
        GameManager.AddUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        stateMachine.UpdateProcess(Time.deltaTime);
    }

    public void ReceiveHit()
    {
        print(name + " received hit!");
    }

    [ServerRpc]
    public void RequestSpawnServerRpc()
    {
        Bullet newBullet = SpawnBullet();
        NetworkObject obj = newBullet.GetComponent<NetworkObject>();
        obj.Spawn();
        //obj.transform.SetParent(transform);
    }

    public Bullet SpawnBullet()
    {
        if(bulletPrefab == null)
        {
            print("Bullet prefab null!!!!!!!!!!!!!!!!!!");
        }

        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.position = transform.position + new Vector3(0, 1, 0);
        newBullet.originUnit = gameObject;
        if (ammoCount <= 0 && laser.gameObject.activeSelf == true)
        {
            laser.gameObject.SetActive(false);
        }

        return newBullet;
    }
}