using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    public PlayersSpawner playersSpawner;

	private void Start()
	{
        List<PlayerRow> activePlayers = MenuManager.activePlayers;

        playersSpawner.SpawnPlayers(0);
	}

	// Use this for initialization
	public void EndGame () {
        Debug.Log("GameOver");
        //StartCoroutine(PlayEndGameAnimation());
	}

    IEnumerator PlayEndGameAnimation() {
        Debug.Log("Playing end animation");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
