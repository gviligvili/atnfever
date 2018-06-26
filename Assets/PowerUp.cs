using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsEnum {
    SELF_SPEED, SELF_THIN, SELF_SLOW, SELF_SPACING, SELF_TRAVERSE_WALL,
    OTHERS_SPEED, OTHERS_THICK, OTHERS_SLOW, OTHERS_SQUARE_MOVE, OTHERS_CONFUSE,
    ALL_TRAVERSE_WALL, ALL_ERASE_ALL_LINES,
    GENERAL_MORE_POWER_UPS
}


public class PowerUp : MonoBehaviour {

    public PowerUpsEnum powerUpKind;
    public float duration;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
