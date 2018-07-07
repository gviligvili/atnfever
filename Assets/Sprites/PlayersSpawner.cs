using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpawner : MonoBehaviour {

    public float width;
    public float height;
    public float radius;
    public GameObject playerFab;
    private List<Player> _allPlayers = new List<Player>();

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SpawnPlayers() {
        //List<PlayerMeta> playerMetas = MenuManager.activePlayers;
        List<PlayerMeta> playerMetas = createMockPlayers();

        Vector2[] playersPositions = getPlayersPositions(playerMetas.Count);
        DrawPlayers(playersPositions, playerMetas);
    }

    void DrawPlayers(Vector2[] playersPositions, List<PlayerMeta> playerMetas){
        Debug.Log("Drawing Players");

        for (int i = 0; i < playerMetas.Count; i++)
        {
            Vector2 playerPos = playersPositions[i];
            PlayerMeta currPlayerMeta = playerMetas[i];

            // Draw a single player
            GameObject currPlayerGO = Instantiate(playerFab, playerPos, Quaternion.identity);
            currPlayerGO.name = "Player " + i;
            Player currPlayer = currPlayerGO.GetComponent<Player>();
            currPlayer.id = currPlayerGO.name;
            currPlayer.setPlayerColor(currPlayerMeta.playerColor);
            currPlayer.setKeys(currPlayerMeta.LeftKey, currPlayerMeta.RightKey);
            _allPlayers.Add(currPlayer);

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


    public List<Player> GetAllPlayers() {
        return _allPlayers;
    }


    private List<PlayerMeta> createMockPlayers() {
        PlayerMeta player1 = new PlayerMeta();
        player1.playerColor = PlayerColor.RED;
        player1.LeftKey = "Alpha1";
        player1.RightKey = "Alpha2";

        PlayerMeta player2 = new PlayerMeta();
        player2.playerColor = PlayerColor.BLUE;
        player2.LeftKey = "Q";
        player2.RightKey = "W";

        PlayerMeta player3 = new PlayerMeta();
        player3.playerColor = PlayerColor.GREEN;
        player3.LeftKey = "A";
        player3.RightKey = "S";


        PlayerMeta player4 = new PlayerMeta();
        player4.playerColor = PlayerColor.PURPLE;
        player4.LeftKey = "Z";
        player4.RightKey = "X";


        List<PlayerMeta> playerMetas = new List<PlayerMeta>();
        playerMetas.Add(player1);
        playerMetas.Add(player2);
        playerMetas.Add(player3);
        playerMetas.Add(player4);

        return playerMetas;
    }
}
