using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class TailPart : MonoBehaviour
{

    LineRenderer line;
    EdgeCollider2D col;
    List<Vector2> points;

    // Use this for initializa tion
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        points = new List<Vector2>();
    }

    public void setPoint(Vector3 followMePosition)
    {

        // We are getting the followMe (bottom of the plane) object world coords.
        // Transform the world coords to local coords (where should we put our self in our parent).
        Vector2 newLocalPoint = this.transform.InverseTransformPoint(followMePosition);

        // Set collider points, always 1 step back from the next step in the tail.
        // rigidedge needs at least 2 points
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2>();
        }

        points.Add(newLocalPoint);
        line.positionCount = this.points.Count;
        line.SetPosition(points.Count - 1, newLocalPoint);
    }
}