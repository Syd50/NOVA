//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.Rendering;

//public class PlayerMovement : MonoBehaviour
//{

//    public Rigidbody rb;

//    public float forwardForce = 200f;
//    public float sidewaysForce = 500f;
 

//    void FixedUpdate()
//    {
//        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

//        if (Keyboard.current.dKey.isPressed)
//        {
//            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
//        }

//        if(Keyboard.current.aKey.isPressed)
//        {
//            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
//        }



//    }
//}
