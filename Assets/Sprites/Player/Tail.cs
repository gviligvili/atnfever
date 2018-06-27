using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour {

    public GameObject TailPartPrefab;
    public Transform followMe;
    public float pointSpacing;
    public float drawingTime;
    public float drawPauseTime;


    public List<TailPart> tailsPartList;
    TailPart currTailPart;
    Vector2 lastPoint;
    bool isDrawing = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(StartDraw());
	}



    // Update is called once per frame
    void Update()
    {
        // if not drawing, return;
        if (!isDrawing) return;

        if (Vector3.Distance(lastPoint, followMe.position) > pointSpacing)
        {
            setPoint();
        }
    }


    void setPoint()
    {
        lastPoint = followMe.position;
        currTailPart.setPoint(lastPoint);
    }


    IEnumerator PauseDraw()
    {
        isDrawing = false;
        yield return new WaitForSeconds(drawPauseTime);
        StartCoroutine(StartDraw());
    }

    IEnumerator StartDraw()
    {
        createAssignNewTailPart();
        isDrawing = true;
        yield return new WaitForSeconds(drawingTime);
        StartCoroutine(PauseDraw());
    }

    private void createAssignNewTailPart() {

        // if it's the first create, there wont be any thing to push; 
        if (currTailPart){
            tailsPartList.Add(currTailPart);
        }

        GameObject newTailPart = (GameObject)Instantiate(TailPartPrefab);
        currTailPart = newTailPart.GetComponent<TailPart>();

        // Set the first point
        currTailPart.setPoint(followMe.position);
    }
}
