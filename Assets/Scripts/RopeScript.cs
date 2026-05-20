using UnityEngine;
using UnityEngine.InputSystem;

//REMEMBER!!!!!!!
//Csharp enforces TYPES. So if you want to use something, you have to declare what type it is.

[RequireComponent(typeof(LineRenderer))]
//UnityEngine.InputSystem.Controls.IPlayerActions
public class RopeScript : MonoBehaviour
{
    [Header("Rope Settings")]
    [SerializeField] private Transform TransPoint1;
    [SerializeField] private Transform TransPoint2;

    [Header("Prefab")]
    [SerializeField] private Transform BallPrefab;

    [Header("Slingshot Settings")]
    [SerializeField] private float mouseDepth = 10f;
    [SerializeField] private float shootForce = 1000f;

    private LineRenderer _lineRenderer;
    private Transform _newBall;
    private Camera _mainCamLocal;
    private Vector3 slingCenter;
    private Vector3 worldPos;

    void Start()
    {
        //find my drawing component so i can control it
        _lineRenderer = GetComponent<LineRenderer>();
        //the rope should have 3 points (ends and middle)
        _lineRenderer.positionCount = 3;

        //find this scene's main camera 
        _mainCamLocal = Camera.main;

        //left point + right point / 2 = middle point
        slingCenter = (TransPoint1.position + TransPoint2.position) / 2f;

        //set all the rope positions at the start
        //* keep refering back to these numbers when deciding which point of the rope to update
        _lineRenderer.SetPosition(0, TransPoint1.position);  //start
        _lineRenderer.SetPosition(1, slingCenter);  //middle
        _lineRenderer.SetPosition(2, TransPoint2.position);  //end
    }

    void Update()
    {
        //EVERY FRAME: check input, move ball, update rope, shoot if released
      
        if (TransPoint1 && TransPoint2) //check both rope points exist
        {
            //recalculate the cneter of the rope every frame
            //if the sling point moves, the centre updates too
            slingCenter = (TransPoint1.position + TransPoint2.position) / 2f;

            //keep the rope ends attached to the sling points
            _lineRenderer.SetPosition(0, TransPoint1.position);
            _lineRenderer.SetPosition(2, TransPoint2.position);
        }

        // CLICK START: create ball
        //if i click on this frame and there is no ball already
        if (Mouse.current.leftButton.wasPressedThisFrame && _newBall == null)
        {
            //then create a new ball at the center of the rope ------------------ LATER LEVEL ---> ADD LIMIT TO AMOUNT OF BALL YOU CAN CREATE + USE UI TO SHOW THE BALL COUNT LEFT?
            _newBall = Instantiate(BallPrefab, slingCenter, Quaternion.identity);

            //get the ball's physics component
            Rigidbody rb = _newBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //make the ball kinematic so it doesn't fall down while we drag it around
                //don't want gravity or physics pulling it away
                rb.isKinematic = true;
            }

            //move the middle rope point to the new ball's position
            //* refering back to the part with the numbers for start, middle and end of the rope.
            _lineRenderer.SetPosition(1, _newBall.position);
        }

        // HOLD: drag ball + rope middle
        //This runs every frame - while the mouse is held down AND a ball exists
        if (Mouse.current.leftButton.isPressed && _newBall != null)
        {

            Vector2 mousePos = Mouse.current.position.ReadValue(); // get mouse position on screeen
            Vector3 screenPos = new Vector3(mousePos.x, mousePos.y, mouseDepth); // turns the 2D mouse into a 3D screen position by adding depth
            //where does the mouse exist inside my 3D scene
            Vector3 worldPos = _mainCamLocal.ScreenToWorldPoint(screenPos); // convert the screen position into a world position
            //copy mouse world position to i can limit it
            Vector3 restrictedPos = worldPos;

            // ----------------  PROBLEM : You can shoot anywhere, towards anything  ------------------

            //Level 1 - just restrict a few axis.
            restrictedPos.x = Mathf.Clamp(worldPos.x, slingCenter.x - 2f, slingCenter.x + 2f);  // left / right
            restrictedPos.y = Mathf.Clamp(worldPos.y, slingCenter.y - 2f, slingCenter.y + 2);  // up / down
            restrictedPos.z = Mathf.Clamp(worldPos.z, slingCenter.z - 4f, slingCenter.z + -2f);  // forward / back ------- PROBLEM ------ you can shoot at the camera and break game

            //NEXT Level 2 restriction - you shouldn't be able to shoot the camera.
            //Vector3 worldPos is taken way too seriously. You shouldn't click in front, and just have the slingshot aim back and the camera.
            //Maybe try having an offset for worldPos, and the offset be BEHIND the slingshot? So it thinks you're clicking there?

            _newBall.position = restrictedPos; //actually moves the ball - APPLIES THE ABOVE
            _lineRenderer.SetPosition(1, restrictedPos); //moves middle rope to position of ball
        }

        // RELEASE: shoot ball
        //if i let go this frame, AND a ball exists
        if (Mouse.current.leftButton.wasReleasedThisFrame && _newBall != null)
        {
            //gets rigid body again
            Rigidbody rb = _newBall.GetComponent<Rigidbody>();

            //check if the rigidbosy exists before using it
            //can't use something that isnt there, CHECK first
            if (rb != null)
            {
                //turns physics on
                rb.isKinematic = false;

                //calculate the direction from the ball to the sling center
                //target - current = direction
                //so - slightCenter -ballPosition = direction back to sling
                //normalize - makes the vector length 1. SO only stores direction and not distance.
                Vector3 shootDirection = (slingCenter - _newBall.position).normalized;

                //shoots ball in that direction with the force I set above and / or in the inspector
                rb.AddForce(shootDirection * shootForce);
            }

            //reset rope back the the way it was when it started., 
            _lineRenderer.SetPosition(1, slingCenter);

            //FORGET the ball. SO you can make a new one
            _newBall = null;
        }
    }
}

//---------------------------- everything below here is the same script, but messier with more problems and more learning / figuring out notes --------------------------------



// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//{
//    //line should be visibile at the start for player to see rope
//    _lineRenderer = GetComponent<LineRenderer>();
//    _lineRenderer.positionCount = 2;

//    _mainCamLocal = Camera.main;


//}

// Update is called once per frame
//void Update()
//{

////Click but NO ball
//if (Mouse.current.leftButton.wasPressedThisFrame && _newBall == null)
//{
//Instantiate (create copy of something)
//Ball Perfab (my prefab)
//Vector3.zero (position of the new ball - world center)
//Quaternion.identity (no rotation)
//c sharp 'Instantiate' == 'new' java
//_newBall = Instantiate(BallPrefab, TransPoint1.position + TransPoint2.position / 2, Quaternion.identity);
//_newBall = Instantiate(BallPrefab, Vector3.zero, Quaternion.identity);

//if (_newBall) //if new ball is truthy
//{
//Add 3rd point in the middle and if don't already have 3 points, make it 3.
//SetVertexCount
//if (_lineRenderer.positionCount < 3)
//    _lineRenderer.positionCount = 3;


//Get the ball's position in the world.
//Vector3 newPos = _newBall.position;
//Store it as a variable - new pos. And adjust its position
//This is taking the ball's position, but tweaking it for where we want the line to sit
//newPos.z = -.55f;
//newPos.y = .6f;

//1 = the middle point of line renderer
//Should the middle point of the line have a raycaster attached to it?
//_lineRenderer.SetPosition(1, newPos);
//----> What if there was a RayCaster figuring out where I have clicked???


//Ray ray = _mainCamLocal.ScreenPointToRay(pos);
//ray.origin = _mainCamLocal.transform.position;


//    }
//}
//now make the hold, dragging and release.
//if (Mouse.current.leftButton.isPressed && _newBall != null)
//{
//let  newPos = _newBall.position;  --- just copying position, havent told unity WHERE to move ball
//copy of ball's position
//Vector3 newPos = _newBall.position;
//Vector3 worldPos = _mainCamLocal.ScreenToWorldPoint(pos);

//take copied position and update to where the mouse is
//newPos.x = worldPos.x;
//newPos.y = worldPos.y;
//newPos.z = worldPos.z;

//actually put those positions onto the ball
//_newBall.position = newPos;

//NEXT THING: WHEN YOU HOLD, THE BALL JUST FALLS DOWN
//kinematic?? Something like when you hold it should be kinematic, so it doesn't fall down, but when you release it should be the opposite

//}
//else if (Mouse.current.leftButton.isPressed && _newBall == null)
//{
//    //do nothing
//}



//If I click, and the ball already exists
//if (Mouse.current.leftButton.wasPressedThisFrame && _newBall)
//{
//Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
//where is my mouse onscreen
//Vector2 mousePos = Mouse.current.position.ReadValue();

//create a 3d point, and adds depth for camera
//Vector3 pos = new Vector3(mousePos.x, mousePos.y, 10f);

//convert to world space
//Vector3 worldPos = _mainCamLocal.ScreenToWorldPoint(pos);

//move the ball
//_newBall.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);

//this part is updating the cneter of the rope, but with a bit of an offset
//    Vector3 newPos = _newBall.position;
//    newPos.z -= .55f;
//    newPos.y += .6f;
//    _lineRenderer.SetPosition(1, newPos);


//}
//if (Mouse.current.leftButton.wasReleasedThisFrame) //Release Mouse 
//{
//    Vector3 newPos = _newBall.position;
//    newPos.z = 0f;
//    newPos.y = .5f;
//    _lineRenderer.SetPosition(1, newPos);

//    _newBall.GetComponent<Rigidbody>().isKinematic = false;
//    _newBall.GetComponent<Rigidbody>().AddForce(_newBall.forward * 1000);
//    _newBall = null;
//}



//check both transform references in the scene are NOT null
//if (TransPoint1 && TransPoint2)
//{
//start and end points of line renderer 
//line[1] = newPos;
//line[line.length - 1] = transPoint2.position;

//            _lineRenderer.SetPosition(0, TransPoint1.position);
//            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, TransPoint2.position);
//        }
//    }
//}

// line  renderer getting stuck - needs a forward driving force? a physics / forward force thing 

//wasPressedThisFrame - starting click
//isPressed - being held ---> for dragging the ball around -- This should be the one updating every frame
//wasReleasedThisFrame - click ended




//3 things to consider - if statement or switch statement? Not just checking true and false, theres the isPressed thing too

//switch(CurrentState)
//{
//    case Idle;
//    case Dragging;
//    case Released;
//}

//Adding a raycaster?? Or something to stick the ball to the center point
//