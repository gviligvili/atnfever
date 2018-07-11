using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerScoreList : MonoBehaviour
{

    public Dictionary<string, int> scoreMap;
    public GameObject scoreRowPrefab;
    static int numOfCreation = 0; 
    const string SCORE_ROW_PREFIX = "ScoreRow";

    // Use this for initialization
    void Start()
    {
        List<Player> activePlayers = FindObjectOfType<PlayersSpawner>().GetAllPlayers();
        initScoreRows(activePlayers.Count);
        updateScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {

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

        Debug.Log("Creating new Score rows ! " + numOfCreation);
        numOfCreation++;
    }

    public void updateScoreBoard()
    {
        List<Player> activePlayers = FindObjectOfType<PlayersSpawner>().GetAllPlayers();
        List<Player> clonedList = activePlayers.ToList<Player>();

        scoreMap = ScoreManager.scoreMap;
        List<GameObject> scoreRowsGOs = getScoreRowsByOrder(clonedList.Count);

        int index = 0;

        // Display results.
        foreach (var scoreItem in scoreMap.OrderByDescending(i => i.Value))
        {
            Player player = clonedList.Find((Player arg1) => arg1.id == scoreItem.Key);
            ScoreRow scoreRow = scoreRowsGOs[index].GetComponent<ScoreRow>();
            scoreRow.updateScore(player.id, scoreMap[player.id], player.playerColor);

            index++;
        }
    }

    List<GameObject> getScoreRowsByOrder(int numOfActivePlayers) {
        List<GameObject> scoreRowsGOs = new List<GameObject>();
        for (int i = 0; i < numOfActivePlayers; i++)
        {
            scoreRowsGOs.Add(GameObject.Find(SCORE_ROW_PREFIX+i));
        }

        return scoreRowsGOs;
    }
}
