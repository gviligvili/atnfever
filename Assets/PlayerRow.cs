using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class PlayerRow : MonoBehaviour
{

    public PlayerColor playerColor;
    public Text PlayerName;
    public Text Right;
    public Text Left;

    public bool isActive;

    Color _myColor;
    MenuManager menuManager;

	private void Awake()
	{
        menuManager = FindObjectOfType<MenuManager>();
        setPlayerColor(playerColor);
	}

	// Use this for initialization
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            bool isSpace = Input.GetKeyUp(KeyCode.Space);
            bool isMouse = Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1)
                                || Input.GetKeyUp(KeyCode.Mouse2) || Input.GetKeyUp(KeyCode.Mouse3)
                                || Input.GetKeyUp(KeyCode.Mouse4) || Input.GetKeyUp(KeyCode.Mouse5)
                                || Input.GetKeyUp(KeyCode.Mouse6);
            bool isEsc = Input.GetKeyUp(KeyCode.Escape);

            bool isForbidden = isSpace || isMouse || isEsc;
            if (isForbidden) return;

            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyUp(vKey))
                {
                    bool isLeftExist = Left.text.Length > 0;
                    bool isRightExist = Right.text.Length > 0;

                    if (!isLeftExist) {
                        Left.text = vKey.ToString();
                    } else if (!isRightExist) {
                        Right.text = vKey.ToString();
                        // We finished putting keys, turn the player off.
                        turnOff();
                    } else if (isLeftExist && isRightExist){
                        // If both turned on, reset both
                        Left.text = "";
                        Right.text = "";
                        // and init the left key again
                        Left.text = vKey.ToString();
                    }
                }
            }
        }
    }

    public void setPlayerColor(PlayerColor color)
    {
        switch(color) {
            case PlayerColor.RED:
                _myColor = new Color(255, 0, 0, 0.6f);
                break;
            case PlayerColor.BLUE:
                _myColor = new Color(0, 0, 255, 0.6f);
                break;
            case PlayerColor.GREEN:
                _myColor = new Color(0, 255, 0, 0.6f);
                break;
            case PlayerColor.PURPLE:
                _myColor = new Color(255, 0, 0, 0.6f);
                break;
            default:
                print("Player color wasn't handled");
                break;

        }

        setColor(_myColor);
    }

    public void setColor(Color newColor)
    {
        
        PlayerName.color = newColor;
        Right.color = newColor;
        Left.color = newColor;
    }

    private void OnMouseDown()
    {
        turnOn();
    }


    public void turnOn()
    {
        // turn everybody off first.
        menuManager.BlurAllPlayers();

        isActive = true;
        //If your mouse hovers over the text make it stronger.
        Color newColor = new Color(_myColor.r, _myColor.g, _myColor.b, 255);
        setColor(newColor);

    }

    public void turnOff()
    {
        isActive = false;

        // if one of the keys is not registered while turning off, reset them.
        bool isLeftExist = Left.text.Length > 0;
        bool isRightExist = Right.text.Length > 0;
        bool bothExist = isLeftExist && isRightExist;

        if (!bothExist) {
            Left.text = "";
            Right.text = "";
        }

        setColor(_myColor);
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the text make it stronger.
        Color newColor = new Color(_myColor.r, _myColor.g, _myColor.b, 255);
        setColor(newColor);
    }

    void OnMouseExit()
    {
        // If mouse exit and the player is currently focused dont exit.
        if (!isActive)
        {
            turnOff();
        }
    }

    public PlayerColor getPlayerColor() {
        return playerColor;
    }
}
