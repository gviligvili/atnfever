using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpsSpawner : MonoBehaviour {

    public PlayersSpawner playersSpawner;
    public float width;
    public float height;
    public PowerUpsEnum PowerUpTypes;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

	// Use this for initialization
	void Start () {

        // Spawn every X time
        Spawn();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn() {
        // Get random XY within the spawner range.
        Vector2 pos = getRandomXY();
    }


    Vector2 getRandomXY()
    {
        float halfHeight = height / 2;
        float halfWidth = width / 2;

        float x = UnityEngine.Random.Range(-halfWidth, halfWidth);
        float y = UnityEngine.Random.Range(-halfHeight, halfHeight);

        return new Vector2(x, y);
    }


    public void applyPowerUp(Player player, PowerUp powerUp) {
        PowerUpsEnum kind = powerUp.powerUpKind;
        switch (powerUp.powerUpKind) {
            case (PowerUpsEnum.SELF_SPEED):
            case (PowerUpsEnum.OTHERS_SPEED):
                player.changeSpeed(SpeedPowerUpEnum.FASTER, powerUp.duration);
                break;

            case (PowerUpsEnum.SELF_SLOW):
            case (PowerUpsEnum.OTHERS_SLOW):
                player.changeSpeed(SpeedPowerUpEnum.SLOWER, powerUp.duration);
                break;

            default:
                Debug.Log("Didn't handle power up: " + kind);
                break;
        }
    }

    public void applyPowerUpOnOthers(Player player, PowerUp powerUp) {
        List<Player> allPlayers = playersSpawner.GetAllPlayers();
        allPlayers.Remove(player);
        allPlayers.ForEach((Player otherPlayer) => applyPowerUp(otherPlayer, powerUp));
    }

    public void PickedPowerUp(Player player, PowerUp powerUp)
    {
        // If it's self power up, apply on the player only
        String kind = Enum.GetName(typeof(PowerUpsEnum), powerUp.powerUpKind);
        bool isSelf = kind.Contains("SELF");
        bool isOthers = kind.Contains("OTHERS");
        bool isGeneral = kind.Contains("GENERAL");

        if (isSelf){
            applyPowerUp(player, powerUp);
        }
        // if It's other, get all the other, and apply on them.
        if (isOthers) {
            applyPowerUpOnOthers(player, powerUp);
        }
        // if it all, use it on all player

        // if it's more power ups, increate the spawn rate.
    }
}
