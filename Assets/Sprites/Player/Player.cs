using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Head head;
    public Tail tail;

    IEnumerator speedCoroutine;
    bool speedCoroutineRunning;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeSpeed(SpeedPowerUpEnum status,float duration) {
        // If a speed coroutine is running, stop it.
        if (speedCoroutineRunning) {
            speedCoroutineRunning = false;
            StopCoroutine(speedCoroutine);
        }

        // create coroutine of that speed change.
        speedCoroutine = changeSpeedCoroutine(status, duration);
        StartCoroutine(speedCoroutine);

    }

    public IEnumerator changeSpeedCoroutine(SpeedPowerUpEnum status, float duration) {
        speedCoroutineRunning = true;
        this.head.setSpeedPowerUpStatus(status);
        yield return new WaitForSeconds(duration);
        this.head.setSpeedPowerUpStatus(SpeedPowerUpEnum.NONE);
        Debug.Log("Reched END OF ROUTINE !");
        speedCoroutineRunning = false;
    }


}
