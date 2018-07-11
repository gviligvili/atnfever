using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreOverview : MonoBehaviour
{
    List<Player> activePlayers;
    public Dictionary<string, int> scoreMap;
    public GameObject scoreRowPrefab;
    const string SCORE_ROW_PREFIX = "ScoreRow";

    // Use this for initialization
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        activePlayers = gameManager.playersPlayed;
        scoreMap = gameManager.finalScore;
        initScoreRows(activePlayers.Count);
        updateScoreBoard();
    }

    private void initScoreRows(int numOfActivePlayers)
    {
        for (int i = 0; i < numOfActivePlayers; i++)
        {
            GameObject scoreRowGO = Instantiate(scoreRowPrefab);
            scoreRowGO.name = SCORE_ROW_PREFIX + i;
            scoreRowGO.transform.SetParent(this.transform);
            scoreRowGO.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void updateScoreBoard()
    {
        List<Player> clonedList = new List<Player>(activePlayers);

        scoreMap = ScoreManager.scoreMap;
        List<GameObject> scoreRowsGOs = getScoreRowsByOrder(clonedList.Count);

        int index = 0;

        // Display results.
        foreach (var scoreItem in scoreMap.OrderByDescending(i => i.Value))
        {

            Player player = clonedList.Find((Player arg1) => arg1.id == scoreItem.Key);
            ScoreRow scoreRow = scoreRowsGOs[index].GetComponent<ScoreRow>();
            scoreRow.updateScore(player.id, scoreMap[player.id], player.playerColor);

            // Destroy the player.
            Destroy(player.gameObject);

            index++;
        }

        Destroy(GameObject.Find("GameManager"));
    }

    List<GameObject> getScoreRowsByOrder(int numOfActivePlayers)
    {
        List<GameObject> scoreRowsGOs = new List<GameObject>();
        for (int i = 0; i < numOfActivePlayers; i++)
        {
            scoreRowsGOs.Add(GameObject.Find(SCORE_ROW_PREFIX + i));
        }

        return scoreRowsGOs;
    }
}
