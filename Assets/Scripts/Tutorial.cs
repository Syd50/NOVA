
//using UnityEngine;

//public class Tutorial : MonoBehaviour
//{
//    //everything you want to control to actually play without mouse input
//    public GameObject prefab;
//    public Transform TransPoint1;
//    public Transform TransPoint2;
//    public LineRenderer lineRenderer;
//    public Transform pullTarget;
//    public float shootForce;

//    private GameObject spawnedBall;

//    private Vector3 slingCenter;

//    //using cinemachine to move between virtual cameras?
//    public GameObject mainCam;

//    public void Start()
//    {
        //what am i using? Line renderer, i need to find that component
        //LineRenderer lineRenderer = GetComponent<LineRenderer>();

        //3 points because you need a middle one to pull to the pull target
        //lineRenderer.positionCount = 3;
        //make the actual centre like in the Rope Script
        //slingCenter = (TransPoint1.position + TransPoint2.position) / 2f;

        //same as rope script
        //lineRenderer.SetPosition(0, TransPoint1.position);  //start
        //lineRenderer.SetPosition(1, slingCenter);  //middle
        //lineRenderer.SetPosition(2, TransPoint2.position);  //end

        //The slingCenter can automatically go to the pullTarget

        //pieces of code i know i need

        //the ball will spawn there, but without touch somehow.
        // _newBall = Instantiate(BallPrefab, slingCenter, Quaternion.identity);

        //now start a coroutine? Nothing from here on just starts, its all being timed really specifically?

        //need to mvoe the ball to the empty position


    //}

    //IEnumerator AnimateBall()
    //{
        //yield return new WaitForSeconds(3);

        //spawnedBall = Instantiate(prefab, slingCenter, Quaternion.identity);
        //Vector3 newPos =  pullTarget.position;
        //you havent actually moved anything yet 

        
        //spawnedBall.transform.position = newPos;

        //dont take newball, the tutorial should already have its own one, only 1
        //middle part of rope to the ball point
        //lineRenderer.SetPosition(1, newPos);
    //}

//}
