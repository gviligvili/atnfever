using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour {

    public static Dictionary<string, int> scoreMap = new Dictionary<string, int>();
    public List<Player> deadPlayers = new List<Player>();
    private PlayerScoreList playerScoreList;

	// Use this for initialization
	void Start () {
        playerScoreList = FindObjectOfType<PlayerScoreList>();
	}

    // if doesn't exist in the score map, register a new player with 0;
    public static void registerPlayers(List<Player> players) {

        foreach(Player p in players){
            if (!scoreMap.ContainsKey(p.id))
            {
                // init score map entry for player
                scoreMap.Add(p.id, 0);
            } 
        }
    }

    public void playerIsDead(Player player) {
        deadPlayers.Add(player);
        int numOfDeadPlayers = deadPlayers.Count;
        int numOfPlayers = scoreMap.Count;
        int additionalScore = (numOfDeadPlayers) * 10;

        // set score
        int oldScore = scoreMap[player.id];
        scoreMap[player.id] = oldScore + additionalScore;
        playerScoreList.updateScoreBoard();



        // if numOfDeadPlayers is totalPlayer -1, it means there is one player left, kill him.
        if (numOfDeadPlayers == numOfPlayers - 1) {
            PlayersSpawner playersSpawner = FindObjectOfType<PlayersSpawner>();
            List<Player> allPlayers = playersSpawner.GetAllPlayers();

            foreach (Player p in allPlayers) {
                if (!deadPlayers.Contains(p)) {
                    p.kill();
                }
            }
        }

        // If numOfDeadPlayers is equal to number of players, end game.
        if (numOfDeadPlayers == numOfPlayers) {
            FindObjectOfType<GameManager>().EndMatch();
        }
    }

    public static bool isThereATie() {
        List<KeyValuePair<string, int>> scoreList = scoreMap.OrderByDescending(i => i.Value).ToList();
        return scoreList[1].Value == scoreList[2].Value;
    }
}
