using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public String id;
    public Head head;
    public Tail tail;
    public ScoreManager scoreManager;
    public PlayerColor playerColor;

    IEnumerator speedCoroutine;
    bool speedCoroutineRunning;
    IEnumerator thicknessCoroutine;
    bool thicknessCoroutineRunning;
    PlayersSpawner playersSpawner;
    PowerUpsSpawner powerUpsSpawner;

	private void Awake()
	{
        playersSpawner = FindObjectOfType<PlayersSpawner>();
        powerUpsSpawner = FindObjectOfType<PowerUpsSpawner>();
        scoreManager = FindObjectOfType<ScoreManager>();
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

    public void kill()
    {
        head.killHead();
        tail.killTail();
        scoreManager.playerIsDead(this);
    }

    public void pickedPowerUp(PowerUp powerUp)
    {
        powerUpsSpawner.PickedPowerUp(this, powerUp);
    }
}
