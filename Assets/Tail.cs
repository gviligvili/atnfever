using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour {

    public float pointSpacing = .1f;
    public Transform followMe;

    LineRenderer line;
    EdgeCollider2D col;
    List<Vector2> points;


    // Test
    public Head head;
	// Use this for initializa tion
	void Start () {
        // Attach the the Tail into the head.
        //this.transform.position = head.position;

        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        points = new List<Vector2>();
        setPoint(); 

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.points.Last(), followMe.position) > pointSpacing){
            setPoint();
        }
	}

    void setPoint() {

        // We are getting the followMe (bottom of the plane) object world coords.
        // Transform the world coords to local coords (where should we put our self in our parent).
        Vector2 newLocalPoint = this.transform.InverseTransformPoint(followMe.position);

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
