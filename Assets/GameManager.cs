using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	private void Start()
	{
        SpawnPlayers();

	}

	// Use this for initialization
	public void EndGame () {
        Debug.Log("GameOver");
        StartCoroutine(PlayEndGameAnimation());
	}

    IEnumerator PlayEndGameAnimation() {
        Debug.Log("Playing end animation");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawnPlayers() {
        
    }
}
