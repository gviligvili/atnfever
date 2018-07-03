using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor { RED, BLUE, GREEN, PURPLE }

public class PlayerMeta {
    public PlayerColor playerColor;
    public string LeftKey;
    public string RightKey;

    public static Color convertPlayerColorToHeadColor(PlayerColor playerColor){
        Color color;
        switch(playerColor) {
            case PlayerColor.RED:
                color = new Color(1, 0, 0, 1);
                break;
            case PlayerColor.BLUE:
                color = new Color(0, (223f/255f), 1, 1);
                break;
            case PlayerColor.GREEN:
                color = new Color(0, 1, (94f/255f), 1);
                break;
            case PlayerColor.PURPLE:
                color = new Color((200f/255f), (75f/ 255f), 1, 1);
                break;
            default:
                throw new System.Exception("Player color wasn't handled in switch");
        }

        return color;
    }

    public static Color convertPlayerColorToTailColor(PlayerColor playerColor)
    {
        Color color;
        switch (playerColor)
        {
            case PlayerColor.RED:
                color = new Color(1, (45f/255f), (55f/255f), 1);
                break;
            case PlayerColor.BLUE:
                color = new Color(0, (127f/255f), 1, 1);
                break;
            case PlayerColor.GREEN:
                color = new Color((70f/255f), (240f/255f), (90f/255f), 1);
                break;
            case PlayerColor.PURPLE:
                color = new Color((250f/255f), (90f/255f), 1, 1);
                break;
            default:
                throw new System.Exception("Player color wasn't handled in switch");
        }

        return color;
    }
}
