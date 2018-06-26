using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsEnum {
    SelfSpeed, SelfThin, SelfSlow, SelfSpacing, SelfTraverseWall,
    OthersSpeed, OthersThick, OthersSlow, OthersSquareMove, OthersSwitchDirection,
    AllMorePowerUps, AllTraverseWall, AllEraseAllLines
}


public class PowerUp : MonoBehaviour {

    public PowerUpsEnum powerUpKind;

	// Use this for initialization
	void Start () {
        // DELETE IT !@#!@#!@#
        powerUpKind = PowerUpsEnum.SelfSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
