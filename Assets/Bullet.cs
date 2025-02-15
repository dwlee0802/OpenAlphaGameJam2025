using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    float lifeTime = 60;
    float speed = 10;

    public GameObject originUnit = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            return;
        }

        lifeTime -= Time.fixedDeltaTime;
        if(lifeTime < 0)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyObjectServerRpc();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.rotation * Vector3.right * Time.fixedDeltaTime * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!NetworkManager.Singleton.IsHost)
        {
            return;
        }

        if (other.CompareTag("Player") && other.gameObject != originUnit)
        {
            print("hit player!");
            other.GetComponent<Unit>().ReceiveHit();

            if (NetworkManager.Singleton.IsHost)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyObjectServerRpc();
            }
        }
        if (other.CompareTag("Wall"))
        {
            print("hit wall!");

            if (NetworkManager.Singleton.IsHost)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyObjectServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyObjectServerRpc(ServerRpcParams param = default)
    {
        print("requesting server destroy");

        gameObject.GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject);
    }
}
