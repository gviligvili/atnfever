using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    public int numOfMatches;
    public int allowedMatches;
    public List<Player> playersPlayed;
    public Dictionary<string, int> finalScore;

    // Use this for initialization
    public void EndMatch () {
        Debug.Log("GameOver");
        DontDestroyOnLoad(this);
        bool isThereATie = ScoreManager.isThereATie();

        // Don't end the game if there is a tie.
        if(numOfMatches < allowedMatches - 1 || isThereATie) {
            numOfMatches++;
        } else {
            playersPlayed = FindObjectOfType<PlayersSpawner>().GetAllPlayers();
            playersPlayed.ForEach((Player obj) => DontDestroyOnLoad(obj));
            finalScore = ScoreManager.scoreMap;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        StartCoroutine(PlayEndMatchAnimation());
	}

    IEnumerator PlayEndMatchAnimation() {
        Debug.Log("Playing end animation");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
