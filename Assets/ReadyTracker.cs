using UnityEngine;
using Unity.Services.Relay;
using Unity.Netcode;

public class ReadyTracker : NetworkBehaviour
{
    public NetworkVariable<bool> hostReady;
    public NetworkVariable<bool> clientReady;

    public static ReadyTracker Singleton;

    public override void OnNetworkSpawn()
    {
        hostReady = new NetworkVariable<bool>();
        clientReady = new NetworkVariable<bool>();
        hostReady.Value = true;
        clientReady.Value = true;
    }

    public void UnreadyAll()
    {
        hostReady.Value = false;
        clientReady.Value = false;
    }
}
