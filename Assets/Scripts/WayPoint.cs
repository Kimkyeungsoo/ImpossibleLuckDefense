using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();

    public List<Transform> GetWayPoints()
    {
        return wayPoints;
    }
}
