using UnityEngine;


public class Planets : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject prefab;
    public Rigidbody rb;
    //public Transform cube;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //stay in place when game starts
        rb.useGravity = true;
    }

    // Update is called once per frame

    //move in space when you've been hit by something
    //does the thing that hit me have a rigid body
    //Colliding with something doesn't keep happening / updating. It should be its own thing?

    //This won't work - all cubes are intersescting + have rigidbodies
    //So they explode to begin with unless they are touching nothing
    //The condition should not be rigid body - maybe instead the Prefab itself is the condition??
    //e.g ONLY if Prefab hits me, then act out the whole collision thing


    //if statement
    //if the planet it hit by a prefab, then collide / use collision physics


    private void OnCollisionEnter(Collision collision)
    {
        if (planetPrefab !=null) 
        {
            Debug.Log("RigidBody");
        }
    }



    void Update()
    {
     
    }
}

//Longer Fix: maybe make all individual collision boxes on each cube smaller. So they won't start by touching.
