using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    private WayPointPath path;

    private Transform sourceWP;
    private Transform targetWP;
    private int targetWPIndex = 0;

    private float totalTimeToWP;
    private float elapasedTimeToWP = 0;
    private float speed = 3.0f;

    void TargetNextWaypoint()
    {
         //reset the elapsed time
        elapasedTimeToWP = 0;

        //determine the source
        sourceWP = path.GetWaypoint(targetWPIndex);

        //determine the targert
        targetWPIndex++;

        if(targetWPIndex >= path.GetWaypointCount())
        {
            targetWPIndex = 0;
        }
        targetWP = path.GetWaypoint(targetWPIndex);

        //calculate distace to waypoint
        float distanceToWP = Vector3.Distance(sourceWP.position, targetWP.position);

        //calculate time to waypoint
        totalTimeToWP = distanceToWP / speed;

    }
    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        elapasedTimeToWP += Time.deltaTime;

        float elapsedTimePercentage = elapasedTimeToWP / totalTimeToWP;

        //move 
        transform.position = Vector3.Lerp(sourceWP.position, targetWP.position, elapsedTimePercentage);

        if(elapsedTimePercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
    private void FixedUpdate()
    {
        MoveTowardsWaypoint();
    }
}
