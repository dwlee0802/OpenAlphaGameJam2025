using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class Unit : NetworkBehaviour
{
    public bool isHost = false;

    // reference to the terminal this unit is bound to
    public Terminal terminal;

    StateMachine stateMachine;

    int directionIndex = 0;
    Vector3[] directions;

    public Bullet bulletPrefab = null;

    public int ammoCount = 1;

    public Transform laser;

    public bool isDead = false;

    public NetworkVariable<bool> isReady;

    void Awake()
    {
    }

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

        if (isHost)
        {
           isReady = new NetworkVariable<bool>(
           value: true,
           NetworkVariableReadPermission.Everyone,
           NetworkVariableWritePermission.Server);
        }
        else
        {
            isReady.OnValueChanged += ReadyChanged;
        }
    }

    public override void OnNetworkSpawn()
    {
        GameManager.AddUnit(this);

        if (NetworkManager.IsHost)
        {
            if (IsOwner)
            {
                isHost = true;
            }
            else
            {
                isHost = false;
            }
        }
        else
        {
            if (IsOwner)
            {
                isHost = false;
            }
            else
            {
                isHost = true;
            }
        }

    }

    public void ReadyChanged(bool before, bool after)
    {
        print("ready changed to " + after);
    }

    [ServerRpc]
    public void RequestReadyServerRpc()
    {
        isReady.Value = true;
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

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        stateMachine.UpdateFixedProcess(Time.fixedDeltaTime);
    }

    public void ReceiveHit()
    {
        print(name + " received hit!");
        isDead = true;
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
        newBullet.transform.position = transform.position + new Vector3(0, 1, 0) + transform.rotation * Vector3.right * 0.8f;
        newBullet.originIsHost = isHost;

        if (ammoCount <= 0 && laser.gameObject.activeSelf == true)
        {
            laser.gameObject.SetActive(false);
        }

        return newBullet;
    }
}