using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform gunModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunModel = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // rotation effect
        gunModel.Rotate(new Vector3(0, 0, 1) * 50 * Time.deltaTime);
    }

    public void Pickup()
    {
        print("Gun was picked up!");
        Destroy(gameObject);
    }
}
