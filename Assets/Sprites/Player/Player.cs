using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Head head;
    public Tail tail;
    public PlayerColor playerColor;
    IEnumerator speedCoroutine;
    bool speedCoroutineRunning;
    IEnumerator thicknessCoroutine;
    bool thicknessCoroutineRunning;

	private void Awake()
	{
	}

	// Use this for initialization
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayerColor(PlayerColor newColor)
    {
        playerColor = newColor;
        head.GetComponent<SpriteRenderer>().color = PlayerMeta.convertPlayerColorToHeadColor(newColor);
        tail.setTailColor(PlayerMeta.convertPlayerColorToTailColor(newColor));

    }


    public void changeSpeed(SpeedPowerUpEnum status, float duration)
    {
        // If a speed coroutine is running, stop it.
        if (speedCoroutineRunning)
        {
            speedCoroutineRunning = false;
            StopCoroutine(speedCoroutine);
        }

        // create coroutine of that speed change.
        speedCoroutine = changeSpeedCoroutine(status, duration);
        StartCoroutine(speedCoroutine);

    }

    public IEnumerator changeSpeedCoroutine(SpeedPowerUpEnum status, float duration)
    {
        speedCoroutineRunning = true;
        this.head.setSpeedPowerUpStatus(status);
        yield return new WaitForSeconds(duration);
        this.head.setSpeedPowerUpStatus(SpeedPowerUpEnum.NONE);
        Debug.Log("Reched END OF ROUTINE !");
        speedCoroutineRunning = false;
    }

    public void setKeys(string leftKey, string rightKey)
	{
        head.setKeys(leftKey, rightKey);
	}
}
