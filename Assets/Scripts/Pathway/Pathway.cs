using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathway : MonoBehaviour
{
    void Update() {
        WayPoint[] wayPoints = GetComponentsInChildren<WayPoint>();
        if (wayPoints.Length > 1) {
            int index;
            for (index = 1; index < wayPoints.Length; index++) {
                Debug.DrawLine(wayPoints[index - 1].transform.position, wayPoints[index].transform.position, Color.black);
            }
        }
    }
    
    public WayPoint GetNearestWayPoint(Vector3 positon) {
        float minDistance = float.MaxValue;
        WayPoint nearestWayPonit = null;
        foreach (WayPoint wayPoint in GetComponentsInChildren<WayPoint>()) {
            if (wayPoint.GetHashCode() != GetHashCode()) {
                Vector3 vector3 = positon - wayPoint.transform.position;
                if (minDistance > vector3.magnitude) {
                    minDistance = vector3.magnitude;
                    nearestWayPonit = wayPoint;
                }
            }
        }
        return nearestWayPonit;
    }

    public WayPoint GetNextWayPoint(WayPoint currentWaypoint, bool loop) {
        WayPoint res = null;
        int index = currentWaypoint.transform.GetSiblingIndex();
        if (index < transform.childCount -1) {
            index += 1;
        } else {
            index = 0;
        }
        if (!(loop == false && index == 0)) {
            res = transform.GetChild(index).GetComponent<WayPoint>();
        }
        return res;
    }

    public float GetPathDistance(WayPoint startWayPoint) {
        WayPoint[] wayPoints = GetComponentsInChildren<WayPoint>();
        bool reached = false;
        float pathDistance = 0f;
        int index;
        for (index = 0; index < wayPoints.Length; index++) {
            if (reached == true) {
                Vector2 distance = wayPoints[index].transform.position - wayPoints[index - 1].transform.position;
                pathDistance += distance.magnitude;
            }
            if (wayPoints[index] == startWayPoint) {
                reached = true;
            }
        }
        return pathDistance;
    }
}
