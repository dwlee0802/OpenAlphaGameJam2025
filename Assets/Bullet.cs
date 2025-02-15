using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    float lifeTime = 60;
    float speed = 50;

    public bool originIsHost;

    bool used = false;

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
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.rotation * Vector3.right * Time.fixedDeltaTime * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (used)
        {
            return;
        }

        // only set used to true if the collision is meaningful
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Unit>().ReceiveHit();

            //if (NetworkManager.Singleton.IsHost)
            //{
            //    Destroy(gameObject);
            //}
            used = true;
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.CompareTag("Wall"))
        {
            print("hit wall!");

            //if (NetworkManager.Singleton.IsHost)
            //{
            //    Destroy(gameObject);
            //}
            used = true;
            GetComponent<MeshRenderer>().enabled = false;
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
