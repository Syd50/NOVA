using UnityEngine;

public class PathwayGizmos : MonoBehaviour
{
    public Transform star;
    public Transform startPoint;
    public Transform endPoint;

    public bool isMovingForward = true;
    public float moveDuration = 10f;
    public float moveProgress = 0f;

    private void Update()
    {
        if (isMovingForward)
        {
            star.position = Vector3.Lerp(startPoint.position, endPoint.position, moveProgress);
        }
        else
        {
            star.position = Vector3.Lerp(endPoint.position, startPoint.position, moveProgress);
        }

        moveProgress += Time.deltaTime / moveDuration;

    }

    private void OnDrawGizmos()
    {
        //visualise lines
        if (star != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(star.transform.position, startPoint.position);
            Gizmos.DrawLine(star.transform.position, endPoint.position);
        }
    }




}
   

