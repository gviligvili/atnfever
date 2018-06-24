using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour {

    public float pointSpacing = .1f;
    public Transform head;

    LineRenderer line;
    EdgeCollider2D col;
    List<Vector2> points; 
	// Use this for initializa tion
	void Start () {
        // Attach the the Tail into the head.
        this.transform.position = head.position;

        line = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
        points = new List<Vector2>();
        setPoint(); 

	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.points.Last(), head.position) > pointSpacing){
            setPoint();
        }
	}

    void setPoint() {
        
        // Set collider points, always 1 step back from the next step in the tail.
        // rigidedge needs at least 2 points
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2>();
        }

        points.Add(head.position);

        line.positionCount = this.points.Count;
        line.SetPosition(points.Count - 1, head.position);

    }
}
