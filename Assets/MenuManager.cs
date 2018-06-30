using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    PlayerRow[] playersRow;
    public Button startButton;
    public Text errorTxt;
    public static List<PlayerRow> activePlayers;

	private void Awake()
	{
        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
        errorTxt.text = "";
        playersRow = FindObjectsOfType<PlayerRow>();
        startButton.onClick.AddListener(StartGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame() {
        bool isValid;

        try {
            activePlayers = ValidatePlayers();
            isValid = true;
        } catch (System.Exception e) {
            isValid = false;
            showError(e.Message);
        }

        if (isValid) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void showError(string message)
    {
        StartCoroutine(displayError(message));
    }

    IEnumerator displayError(string message){
        errorTxt.text = message;
        yield return new WaitForSeconds(5);
        errorTxt.text = "";
    }

    List<PlayerRow> ValidatePlayers()
	{
        List<PlayerRow> playersToActivate = new List<PlayerRow>();

        for (int i = 0; i < playersRow.Length; i++)
        {
            PlayerRow currPlayerRow = playersRow[i];
            bool isLeftKeyAssigned = currPlayerRow.Left.text.Length > 0;
            bool isRightKeyAssigned = currPlayerRow.Right.text.Length > 0;
            bool oneKeyIsMissing = (isLeftKeyAssigned && !isRightKeyAssigned) || (!isLeftKeyAssigned && isRightKeyAssigned);

            if(oneKeyIsMissing) {
                throw new System.Exception(currPlayerRow.PlayerName.text + " - please choose keys");
            }

            if (isLeftKeyAssigned && isRightKeyAssigned) {
                playersToActivate.Add(currPlayerRow);
            }
        }

        if (playersToActivate.Count < 1)
        {
            throw new System.Exception("At least one player is required.");
        }

        return playersToActivate;
    }
}
