using UnityEngine;

public class PlanetRotation : MonoBehaviour
{

    public Transform planet;
    public float speed = 10f;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.up * speed;

    }
}
