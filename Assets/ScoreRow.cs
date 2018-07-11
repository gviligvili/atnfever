using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour {

    public Text playerText;
    public Text scoreText; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateScore(string playerName, int score, PlayerColor playerColor) {
        Color color = PlayerMeta.convertPlayerColorToHeadColor(playerColor);
        playerText.text = playerName;
        playerText.color = color;

        scoreText.text = score.ToString();
        scoreText.color = color;
    }
}
