using UnityEngine;

public class Star : MonoBehaviour
{

    

    //OnTriggerEnter - unity's event system
    //this will automatically do something / acknowledge when another object is close to it
    private void OnTriggerEnter(Collider other) //(Collider other) the thing that touched this star
    {
        //what object exactly are we checking for, does it have a tag???
        // other.gameObject.tag = "SphereThatCollidesWithStar";  -- will change ANY tag of something that collides with star

        if (other.gameObject.CompareTag("SphereThatCollidesWithStar"))
        {
            Debug.Log("A sphere touched the star");

            //does the thing that touched this star have a PlayerInventory component
            //other - the thing that collided - because you can't  name everything that MIGHT collide  --- BUT MAYBE YOU SHOULD --- specify ONLY the sphere can collide
            //GetComponent<PlayerInventory>() - look for the script
            PlayerInventory playerInventory = FindAnyObjectByType<PlayerInventory>();


            //did we actually find a player inventory
            if (playerInventory != null)
            {
                playerInventory.StarCollected();
                gameObject.SetActive(false);
            }

        }


        //does the thing that touched this star have a PlayerInventory component
        //other - the thing that collided - because you can't  name everything that MIGHT collide  --- BUT MAYBE YOU SHOULD --- specify ONLY the sphere can collide
        //GetComponent<PlayerInventory>() - look for the script
        
    }
}

//gameObject - the thing that this script is attached to
//other.gameObject - the object that collided / triggered this event
