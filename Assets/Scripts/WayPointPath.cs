using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPath : MonoBehaviour
{
  //get the transform of a specific waypoint

    public  Transform GetWaypoint(int index)
    {
        return transform.GetChild(index).transform;
    }

    //return the number of waypoints in the path
    public int GetWaypointCount()
    {
        return transform.childCount;
    }

}
