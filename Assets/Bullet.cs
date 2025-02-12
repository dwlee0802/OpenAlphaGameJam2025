using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 60;
    float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.fixedDeltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.rotation * Vector3.right * Time.fixedDeltaTime * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("hit player!");
        }
        if (other.CompareTag("Wall"))
        {
            print("hit wall!");
            Destroy(gameObject);
        }
    }
}
