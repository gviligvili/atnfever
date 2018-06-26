using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpawner : MonoBehaviour {

    public float width;
    public float height;
    public float radius;
    public GameObject player;

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SpawnPlayers(int numOfPlayers) {
        Vector2[] playersPositions = getPlayersPositions(numOfPlayers);
        DrawPlayers(playersPositions);
    }

    void DrawPlayers(Vector2[] playersPositions){
        Debug.Log("Drawing Players");
        foreach(Vector2 playerPos in playersPositions){
            // Draw a single player
            GameObject currPlayer = Instantiate(player, playerPos, Quaternion.identity);
            // Create a random angle for the spawning point. (so not everyone will be at the same direction).
            int alpha = Random.Range(-180, 180);
            currPlayer.transform.Rotate(new Vector3(0, 0, alpha));
        }
    }

    Vector2[] getPlayersPositions(int numOfPlayers) {
        Vector2[] spawnedPoints = new Vector2[numOfPlayers];

        // For each play, get a spawn point
        for (int i = 0; i < numOfPlayers; i++)
        {
            bool spawned = false;

            // Don't stop until you find a spot for the player.
            while (!spawned)
            {
                Vector2 newPos = getRandomXY();

                // If the spawn point doesn't collide with others, it's valid.
                bool isValid = !doesPointCollideWithPoints(newPos, spawnedPoints);

                if (isValid)
                {
                    // Stop trying to spawn the same point.
                    spawned = true;
                    spawnedPoints[i] = newPos;
                }
            }
        }

        return spawnedPoints;
    }

    bool doesPointCollideWithPoints(Vector2 newPos, Vector2[] points){
        bool doesCollide = false;

        foreach (Vector2 spnPoint in points)
        {
            if (doesPointsCollide(spnPoint, newPos))
            {
                doesCollide = true;
                break;
            }
        }

        return doesCollide;
    }

    bool doesPointsCollide(Vector2 point1, Vector2 point2) {
        float distanceSqr = (point1 - point2).sqrMagnitude;
        return distanceSqr < radius;
    }

    Vector2 getRandomXY() {
        float halfHeight = height / 2;
        float halfWidth = width / 2;

        float x = Random.Range(-halfWidth, halfWidth);
        float y = Random.Range(-halfHeight, halfHeight);

        return new Vector2(x, y);
    }


}
