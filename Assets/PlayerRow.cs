using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerRow : MonoBehaviour
{

    public Color myColor;
    public Text PlayerName;
    public Text Right;
    public Text Left;

    public bool isActive;

    // Use this for initialization
    void Start()
    {
        setColor(myColor);

        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            Debug.Log(vKey);

        }

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

    void setColor(Color color)
    {
        PlayerName.color = color;
        Right.color = color;
        Left.color = color;
    }


    private void OnMouseDown()
    {
        isActive = true;
    }


    public void turnOn()
    {
        isActive = true;
        //If your mouse hovers over the text make it stronger.
        Color newColor = new Color(myColor.r, myColor.g, myColor.b, 255);
        setColor(newColor);

    }

    public void turnOff()
    {
        isActive = false;
        setColor(myColor);

    }

    void OnMouseOver()
    {
        //If your mouse hovers over the text make it stronger.
        Color newColor = new Color(myColor.r, myColor.g, myColor.b, 255);
        setColor(newColor);
    }

    void OnMouseExit()
    {
        if (!isActive)
        {
            turnOff();
        }
    }

}
