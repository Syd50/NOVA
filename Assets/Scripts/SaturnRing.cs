using UnityEngine;

public class SaturnRing : MonoBehaviour
{

    // public Rigidbody rb;
    public GameObject saturnRing;

    // Update is called once per frame
    void Update()
    {
        saturnRing.transform.Rotate(0, Time.deltaTime * -10, 0);
    }
}

